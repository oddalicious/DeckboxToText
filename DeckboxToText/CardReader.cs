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
    internal class CardReader
    {
        public string Error;
        public string Output;
        public bool UseMyPrice;
        public double TotalValue;

        private List<Card> _cards;
        private readonly bool _nearestHalf;
        private readonly string[] _headers = { "Count", "Name", "Edition", "Condition", "Language", "Foil", "My Price", "Price", "Card Number" };
        public string WishListLocation { get; }
        private readonly string _outputLocation;
        private readonly string _csvLocation;
        private readonly double _percentMultiplier;
        private readonly double _uStoAudMultiplier;
        private readonly double _minValue;
        private readonly double _maxValue;

        public CardReader(string csvLocation, string wishListLocation, string outputLocation, double percentMultiplier = 1.0, double uStoAudMultiplier = 1.0, double minValue = 0.25, double maxValue = 9999.00, bool nearestHalf = false)
        {
            _csvLocation = csvLocation;
            WishListLocation = wishListLocation;
            _outputLocation = outputLocation;
            _percentMultiplier = percentMultiplier;
            _uStoAudMultiplier = uStoAudMultiplier;
            Output = "";
            Error = "";
            _cards = new List<Card>();
            _minValue = minValue;
            _maxValue = maxValue;
            _nearestHalf = nearestHalf;
        }

        public bool ReadFile()
        {
            TotalValue = 0.0;
            string[] fileList;

            if (!File.Exists(_csvLocation))
            {
                Error += "File does not exist at: " + _csvLocation + "\n";
                return false;
            }

            if (WishListLocation.Length > 0)
            {
                if (!File.Exists(WishListLocation))
                {
                    Error += "Wishlist does not exist at: " + WishListLocation + "\n";
                    return false;
                }
            }
            
            try
            {
                fileList = File.ReadAllLines(_csvLocation);
            }
            catch (Exception e)
            {
                Error += "Error has occured opening the CSV";
                return false;
            }

            var tempHeaders = Regex.Split(fileList[0], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            //Add the headings and their related Lists
            var lines = tempHeaders.Where(t => _headers.Contains(t)).ToDictionary(t => t, t => new List<string>());
            //Populate the lists
            //For each Item - Begin at 1 to prevent headings being included
            for (var i = 1; i < fileList.Length; i++)
            {
                //Split the item into Columns
                string[] splitLine = Regex.Split(fileList[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                if (splitLine[0] == null || splitLine[0] == "")
                    continue;
                //Add the relevant item to the heading (ASSUMING THEY ARE ALL THE SAME LENGTH)
                for (int dictIndex = 0; dictIndex < splitLine.Length; dictIndex++)
                {
                    if (_headers.Contains(tempHeaders[dictIndex]))
                    //Get the headings string to minimise shenanigans
                    lines[tempHeaders[dictIndex]].Add(splitLine[dictIndex]);
                }
            }

            //Parse Headers
            if (ParseHeadings(lines) == false)
            {
                return false;
            }

            for (var i = 0; i < lines[_headers[0]].Count; i++)
            {
                var c = new Card(lines[_headers[0]][i], lines[_headers[1]][i], lines[_headers[2]][i], lines[_headers[3]][i], lines[_headers[4]][i], lines[_headers[5]][i], UseMyPrice ? lines[_headers[6]][i] : lines[_headers[7]][i], _uStoAudMultiplier, _percentMultiplier, UseMyPrice, _nearestHalf, lines[_headers[8]][i]);
                if (!(c.PriceAus >= _minValue) || !(c.PriceAus < _maxValue)) continue;
                TotalValue += c.TotalPriceAus;
                _cards.Add(c);
            }

            //Order the cards by price, descending
            _cards = _cards.Where(n => n.PriceAus > 0.0).ToList();
            _cards = _cards.OrderByDescending(n => n.PriceAus).ToList();
            return true;
        }

        public bool PrintCards()
        {
            StreamWriter sw = null;
            var completed = true;

            try
            {
                //Why update when you can just nuke and rebuild?
                if (File.Exists(_outputLocation))
                    File.Delete(_outputLocation);

                //Add the Wishlist if it exists
                if (WishListLocation != "" && File.Exists(WishListLocation))
                    AppendWishlist();

                //So they know that this is what we're selling
                Output += "WTS/WTT\n";

                //Loop through and add the additional cards
                foreach (var c in _cards)
                {
                    if (c.PriceAus > _minValue)
                    {
                        Output += c + "\n";
                    }
                }

                //Open and write to the output file
                sw = new StreamWriter(_outputLocation);
                sw.WriteLine(Output);
            } catch (Exception e)
            {
                completed = false;
                Error += "Failed to write to file";
                Console.WriteLine(e.ToString());
            } finally
            {
                sw?.Close();
            }
            return completed;
        }

        private void AppendWishlist()
        {
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(WishListLocation);
                while (!sr.EndOfStream)
                {
                    Output += sr.ReadLine() + "\n";
                }
                Output += "\n\n";
            } catch (Exception e)
            {
                Error += "Error occured appending wishlist: ";
                Error += e.ToString();
            } finally
            {
                sr?.Close();
            }
        }

        private bool ParseHeadings(Dictionary<string,List<String>> cardList)
        {
            if (cardList["Count"] == null)
            {
                Error += "Count Column not assigned \n";
                return false;
            } 
            if (cardList["Name"] == null)
            {
                Error += "Name Column not assigned \n";
                return false;
            }
            if (cardList["Foil"] == null)
            {
                Error += "Foil Column not assigned \n";
                return false;
            }
            if (cardList["My Price"] == null && UseMyPrice)
            {
                Error += "My Price Column not assigned \n";
                return false;
            }
            if (cardList["Price"] == null)
            {
                Error += "Price Column not assigned \n";
                return false;
            }
            if (cardList["Edition"] == null)
            {
                Error += "Edition Column not assigned \n";      
                return false;
            }
            if (cardList["Language"] == null)
            {
                Error += "Language Column not assigned \n";
                return false;
            }
            if (cardList["Condition"] == null)
            {
                Error += "Condition Column not assigned \n";
                return false;
            }
            return true;
        }
    }

}
