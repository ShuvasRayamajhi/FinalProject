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
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Activate();
        }
        private void btnLogIn_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnLogIn_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.Style = MetroFramework.MetroColorStyle.Default;
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
