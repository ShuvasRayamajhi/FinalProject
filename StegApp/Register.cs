using System;
using System.Windows.Forms;

namespace StegApp
{
    public partial class Register : MetroFramework.Forms.MetroForm
    {
        Database auth;
        public Register()
        {
            InitializeComponent();
        }
     
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty
                && txtConfirm.Text != string.Empty)
            {
                if (txtPassword.Text == txtConfirm.Text)
                {
                    string password = txtPassword.Text;
                    string username = txtUsername.Text;
                    auth = new Database();
                    auth.CreateDatabase();
                    auth.GetConnection();

                    bool exist = auth.VerifyUser(username, password);  //does the account exist already, call function from database class

                    if (exist == true)
                    {
                        MessageBox.Show("User already exists.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else if (exist == false)
                    {
                        auth.CreateAccount(username, password);
                        MessageBox.Show("User created", "Sucess!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field/s!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
