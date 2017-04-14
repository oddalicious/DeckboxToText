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
    class Card
    {

        private int count;
        private bool foil;
        private String name;
        private double price;
        private double priceAU;
        private string edition;
        private double percentMultiplier = 1.0;
        private string condition;
        private string language;

        public double PriceAU
        {
            get
            {
                return priceAU;
            }
        }

        public Card(string _count, string _name, string _edition, string _condition, string _language, string _foil,  string _price,  double _exchangeMultiplier, double _percentMultiplier)
        {
            Int32.TryParse(_count, out count);
            foil = (_foil.Contains("foil")) ? true : false;
            name = _name;
            name = name.Replace("\"", string.Empty);
            if (_price[0] == '$')
                _price = _price.Remove(0, 1);
            Double.TryParse(_price, out priceAU);
            percentMultiplier = _percentMultiplier;
            edition = _edition;
            language = _language;
            condition = _condition;
            priceAU = priceAU / 100;
            priceAU = priceAU * _exchangeMultiplier * percentMultiplier;
            priceAU = Math.Round(priceAU, 2);
        }

        public override string ToString()
        {
            string output = "" + count + " ";
            output += (foil) ? "FOIL " : "";
            output += name + " ";
            output += "(" + edition;
            output += (condition.Equals("Near Mint")) ? "" : " - " + condition;
            output += (language.Equals("English")) ? "" : " - " + language;
            output += ") ";
            output += "$";
            output += priceAU.ToString();
            output += (count > 1) ? "ea" : "";
            return output;
        }
    }
}
