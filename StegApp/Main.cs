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

        private void btnOpen_Click(object sender, EventArgs e) //load an image to be encoded
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                picBox.Image = Image.FromFile(open_dialog.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e) //save image
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                switch (saveFile.FilterIndex)
                {
                    case 0:
                        {
                            bitmp.Save(saveFile.FileName, ImageFormat.Png);
                        }
                        break;
                    case 1:
                        {
                            bitmp.Save(saveFile.FileName, ImageFormat.Bmp);
                        }
                        break;
                }
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

        private void btnOpen_MouseEnter(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnOpen_MouseLeave(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void btnDecode_MouseEnter_1(object sender, EventArgs e)
        {
            btnDecode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnDecode_MouseLeave_1(object sender, EventArgs e)
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

        private void btnEncode_Click(object sender, EventArgs e)//encode message into image
        {
            bitmp = (Bitmap)picBox.Image;

            string encodeText = txtMessage.Text;

            if (encodeText == null)
                MessageBox.Show("No Text Entered!", "Warning");

            if (togEncrypt.Checked)
            {
                if (txtMessage.Text.Length < 1)
                    MessageBox.Show("No Text Entered!", "Warning");
                if (txtPassword.Text.Length < 1)
                    MessageBox.Show("No Password Entered!", "Warning");
            }
            if (bitmp != null)
            {
                bitmp = StegEncode.Encoding(encodeText, bitmp); //steganography
                MessageBox.Show("Success!", "Done");

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    switch (saveFile.FilterIndex)
                    {
                        case 0:
                            {
                                bitmp.Save(saveFile.FileName, ImageFormat.Png);
                            }
                            break;
                        case 1:
                            {
                                bitmp.Save(saveFile.FileName, ImageFormat.Bmp);
                            }
                            break;
                    }
                }

            }
            else
            {
                MessageBox.Show("No Image Selected!", "Warning");
            }
        }
    }
}
