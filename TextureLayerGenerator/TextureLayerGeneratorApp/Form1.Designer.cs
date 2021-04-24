namespace TextureLayerGeneratorApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtWorkDir = new System.Windows.Forms.TextBox();
            this.lblWorkDir = new System.Windows.Forms.Label();
            this.txtLayerCount = new System.Windows.Forms.TextBox();
            this.lblLayerCount = new System.Windows.Forms.Label();
            this.txtTcR = new System.Windows.Forms.TextBox();
            this.txtTcG = new System.Windows.Forms.TextBox();
            this.txtTcB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxTransparentColor = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(205, 209);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(122, 32);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtWorkDir
            // 
            this.txtWorkDir.Location = new System.Drawing.Point(12, 88);
            this.txtWorkDir.Name = "txtWorkDir";
            this.txtWorkDir.Size = new System.Drawing.Size(315, 20);
            this.txtWorkDir.TabIndex = 3;
            this.txtWorkDir.Text = "C:\\Users\\Jan\\Documents\\UnityGames\\ld48\\ExternalAssets\\Graphics\\Ground\\AutoGenerat" + "or";
            // 
            // lblWorkDir
            // 
            this.lblWorkDir.Location = new System.Drawing.Point(12, 67);
            this.lblWorkDir.Name = "lblWorkDir";
            this.lblWorkDir.Size = new System.Drawing.Size(104, 18);
            this.lblWorkDir.TabIndex = 4;
            this.lblWorkDir.Text = "Working  Directory:";
            // 
            // txtLayerCount
            // 
            this.txtLayerCount.Location = new System.Drawing.Point(12, 40);
            this.txtLayerCount.Name = "txtLayerCount";
            this.txtLayerCount.Size = new System.Drawing.Size(81, 20);
            this.txtLayerCount.TabIndex = 5;
            this.txtLayerCount.Text = "3";
            // 
            // lblLayerCount
            // 
            this.lblLayerCount.Location = new System.Drawing.Point(12, 22);
            this.lblLayerCount.Name = "lblLayerCount";
            this.lblLayerCount.Size = new System.Drawing.Size(100, 15);
            this.lblLayerCount.TabIndex = 6;
            this.lblLayerCount.Text = "Layer Count:";
            // 
            // txtTcR
            // 
            this.txtTcR.Location = new System.Drawing.Point(12, 164);
            this.txtTcR.Name = "txtTcR";
            this.txtTcR.Size = new System.Drawing.Size(42, 20);
            this.txtTcR.TabIndex = 7;
            this.txtTcR.Text = "0";
            // 
            // txtTcG
            // 
            this.txtTcG.Location = new System.Drawing.Point(60, 164);
            this.txtTcG.Name = "txtTcG";
            this.txtTcG.Size = new System.Drawing.Size(42, 20);
            this.txtTcG.TabIndex = 8;
            this.txtTcG.Text = "250";
            // 
            // txtTcB
            // 
            this.txtTcB.Location = new System.Drawing.Point(108, 164);
            this.txtTcB.Name = "txtTcB";
            this.txtTcB.Size = new System.Drawing.Size(42, 20);
            this.txtTcB.TabIndex = 9;
            this.txtTcB.Text = "255";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "R";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(60, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "G";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(108, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "B";
            // 
            // cbxTransparentColor
            // 
            this.cbxTransparentColor.Checked = true;
            this.cbxTransparentColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxTransparentColor.Location = new System.Drawing.Point(9, 129);
            this.cbxTransparentColor.Name = "cbxTransparentColor";
            this.cbxTransparentColor.Size = new System.Drawing.Size(141, 15);
            this.cbxTransparentColor.TabIndex = 14;
            this.cbxTransparentColor.Text = "transparent color";
            this.cbxTransparentColor.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 254);
            this.Controls.Add(this.cbxTransparentColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTcB);
            this.Controls.Add(this.txtTcG);
            this.Controls.Add(this.txtTcR);
            this.Controls.Add(this.lblLayerCount);
            this.Controls.Add(this.txtLayerCount);
            this.Controls.Add(this.lblWorkDir);
            this.Controls.Add(this.txtWorkDir);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Texture Layer Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox cbxTransparentColor;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.TextBox txtTcB;

        private System.Windows.Forms.TextBox txtTcG;

        private System.Windows.Forms.TextBox txtTcR;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TextBox txtLayerCount;
        private System.Windows.Forms.Label lblLayerCount;

        private System.Windows.Forms.Label lblWorkDir;

        private System.Windows.Forms.TextBox txtWorkDir;

        private System.Windows.Forms.Button btnGenerate;

        #endregion
    }
}