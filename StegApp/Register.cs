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
    public partial class Register : MetroFramework.Forms.MetroForm
    {
        public Register()
        {
            InitializeComponent();
        }
        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            btnRegister.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnRegister_MouseLeave(object sender, EventArgs e)
        {
            btnRegister.Style = MetroFramework.MetroColorStyle.Default;
        }
    }
}
