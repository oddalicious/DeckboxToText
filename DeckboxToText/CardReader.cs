/* License
The MIT License (MIT)
Copyright (C) 2016 Timothy Patullock

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    class CardReader
    {
        public string error;
        public string output;
        public bool useMyPrice;
        public double totalValue = 0.0;

        private List<Card> cards;
        private string headers;
        private string wishlistLocation = "", outputLocation = "", csvLocation = "";
        private double percentMultiplier = 1.0, UStoAUDMultiplier = 1.3, minValue = 0.0, maxValue = 9999.00;

        public CardReader(string _csvLocation, string _wishListLocation, string _outputLocation, double _percentMultiplier = 1.0, double _UStoAUDMultiplier = 1.0, double _minValue = 0.25, double _maxValue = 9999.00)
        {
            csvLocation = _csvLocation;
            wishlistLocation = _wishListLocation;
            outputLocation = _outputLocation;
            percentMultiplier = _percentMultiplier;
            UStoAUDMultiplier = _UStoAUDMultiplier;
            output = "";
            error = "";
            cards = new List<Card>();
            minValue = _minValue;
            maxValue = _maxValue;
        }

        public bool ReadFile()
        {
            totalValue = 0.0;
            string[] headers;
            string[] fileList;
            Dictionary<string, List<string>> lines = new Dictionary<string, List<String>>();

            if (!File.Exists(csvLocation))
            {
                error += "File does not exist at: " + csvLocation + "\n";
                return false;
            }

            if (wishlistLocation.Length > 0)
            {
                if (!File.Exists(wishlistLocation))
                {
                    error += "Wishlist does not exist at: " + wishlistLocation + "\n";
                    return false;
                }
            }
            
            try
            {
                fileList = File.ReadAllLines(csvLocation);
            }
            catch (Exception e)
            {
                error += "Error has occured opening the CSV";
                return false;
            }

            headers = Regex.Split(fileList[0], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            //Add the headings and their related Lists
            for (int i = 0; i < headers.Length; i++)
            {
                lines.Add(headers[i], new List<string>());
            }
            //Populate the lists
            //For each Item
            for (int i = 0; i < fileList.Length; i++)
            {
                //Split the item into Columns
                string[] splitLine = Regex.Split(fileList[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                //Add the relevant item to the heading (ASSUMING THEY ARE ALL THE SAME LENGTH)
                for (int dictIndex = 0; dictIndex < splitLine.Length; dictIndex++)
                {
                    try
                    {
                        if (lines[headers[dictIndex]] == null)
                            break;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                        
                    //Get the headings string to minimise shenanigans
                    lines[headers[dictIndex]].Add(splitLine[dictIndex]);
                }
            }

            //Parse Headers
            if (ParseHeadings(lines) == false)
            {
                return false;
            }

            for (int i = 0; i < lines[headers[0]].Count; i++)
            {
                Card c = new Card(lines["Count"][i], lines["Name"][i], lines["Edition"][i], lines["Condition"][i], lines["Language"][i], lines["Foil"][i], useMyPrice ? lines["My Price"][i] : lines["Price"][i], UStoAUDMultiplier, percentMultiplier);
                if (c.PriceAU >= minValue && c.PriceAU < maxValue)
                {
                    totalValue += c.PriceAU;
                    cards.Add(c);
                }
            }

            //Order the cards by price, descending
            cards = cards.Where(n => n.PriceAU > 0.0).ToList();
            cards = cards.OrderByDescending(n => n.PriceAU).ToList();
            return true;
        }

        public bool PrintCards()
        {
            StreamWriter sw = null;
            double value = 0.0;
            bool completed = true;

            try
            {
                //Why update when you can just nuke and rebuild?
                if (File.Exists(outputLocation))
                    File.Delete(outputLocation);

                //Add the Wishlist if it exists
                if (wishlistLocation != "" && File.Exists(wishlistLocation))
                    AppendWishlist();

                //So they know that this is what we're selling
                output += "WTS/WTT\n";

                //Loop through and add the additional cards
                foreach (Card c in cards)
                {
                    if (c.PriceAU > minValue)
                    {
                        value += c.PriceAU;
                        output += c.ToString() + "\n";
                    }
                }

                //Open and write to the output file
                sw = new StreamWriter(outputLocation);
                sw.WriteLine(output);
            } catch (Exception e)
            {
                completed = false;
                error += "Failed to write to file";
                Console.WriteLine(e.ToString());
            } finally
            {
                if (sw != null)
                    sw.Close();
            }
            return completed;
        }

        private bool AppendWishlist()
        {
            bool completed = true;
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(wishlistLocation);
                while (!sr.EndOfStream)
                {
                    output += sr.ReadLine() + "\n";
                }
                output += "\n\n";
            } catch (Exception e)
            {
                error += "Error occured appending wishlist: ";
                error += e.ToString();
                completed = false;
            } finally
            {
                if (sr != null)
                    sr.Close();
            }
            return completed;
        }

        private bool ParseHeadings(Dictionary<string,List<String>> cardList)
        {
            if (cardList["Count"] == null)
            {
                error += "Count Column not assigned \n";
                return false;
            } 
            if (cardList["Name"] == null)
            {
                error += "Name Column not assigned \n";
                return false;
            }
            if (cardList["Foil"] == null)
            {
                error += "Foil Column not assigned \n";
                return false;
            }
            if (cardList["My Price"] == null && useMyPrice)
            {
                error += "My Price Column not assigned \n";
                return false;
            }
            if (cardList["Price"] == null)
            {
                error += "Price Column not assigned \n";
                return false;
            }
            if (cardList["Edition"] == null)
            {
                error += "Edition Column not assigned \n";      
                return false;
            }
            if (cardList["Language"] == null)
            {
                error += "Language Column not assigned \n";
                return false;
            }
            if (cardList["Condition"] == null)
            {
                error += "Condition Column not assigned \n";
                return false;
            }
            return true;
        }
    }

}
