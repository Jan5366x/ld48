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
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(205, 137);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(122, 32);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtWorkDir
            // 
            this.txtWorkDir.Location = new System.Drawing.Point(12, 93);
            this.txtWorkDir.Name = "txtWorkDir";
            this.txtWorkDir.Size = new System.Drawing.Size(315, 20);
            this.txtWorkDir.TabIndex = 3;
            this.txtWorkDir.Text = "C:\\Users\\Jan\\Documents\\UnityGames\\ld48\\ExternalAssets\\Graphics\\Ground\\AutoGenerat" + "or";
            // 
            // lblWorkDir
            // 
            this.lblWorkDir.Location = new System.Drawing.Point(12, 72);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 199);
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

        private System.Windows.Forms.TextBox txtLayerCount;
        private System.Windows.Forms.Label lblLayerCount;

        private System.Windows.Forms.Label lblWorkDir;

        private System.Windows.Forms.TextBox txtWorkDir;

        private System.Windows.Forms.Button btnGenerate;

        #endregion
    }
}