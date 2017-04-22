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
        string wishlistLocation = "";
        string outputLocation = "";
        string csvLocation = "";
        double uStoAUDMultiplier = 0.0;
        double gainMultiplier = 0.0;
        double minValue = 0.25;
        double maxValue = 9999.00;
        bool nearestHalf = false;

        bool useMyPrice = false;
        OpenFileDialog fDialog;
        CardReader reader;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //Link the LinkLabels
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://www.twitter.com/0dd3sy/";
            linkTwitter.Links.Add(link);

            //Parse the options
            ParseGains();
            ParseConversionRate();
            useMyPrice = boolMyPrice.Checked;

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
                Process.Start(e.Link.LinkData as string);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonTarget_Click(object sender, EventArgs e)
        {
            generateDialog(".csv");
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                fDialog.InitialDirectory = GetDirectory(fDialog.FileName);
                csvLocation = fDialog.FileName;
                textTarget.Text = csvLocation;

                if (outputLocation.Equals(""))
                {
                    outputLocation = GetDirectory(fDialog.FileName) + "output.txt";
                    textOutput.Text = outputLocation;
                }

                fDialog.FileName = "";
            }

            buttonCreateOutput.Enabled = (csvLocation.Length > 0);
        }

        private void buttonWishlist_Click(object sender, EventArgs e)
        {
            generateDialog(".txt");
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                fDialog.InitialDirectory = GetDirectory(fDialog.FileName);
                wishlistLocation = fDialog.FileName;
                textWishlist.Text = wishlistLocation;
                fDialog.FileName = "";
            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            generateDialog(".txt");
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                fDialog.InitialDirectory = GetDirectory(fDialog.FileName);
                outputLocation = fDialog.FileName;
                textOutput.Text = outputLocation;
                fDialog.FileName = "";
            }
        }

        string GetDirectory(string input)
        {
            string output = "";
            string[] array = input.Split('\\');
            for (int i = 0; i < array.Length - 1; i++)
            {
                output += array[i] + "\\";
            }
            return output;
        }

        void generateDialog(string acceptedType)
        {
            if (fDialog == null)
            {
                fDialog = new OpenFileDialog();
                fDialog.InitialDirectory = Environment.CurrentDirectory;
                fDialog.Multiselect = false;
                fDialog.RestoreDirectory = true;
            }
            string filterType = (acceptedType.Equals(".txt")) ? "Text Document|*.txt" : "CSV File|*.csv";
            fDialog.Filter = filterType;
        }

        void GenerateCardReader()
        {
            reader = null;
            if (outputLocation == "")
                outputLocation = fDialog.InitialDirectory + "/output.txt";
            if (gainMultiplier != 0.0 && uStoAUDMultiplier != 0.0)
                reader = new CardReader(csvLocation, wishlistLocation, outputLocation, gainMultiplier, uStoAUDMultiplier, minValue, maxValue, nearestHalf);
            reader.useMyPrice = useMyPrice;
        }

        private void boolMyPrice_CheckedChanged(object sender, EventArgs e)
        {
            useMyPrice = boolMyPrice.Checked;
        }


        public void ParseGains()
        {
            if (!Double.TryParse(textGains.Text, out gainMultiplier))
            {
                MessageBox.Show("Gain Multiplier is not a number, please type an integer or decimal number");
                gainMultiplier = 0.0;
            }
        }

        public void ParseConversionRate()
        {
            if (!Double.TryParse(textUSMultiplier.Text, out uStoAUDMultiplier))
            {
                MessageBox.Show("Conversion Rate is not a number, please type an integer or decimal number");
                uStoAUDMultiplier = 0.0;
            }
        }

        private void textGains_TextChanged(object sender, EventArgs e)
        {
            ParseGains();
        }

        private void textUSMultiplier_TextChanged(object sender, EventArgs e)
        {
            ParseConversionRate();
        }

        private void buttonCreateOutput_Click(object sender, EventArgs e)
        {
            if (csvLocation.Length > 0)
            {
                GenerateCardReader();
                if (!reader.ReadFile())
                {
                    MessageBox.Show(reader.error);
                } else
                {
                    reader.PrintCards();
                    if (outputLocation.Length > 0 && boolOpenFile.Checked)
                        Process.Start(outputLocation);
                    textTotalValue.Text = "$" + Math.Round(reader.totalValue, 2);
                }
            } else
            {
                MessageBox.Show("Please ensure a DeckBox File is set");
            }
        }

        private void textRangeMin_TextChanged(object sender, EventArgs e)
        {
            double tempValue = minValue;
            if (!Double.TryParse(textRangeMin.Text, out tempValue))
            {
                MessageBox.Show("Conversion Rate is not a number, please type an integer or decimal number");
            } else if (tempValue < maxValue)
            {
                minValue = tempValue;
            } else
            {
                MessageBox.Show("Minimum Value must be lower than Maximum Value");
            }
        }

        private void textRangeMax_TextChanged(object sender, EventArgs e)
        {
            double tempValue = maxValue;
            if (!Double.TryParse(textRangeMax.Text, out tempValue))
            {
                MessageBox.Show("Conversion Rate is not a number, please type an integer or decimal number");
            } else if (tempValue > minValue)
            {
                maxValue = tempValue;
            } else
            {
                MessageBox.Show("Maximum Value must be higher than Minimum Value");
            }
        }

        private void boolNearestFifty_CheckedChanged(object sender, EventArgs e)
        {
            nearestHalf = boolNearestFifty.Checked;
        }
    }
}
