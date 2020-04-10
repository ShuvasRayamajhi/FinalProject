namespace StegApp
{
    partial class Main
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnOpen = new MetroFramework.Controls.MetroTile();
            this.btnEncode = new MetroFramework.Controls.MetroTile();
            this.btnSave = new MetroFramework.Controls.MetroTile();
            this.btnDecode = new MetroFramework.Controls.MetroTile();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(23, 63);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(447, 255);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(395, 340);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 38);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open File";
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(395, 398);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(75, 38);
            this.btnEncode.TabIndex = 2;
            this.btnEncode.Text = "Encode";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(395, 509);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(395, 451);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 38);
            this.btnDecode.TabIndex = 3;
            this.btnDecode.Text = "Decode";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 589);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.picBox);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private MetroFramework.Controls.MetroTile btnOpen;
        private MetroFramework.Controls.MetroTile btnEncode;
        private MetroFramework.Controls.MetroTile btnSave;
        private MetroFramework.Controls.MetroTile btnDecode;
    }
}

