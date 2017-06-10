/* License
The MIT License (MIT)
Copyright (C) 2016 Timothy Patullock

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        private string _wishlistLocation = "";
        private string _outputLocation = "";
        private string _csvLocation = "";
        private double _uStoAudMultiplier;
        private double _gainMultiplier;
        private double _minValue = 0.25;
        private double _maxValue = 9999.00;
        private bool _nearestHalf;

        private bool _useMyPrice;
        private OpenFileDialog _fDialog;
        private CardReader _reader;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Link the LinkLabels
            LinkLabel.Link link = new LinkLabel.Link {LinkData = "https://www.twitter.com/0dd3sy/"};
            linkTwitter.Links.Add(link);

            //Parse the options
            ParseGains();
            ParseConversionRate();
            _useMyPrice = boolMyPrice.Checked;

            //Create Tooltips
            toolTip.SetToolTip(labelOwnMultiplier, "Modify the eventual price, default 1.3");
            toolTip.SetToolTip(labelUSMultiplier, "US to AUD Multiplier, default 1.3");
            toolTip.SetToolTip(labelMyPrice, "If you exported with \"My Price\" and want to use that. The application will throw an error if \"My Price\" does not exist");
            toolTip.SetToolTip(buttonWishlist, "OPTIONAL: Location of your wishlist (.txt document preferrably), if location is empty, it won't append it");
            toolTip.SetToolTip(buttonTarget, "Location of your Deckbox Export file, must be set to a file");
            toolTip.SetToolTip(buttonOutput, "OPTIONAL: Location of output file, if empty this will autogenerate one in the folder of the executable.");
            toolTip.SetToolTip(labelLoadAfter, "OPTIONAL: Load the output file after conversion. THIS MAY CAUSE A POPUP TO SELECT DEFAULT EDITOR IF ONE IS NOT SELECTED ALREADY!");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (e != null) Process.Start((string) e.Link.LinkData);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonTarget_Click(object sender, EventArgs e)
        {
            GenerateDialog(".csv");
            if (_fDialog.ShowDialog() == DialogResult.OK)
            {
                _fDialog.InitialDirectory = GetDirectory(_fDialog.FileName);
                _csvLocation = _fDialog.FileName;
                textTarget.Text = _csvLocation;

                if (_outputLocation.Equals(""))
                {
                    _outputLocation = GetDirectory(_fDialog.InitialDirectory) + "output.txt";
                    textOutput.Text = _outputLocation;
                }

                _fDialog.FileName = "";
            }

            buttonCreateOutput.Enabled = (_csvLocation.Length > 0);
        }

        private void buttonWishlist_Click(object sender, EventArgs e)
        {
            GenerateDialog(".txt");
            if (_fDialog.ShowDialog() != DialogResult.OK) return;
            _fDialog.InitialDirectory = GetDirectory(_fDialog.FileName);
            _wishlistLocation = _fDialog.FileName;
            textWishlist.Text = _wishlistLocation;
            _fDialog.FileName = "";
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            GenerateDialog(".txt");
            if (_fDialog.ShowDialog() != DialogResult.OK) return;
            _fDialog.InitialDirectory = GetDirectory(_fDialog.FileName);
            _outputLocation = _fDialog.FileName;
            textOutput.Text = _outputLocation;
            _fDialog.FileName = "";
        }

        private static string GetDirectory(string input)
        {
            var output = "";
            var array = input.Split('\\');
            for (var i = 0; i < array.Length - 1; i++)
            {
                output += array[i] + "\\";
            }
            return output;
        }

        private void GenerateDialog(string acceptedType)
        {
            if (_fDialog == null)
            {
                _fDialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.CurrentDirectory,
                    Multiselect = false,
                    RestoreDirectory = true
                };
            }
            var filterType = (acceptedType.Equals(".txt")) ? "Text Document|*.txt" : "CSV File|*.csv";
            _fDialog.Filter = filterType;
        }

        private void GenerateCardReader()
        {
            _reader = null;
            if (_outputLocation == "")
                _outputLocation = _fDialog.InitialDirectory + "/output.txt";
                _reader = new CardReader(_csvLocation, _wishlistLocation, _outputLocation, _gainMultiplier, _uStoAudMultiplier, _minValue, _maxValue, _nearestHalf);
            if (_reader != null)
                _reader.UseMyPrice = _useMyPrice;
        }

        private void boolMyPrice_CheckedChanged(object sender, EventArgs e)
        {
            _useMyPrice = boolMyPrice.Checked;
        }


        public bool ParseGains()
        {
            return double.TryParse(textGains.Text, out _gainMultiplier);
        }

        public bool ParseConversionRate()
        {
            return double.TryParse(textUSMultiplier.Text, out _uStoAudMultiplier);
        }

        private void textGains_TextChanged(object sender, EventArgs e)
        {
            CheckRequirements();
        }

        private void textUSMultiplier_TextChanged(object sender, EventArgs e)
        {
            CheckRequirements();
        }

        private void buttonCreateOutput_Click(object sender, EventArgs e)
        {
            if (_csvLocation.Length > 0)
            {
                GenerateCardReader();
                if (!_reader.ReadFile())
                {
                    MessageBox.Show(_reader.Error);
                } else
                {
                    _reader.PrintCards();
                    if (_outputLocation.Length > 0 && boolOpenFile.Checked)
                        Process.Start(_outputLocation);
                    textTotalValue.Text = @"$" + Math.Round(_reader.TotalValue, 2);
                }
            } else
            {
                MessageBox.Show(@"Please ensure a DeckBox File is set");
            }
        }

        private void textRangeMin_TextChanged(object sender, EventArgs e)
        {
            CheckRequirements();
        }

        private void textRangeMax_TextChanged(object sender, EventArgs e)
        {
            CheckRequirements();
        }

        private void boolNearestFifty_CheckedChanged(object sender, EventArgs e)
        {
            _nearestHalf = boolNearestFifty.Checked;
        }

        private void CheckRequirements()
        {
            var isReady = true;
            if (!CheckRangeMax())
                isReady = false;
            else if (!CheckRangeMin())
                isReady = false;
            else if (!ParseConversionRate())
                isReady = false;
            else if (!ParseGains())
                isReady = false;

            buttonCreateOutput.Enabled = isReady;
        }

        private bool CheckRangeMax()
        {
            double tempValue;
            if (!double.TryParse(textRangeMax.Text, out tempValue))
            {
                MessageBox.Show(@"Conversion Rate is not a number, please type an integer or decimal number");
                return false;
            }
            else if (tempValue <= _minValue)
            {
                MessageBox.Show(@"Maximum Value must be higher than Minimum Value");
                return false;
            }
            _maxValue = tempValue;
            return true;
        }

        private bool CheckRangeMin()
        {
            double tempValue;
            if (!double.TryParse(textRangeMin.Text, out tempValue))
            {
                MessageBox.Show(@"Conversion Rate is not a number, please type an integer or decimal number");
                return false;
            }
            else if (tempValue >= _maxValue)
            {
                MessageBox.Show(@"Minimum Value must be lower than Maximum Value");
                return false;
            }
            _minValue = tempValue;
            return true;
        }
    }
}
