namespace WindowsFormsApplication1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.linkTwitter = new System.Windows.Forms.LinkLabel();
            this.textTarget = new System.Windows.Forms.TextBox();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.buttonWishlist = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.textWishlist = new System.Windows.Forms.TextBox();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boolOpenFile = new System.Windows.Forms.CheckBox();
            this.labelLoadAfter = new System.Windows.Forms.Label();
            this.boolMyPrice = new System.Windows.Forms.CheckBox();
            this.labelMyPrice = new System.Windows.Forms.Label();
            this.textGains = new System.Windows.Forms.TextBox();
            this.labelOwnMultiplier = new System.Windows.Forms.Label();
            this.textUSMultiplier = new System.Windows.Forms.TextBox();
            this.labelUSMultiplier = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCreateOutput = new System.Windows.Forms.Button();
            this.textTotalValue = new System.Windows.Forms.TextBox();
            this.labelTotalValue = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelRangeMax = new System.Windows.Forms.Label();
            this.labelRangeMin = new System.Windows.Forms.Label();
            this.textRangeMax = new System.Windows.Forms.TextBox();
            this.textRangeMin = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Created by Timothy Patullock";
            // 
            // linkTwitter
            // 
            this.linkTwitter.AutoSize = true;
            this.linkTwitter.Location = new System.Drawing.Point(312, 270);
            this.linkTwitter.Name = "linkTwitter";
            this.linkTwitter.Size = new System.Drawing.Size(39, 13);
            this.linkTwitter.TabIndex = 1;
            this.linkTwitter.TabStop = true;
            this.linkTwitter.Text = "Twitter";
            this.linkTwitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textTarget
            // 
            this.textTarget.Enabled = false;
            this.textTarget.Location = new System.Drawing.Point(135, 14);
            this.textTarget.Name = "textTarget";
            this.textTarget.ReadOnly = true;
            this.textTarget.Size = new System.Drawing.Size(212, 20);
            this.textTarget.TabIndex = 2;
            // 
            // buttonTarget
            // 
            this.buttonTarget.Location = new System.Drawing.Point(12, 12);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(105, 23);
            this.buttonTarget.TabIndex = 3;
            this.buttonTarget.Text = "Deckbox File";
            this.buttonTarget.UseVisualStyleBackColor = true;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // buttonWishlist
            // 
            this.buttonWishlist.Location = new System.Drawing.Point(12, 70);
            this.buttonWishlist.Name = "buttonWishlist";
            this.buttonWishlist.Size = new System.Drawing.Size(105, 23);
            this.buttonWishlist.TabIndex = 4;
            this.buttonWishlist.Text = "Wishlist File";
            this.buttonWishlist.UseVisualStyleBackColor = true;
            this.buttonWishlist.Click += new System.EventHandler(this.buttonWishlist_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(12, 41);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(105, 23);
            this.buttonOutput.TabIndex = 5;
            this.buttonOutput.Text = "Output";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // textWishlist
            // 
            this.textWishlist.Enabled = false;
            this.textWishlist.Location = new System.Drawing.Point(135, 72);
            this.textWishlist.Name = "textWishlist";
            this.textWishlist.ReadOnly = true;
            this.textWishlist.Size = new System.Drawing.Size(212, 20);
            this.textWishlist.TabIndex = 6;
            // 
            // textOutput
            // 
            this.textOutput.Enabled = false;
            this.textOutput.Location = new System.Drawing.Point(135, 43);
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.Size = new System.Drawing.Size(212, 20);
            this.textOutput.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boolOpenFile);
            this.groupBox1.Controls.Add(this.labelLoadAfter);
            this.groupBox1.Controls.Add(this.boolMyPrice);
            this.groupBox1.Controls.Add(this.labelMyPrice);
            this.groupBox1.Controls.Add(this.textGains);
            this.groupBox1.Controls.Add(this.labelOwnMultiplier);
            this.groupBox1.Controls.Add(this.textUSMultiplier);
            this.groupBox1.Controls.Add(this.labelUSMultiplier);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 112);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // boolOpenFile
            // 
            this.boolOpenFile.AutoSize = true;
            this.boolOpenFile.Location = new System.Drawing.Point(123, 86);
            this.boolOpenFile.Name = "boolOpenFile";
            this.boolOpenFile.Size = new System.Drawing.Size(15, 14);
            this.boolOpenFile.TabIndex = 7;
            this.boolOpenFile.UseVisualStyleBackColor = true;
            // 
            // labelLoadAfter
            // 
            this.labelLoadAfter.AutoSize = true;
            this.labelLoadAfter.Location = new System.Drawing.Point(7, 87);
            this.labelLoadAfter.Name = "labelLoadAfter";
            this.labelLoadAfter.Size = new System.Drawing.Size(75, 13);
            this.labelLoadAfter.TabIndex = 6;
            this.labelLoadAfter.Text = "Load File After";
            // 
            // boolMyPrice
            // 
            this.boolMyPrice.AutoSize = true;
            this.boolMyPrice.Location = new System.Drawing.Point(123, 66);
            this.boolMyPrice.Name = "boolMyPrice";
            this.boolMyPrice.Size = new System.Drawing.Size(15, 14);
            this.boolMyPrice.TabIndex = 5;
            this.boolMyPrice.UseVisualStyleBackColor = true;
            this.boolMyPrice.CheckedChanged += new System.EventHandler(this.boolMyPrice_CheckedChanged);
            // 
            // labelMyPrice
            // 
            this.labelMyPrice.AutoSize = true;
            this.labelMyPrice.Location = new System.Drawing.Point(7, 67);
            this.labelMyPrice.Name = "labelMyPrice";
            this.labelMyPrice.Size = new System.Drawing.Size(80, 13);
            this.labelMyPrice.TabIndex = 4;
            this.labelMyPrice.Text = "Use \"My Price\"";
            // 
            // textGains
            // 
            this.textGains.Location = new System.Drawing.Point(123, 40);
            this.textGains.Name = "textGains";
            this.textGains.Size = new System.Drawing.Size(100, 20);
            this.textGains.TabIndex = 3;
            this.textGains.Text = "1.3";
            this.textGains.TextChanged += new System.EventHandler(this.textGains_TextChanged);
            // 
            // labelOwnMultiplier
            // 
            this.labelOwnMultiplier.AutoSize = true;
            this.labelOwnMultiplier.Location = new System.Drawing.Point(7, 43);
            this.labelOwnMultiplier.Name = "labelOwnMultiplier";
            this.labelOwnMultiplier.Size = new System.Drawing.Size(78, 13);
            this.labelOwnMultiplier.TabIndex = 2;
            this.labelOwnMultiplier.Text = "Gains Multiplier";
            // 
            // textUSMultiplier
            // 
            this.textUSMultiplier.Location = new System.Drawing.Point(123, 17);
            this.textUSMultiplier.Name = "textUSMultiplier";
            this.textUSMultiplier.Size = new System.Drawing.Size(100, 20);
            this.textUSMultiplier.TabIndex = 1;
            this.textUSMultiplier.Text = "1.3";
            this.textUSMultiplier.TextChanged += new System.EventHandler(this.textUSMultiplier_TextChanged);
            // 
            // labelUSMultiplier
            // 
            this.labelUSMultiplier.AutoSize = true;
            this.labelUSMultiplier.Location = new System.Drawing.Point(7, 20);
            this.labelUSMultiplier.Name = "labelUSMultiplier";
            this.labelUSMultiplier.Size = new System.Drawing.Size(86, 13);
            this.labelUSMultiplier.TabIndex = 0;
            this.labelUSMultiplier.Text = "Conversion Rate";
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // buttonCreateOutput
            // 
            this.buttonCreateOutput.Enabled = false;
            this.buttonCreateOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateOutput.Location = new System.Drawing.Point(12, 260);
            this.buttonCreateOutput.Name = "buttonCreateOutput";
            this.buttonCreateOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateOutput.TabIndex = 9;
            this.buttonCreateOutput.Text = "Create";
            this.buttonCreateOutput.UseVisualStyleBackColor = true;
            this.buttonCreateOutput.Click += new System.EventHandler(this.buttonCreateOutput_Click);
            // 
            // textTotalValue
            // 
            this.textTotalValue.Location = new System.Drawing.Point(135, 234);
            this.textTotalValue.Name = "textTotalValue";
            this.textTotalValue.Size = new System.Drawing.Size(100, 20);
            this.textTotalValue.TabIndex = 8;
            this.textTotalValue.Text = "$0.0";
            // 
            // labelTotalValue
            // 
            this.labelTotalValue.AutoSize = true;
            this.labelTotalValue.Location = new System.Drawing.Point(19, 237);
            this.labelTotalValue.Name = "labelTotalValue";
            this.labelTotalValue.Size = new System.Drawing.Size(61, 13);
            this.labelTotalValue.TabIndex = 8;
            this.labelTotalValue.Text = "Total Value";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textRangeMin);
            this.groupBox2.Controls.Add(this.textRangeMax);
            this.groupBox2.Controls.Add(this.labelRangeMin);
            this.groupBox2.Controls.Add(this.labelRangeMax);
            this.groupBox2.Location = new System.Drawing.Point(241, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 112);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Range";
            // 
            // labelRangeMax
            // 
            this.labelRangeMax.AutoSize = true;
            this.labelRangeMax.Location = new System.Drawing.Point(7, 24);
            this.labelRangeMax.Name = "labelRangeMax";
            this.labelRangeMax.Size = new System.Drawing.Size(27, 13);
            this.labelRangeMax.TabIndex = 8;
            this.labelRangeMax.Text = "Max";
            // 
            // labelRangeMin
            // 
            this.labelRangeMin.AutoSize = true;
            this.labelRangeMin.Location = new System.Drawing.Point(10, 46);
            this.labelRangeMin.Name = "labelRangeMin";
            this.labelRangeMin.Size = new System.Drawing.Size(24, 13);
            this.labelRangeMin.TabIndex = 9;
            this.labelRangeMin.Text = "Min";
            // 
            // textRangeMax
            // 
            this.textRangeMax.Location = new System.Drawing.Point(40, 21);
            this.textRangeMax.Name = "textRangeMax";
            this.textRangeMax.Size = new System.Drawing.Size(66, 20);
            this.textRangeMax.TabIndex = 8;
            this.textRangeMax.Text = "9999.99";
            this.textRangeMax.TextChanged += new System.EventHandler(this.textRangeMax_TextChanged);
            // 
            // textRangeMin
            // 
            this.textRangeMin.Location = new System.Drawing.Point(40, 43);
            this.textRangeMin.Name = "textRangeMin";
            this.textRangeMin.Size = new System.Drawing.Size(66, 20);
            this.textRangeMin.TabIndex = 10;
            this.textRangeMin.Text = "0.25";
            this.textRangeMin.TextChanged += new System.EventHandler(this.textRangeMin_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 283);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelTotalValue);
            this.Controls.Add(this.textTotalValue);
            this.Controls.Add(this.buttonCreateOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.textWishlist);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonWishlist);
            this.Controls.Add(this.buttonTarget);
            this.Controls.Add(this.textTarget);
            this.Controls.Add(this.linkTwitter);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Deckbox To Text";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkTwitter;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Button buttonTarget;
        private System.Windows.Forms.Button buttonWishlist;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.TextBox textWishlist;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUSMultiplier;
        private System.Windows.Forms.TextBox textUSMultiplier;
        private System.Windows.Forms.CheckBox boolMyPrice;
        private System.Windows.Forms.Label labelMyPrice;
        private System.Windows.Forms.TextBox textGains;
        private System.Windows.Forms.Label labelOwnMultiplier;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonCreateOutput;
        private System.Windows.Forms.CheckBox boolOpenFile;
        private System.Windows.Forms.Label labelLoadAfter;
        private System.Windows.Forms.TextBox textTotalValue;
        private System.Windows.Forms.Label labelTotalValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textRangeMin;
        private System.Windows.Forms.TextBox textRangeMax;
        private System.Windows.Forms.Label labelRangeMin;
        private System.Windows.Forms.Label labelRangeMax;
    }
}

