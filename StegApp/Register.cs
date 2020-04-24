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
            if (txtUsername.Text != string.Empty //check empty input
                && txtPassword.Text != string.Empty
                && txtConfirm.Text != string.Empty)
            {
                if (txtPassword.Text == txtConfirm.Text)
                {
                    if (txtPassword.TextLength >= 5)
                    {
                        string password = txtPassword.Text; //variable
                        string username = txtUsername.Text;
                        auth = new Database(); //new database
                        auth.CreateDatabase(); //create database if not created already
                        auth.GetConnection();

                        bool exist = auth.VerifyUser(username);  //does the account exist already, call function from database class

                        if (exist == true) //account exists
                        {
                            MessageBox.Show("User already exists.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error); //give error message
                        }

                        else if (exist == false) //account does not exist
                        {
                            auth.CreateAccount(username, password); //call function to create account
                            MessageBox.Show("User created", "Sucess!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password too short.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error); //give error message
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
