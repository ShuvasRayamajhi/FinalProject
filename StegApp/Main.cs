using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegApp
{
    public partial class  Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                picBox.Image = Image.FromFile(open_dialog.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtPassword.Enabled = false;
        }

        private void togEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (togEncrypt.Checked == false)
                txtPassword.Enabled = false;
            if (togEncrypt.Checked == true)
                txtPassword.Enabled = true;
        }
        private void tileEncode_MouseEnter(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void tileEncode_MouseLeave(object sender, EventArgs e)
        {
            btnEncode.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void tileOpenFile_MouseEnter(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void tileOpenFile_MouseLeave(object sender, EventArgs e)
        {
            btnOpen.Style = MetroFramework.MetroColorStyle.Default;
        }

        private void tileDecode_MouseEnter_1(object sender, EventArgs e)
        {
            btnDecode.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void tileDecode_MouseLeave_1(object sender, EventArgs e)
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
