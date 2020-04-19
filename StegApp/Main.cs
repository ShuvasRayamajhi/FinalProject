using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace StegApp
{
    public partial class  Main : MetroFramework.Forms.MetroForm
    {
        private Bitmap bitmp = null;
        public Main()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e) 
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); //get image
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp"; //the accepted file types

            if (open_dialog.ShowDialog() == DialogResult.OK)
                picBox.Image = Image.FromFile(open_dialog.FileName); //display the image on the picture box
        }

        private void btnSave_Click(object sender, EventArgs e) //save image
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FilterIndex == 0)
                    bitmp.Save(saveFile.FileName, ImageFormat.Png); //png

                else if (saveFile.FilterIndex == 1)
                    bitmp.Save(saveFile.FileName, ImageFormat.Bmp);//bmp
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtPassword.Enabled = false;
        }

        private void togEncrypt_CheckedChanged(object sender, EventArgs e) //password button is disabled unless toggle is switched on
        {
            if (togEncrypt.Checked == false)
                txtPassword.Enabled = false;
            if (togEncrypt.Checked == true)
                txtPassword.Enabled = true;
        }
        private void btnEncode_MouseEnter(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnEncode_MouseLeave(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void btnEncode_Click(object sender, EventArgs e)//when the encode button is clicked
        {
            bitmp = (Bitmap)picBox.Image; //assign a variable to the image

            string encodeText = txtMessage.Text; //assign variable to the input message

            if (encodeText == null)
                MessageBox.Show("No text entered!", "Warning"); //empty check for the text message

            if (togEncrypt.Checked) //check the toggle switch
            {
                if (txtPassword.Text.Length < 5) //check that password is at least of length 5
                    MessageBox.Show("Password has to be 5 characters!", "Warning");
                
                else if (txtPassword.Text.Length >= 5)
                    encodeText = Cryptography.Encryption(encodeText, txtPassword.Text); //encryption
            }
            if (bitmp != null)
            {
                bitmp = StegEncode.Encoding(encodeText, bitmp); //call steganography function
                MessageBox.Show("Success!", "Done");

                SaveFileDialog saveFile = new SaveFileDialog(); //save image
                saveFile.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

                if (saveFile.ShowDialog() == DialogResult.OK) 
                {
                    if (saveFile.FilterIndex == 0)
                        bitmp.Save(saveFile.FileName, ImageFormat.Png); //png

                   
                    else if (saveFile.FilterIndex == 1)
                        bitmp.Save(saveFile.FileName, ImageFormat.Bmp);//bmp
                }
            }
            else
            {
                MessageBox.Show("No Image Selected!", "Warning");
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            bitmp = (Bitmap)picBox.Image;
            if (bitmp != null )
            {
                string decodeText = StegDecode.StegDecoding(bitmp); //steganography
                
                if (togEncrypt.Checked)
                {
                    if (txtPassword.TextLength < 5)
                    {
                        MessageBox.Show("Password has to be at least 5 characters!", "Error");
                    }
                    else if (txtPassword == null)
                    {
                        MessageBox.Show("No password entered!", "Error");
                    }

                    else if (togEncrypt.Checked == false)
                    {
                        MessageBox.Show("Turn on encryption!", "Error");
                    }
                    else
                    {
                        decodeText = Cryptography.Decryption(decodeText, txtPassword.Text); //decryption
                    }
                }

                if (decodeText != " ")
                    txtMessage.Text = decodeText;

                else
                    MessageBox.Show("Non text to decode!", "Error");
                
            }
            else
                MessageBox.Show("No Image Selected!", "Warning");
        }

        private void btnOpen_MouseEnter(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnOpen_MouseLeave(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void btnEncode_MouseEnter_1(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnEncode_MouseLeave_1(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void btnDecode_MouseEnter(object sender, EventArgs e)
        {
            btnDecode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnDecode_MouseLeave(object sender, EventArgs e)
        {
            btnDecode.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.Style = MetroFramework.MetroColorStyle.Default;
        }
    }
}
