﻿/* License
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
using LumenWorks.Framework.IO.Csv;

namespace WindowsFormsApplication1
{
    class CardReader
    {
        public string error;
        public string output;
        public bool useMyPrice;
        public double totalValue = 0.0;

        private List<Card> cards;
        private int countCol = -1, foilCol = -1, nameCol = -1, priceCol = -1, editionCol = -1, conditionCol = -1, languageCol = -1, myPriceCol = -1;
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
            CsvReader reader;

            int count = 0;
            totalValue = 0.0;

            if (!File.Exists(csvLocation))
            {
                error += "File does not exist at: " + csvLocation + "\n";
                return false;
            }
            if (wishlistLocation.Length > 0)
                if (!File.Exists(wishlistLocation))
                {
                    error += "Wishlist does not exist at: " + wishlistLocation + "\n";
                    return false;
                }
            try
            {
                reader = new CsvReader(new StreamReader(csvLocation), true);
            } catch (Exception e)
            {
                error += "CSV file is in use or is invalid, please ensure that the file is not in use or of an invalid type. ERROR BELOW:\n\n";
                error += e + "\n\n";
                return false;
            }
            string[] headers = reader.GetFieldHeaders();

            //Parse Headers
            if (ParseHeadings(headers) == false)
            {
                reader.Dispose();
                return false;
            }

            if (useMyPrice && myPriceCol == -1)
            {
                error += "My Price Column not assigned, did you select \"Price\" as an additional column? \n";
                reader.Dispose();
                return false;
            }

            //Create new cards
            while (reader.ReadNextRecord())
            {
                try
                {
                    string[] line = new string[reader.FieldCount];
                    reader.CopyCurrentRecordTo(line, 0);
                    CreateCard(line);
                    count++;
                } catch (Exception ex)
                {
                    error += "Card creation failed: Line + " + (count + 1) + "\n";
                    error += ex.ToString();
                    reader = null;
                    return false;
                }
            }

            reader.Dispose();

            //Order the cards by price, descending
            cards = cards.Where(n => n.PriceAU > 0.0).ToList();
            cards = cards.OrderByDescending(n => n.PriceAU).ToList();
            return true;
        }

        private void CreateCard(string[] input)
        {
            Card card;

            if (useMyPrice)
                card = new Card(input[countCol], input[foilCol], input[nameCol], input[priceCol], input[editionCol], UStoAUDMultiplier, percentMultiplier, input[conditionCol], input[languageCol], input[myPriceCol]);
            else
                card = new Card(input[countCol], input[foilCol], input[nameCol], input[priceCol], input[editionCol], UStoAUDMultiplier, percentMultiplier, input[conditionCol], input[languageCol]);
            if (card.PriceAU >= minValue && card.PriceAU <= maxValue)
            {
                totalValue += card.PriceAU;
                cards.Add(card);
            }

        }

        public bool PrintCards()
        {
            double value = 0.0;
            bool completed = true;
            StreamWriter sw;
            if (File.Exists(outputLocation))
                File.Delete(outputLocation);

            sw = new StreamWriter(outputLocation);

            if (File.Exists(wishlistLocation))
                AppendWishlist();

            output += "WTS/WTT\n";

            foreach (Card c in cards)
            {
                if (c.PriceAU > minValue)
                    value += c.PriceAU;
                output += c.ToString() + "\n";
            }
            try
            {
                sw.WriteLine(output);
            } catch (Exception e)
            {
                completed = false;
                error += "Failed to write to file";
                Console.WriteLine(e.ToString());
            } finally
            {
                sw.Close();
            }
            return completed;
        }

        private bool AppendWishlist()
        {
            bool completed = true;
            StreamReader sr;
            if (wishlistLocation.Length == 0)
                return completed;

            sr = new StreamReader(wishlistLocation);
            try
            {
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
                sr.Close();
            }
            return completed;
        }

        private bool ParseHeadings(string[] columnNames)
        {
            bool completed = true;

            for (int i = 0; i < columnNames.Length; i++)
            {
                if (columnNames[i].Equals("Count"))
                    countCol = i;
                if (columnNames[i].Equals("Name"))
                    nameCol = i;
                if (columnNames[i].Equals("Foil"))
                    foilCol = i;
                if (columnNames[i].Equals("My Price") && useMyPrice)
                    myPriceCol = i;
                if (columnNames[i].Equals("Price"))
                    priceCol = i;
                if (columnNames[i].Equals("Edition"))
                    editionCol = i;
                if (columnNames[i].Equals("Language"))
                    languageCol = i;
                if (columnNames[i].Equals("Condition"))
                    conditionCol = i;
            }

            if (countCol == -1)
            {
                error += "Count Column not assigned \n";
                completed = false;
            }
            if (nameCol == -1)
            {
                error += "Name Column not assigned \n";
                completed = false;
            }
            if (foilCol == -1)
            {
                error += "Foil Column not assigned \n";
                completed = false;
            }
            if (priceCol == -1)
            {
                error += "Price Column not assigned \n";
                completed = false;
            }
            if (editionCol == -1)
            {
                error += "Edition Column not assigned \n";
                completed = false;
            }
            if (languageCol == -1)
            {
                error += "Language Column not assigned \n";
                completed = false;
            }
            if (conditionCol == -1)
            {
                error += "Condition Column not assigned \n";
                completed = false;
            }

            return completed;
        }
    }
}
