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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.message = new MetroFramework.Controls.MetroLabel();
            this.togEncrypt = new MetroFramework.Controls.MetroToggle();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(23, 63);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(447, 255);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
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
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(395, 398);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(75, 38);
            this.btnEncode.TabIndex = 2;
            this.btnEncode.Text = "Encode";
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(395, 509);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(395, 451);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 38);
            this.btnDecode.TabIndex = 3;
            this.btnDecode.Text = "Decode";
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(23, 340);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(352, 96);
            this.txtMessage.TabIndex = 5;
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(23, 321);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(63, 19);
            this.message.TabIndex = 6;
            this.message.Text = "Message:";
            // 
            // togEncrypt
            // 
            this.togEncrypt.AutoSize = true;
            this.togEncrypt.Location = new System.Drawing.Point(23, 495);
            this.togEncrypt.Name = "togEncrypt";
            this.togEncrypt.Size = new System.Drawing.Size(80, 17);
            this.togEncrypt.TabIndex = 7;
            this.togEncrypt.Text = "Off";
            this.togEncrypt.UseVisualStyleBackColor = true;
            this.togEncrypt.CheckedChanged += new System.EventHandler(this.togEncrypt_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(133, 495);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(153, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(133, 473);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(66, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Password:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 470);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(74, 19);
            this.metroLabel2.TabIndex = 10;
            this.metroLabel2.Text = "Encryption:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 576);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.togEncrypt);
            this.Controls.Add(this.message);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.picBox);
            this.Name = "Main";
            this.Text = "StegTool";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private MetroFramework.Controls.MetroTile btnOpen;
        private MetroFramework.Controls.MetroTile btnEncode;
        private MetroFramework.Controls.MetroTile btnSave;
        private MetroFramework.Controls.MetroTile btnDecode;
        private System.Windows.Forms.TextBox txtMessage;
        private MetroFramework.Controls.MetroLabel message;
        private MetroFramework.Controls.MetroToggle togEncrypt;
        private System.Windows.Forms.TextBox txtPassword;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}

