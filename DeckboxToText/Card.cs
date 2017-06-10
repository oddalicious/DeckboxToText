/* License
The MIT License (MIT)
Copyright (C) 2016 Timothy Patullock

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

namespace WindowsFormsApplication1
{
    internal class Card
    {

        private readonly int _count;
        private readonly bool _foil;
        private readonly string _name;
        private double _price;
        private readonly double _priceAus;
        private readonly string _edition;
        private readonly string _condition;
        private readonly string _language;
        private readonly string _cardNo;

        public double PriceAus => _priceAus;

        public double TotalPriceAus => _priceAus * _count;

        public Card(string count, string name, string edition, string condition, string language, string foil,  string price,  double exchangeMultiplier, double percentMultiplier, bool useMyPrice, bool nearestHalf, string cardNo)
        {
            int.TryParse(count, out _count);
            _foil = foil.Contains("foil");
            _name = name;
            _name = _name.Replace("\"", string.Empty);
            if (price[0] == '$')
                price = price.Remove(0, 1);
            double.TryParse(price, out _priceAus);
            _edition = edition;
            _language = language;
            _condition = condition;
            if (useMyPrice)
                _priceAus /= 100;
            _priceAus = _priceAus * exchangeMultiplier * percentMultiplier;
            //ROUND TO NEAREST .5
            if (nearestHalf)
            {
                _priceAus *= 2;
                _priceAus = Math.Round(_priceAus, MidpointRounding.AwayFromZero) / 2;
            }
            else
            {
                _priceAus = Math.Round(_priceAus, 2);
            }
            _cardNo = cardNo;
        }

        public override string ToString()
        {
            var output = "" + _count + " ";
            output += (_foil) ? "FOIL " : "";
            output += _name + " ";
            output += IsBasicLand() ? "#" + _cardNo + " " : "";
            output += "(" + _edition;
            output += (_condition.Equals("Near Mint")) ? "" : " - " + _condition;
            output += (_language.Equals("English")) ? "" : " - " + _language;
            output += ") ";
            output += "$";
            output += _priceAus;
            output += (_count > 1) ? "ea" : "";
            return output;
        }

        private bool IsBasicLand()
        {
            if (_name.CompareTo("Plains") == 0)
            {
                return true;
            }
            else if (_name.CompareTo("Island") == 0)
            {
                return true;
            }
            else if (_name.CompareTo("Swamp") == 0)
            {
                return true;
            }
            else if (_name.CompareTo("Mountain") == 0)
            {
                return true;
            }
            else if (_name.CompareTo("Forest") == 0)
            {
                return true;
            }
            else if (_name.CompareTo("Wastes") == 0)
            {
                return true;
            }
            return false;
        }
    }
}
