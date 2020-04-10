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
        Database auth;
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
             if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty)
            {
                string password = txtPassword.Text;
                string username = txtUsername.Text;
                auth = new Database();
                auth.CreateDatabase();
                auth.GetConnection();

                bool success = auth.LogIn(username, password);

                if (success == true)
                {
                    this.Hide();
                    Main mainForm = new Main();
                    mainForm.ShowDialog();
                }
                else if (success == false)
                {
                    MessageBox.Show("Incorrect username or password.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Filed/s Empty!", "Warning");
            }
        }
    }
}
