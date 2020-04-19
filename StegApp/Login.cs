using System;
using System.Windows.Forms;
using System.Threading;

namespace StegApp
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        Database auth;
        public Login()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(500);
            InitializeComponent();
            t.Abort();
        }

        public void StartForm()
        {
            Application.Run(new Splash());
        }
        private void Login_Load(object sender, EventArgs e)
        {
            this.Activate();
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty
               && txtPassword.Text != string.Empty) //if the textboxes are not empty
            {
                string password = txtPassword.Text; //store username
                string username = txtUsername.Text; //store password
                auth = new Database(); //call database class 
                auth.CreateDatabase(); //call create database function
                auth.GetConnection(); //call get connection function

                bool success = auth.LogIn(username, password); //call login function

                if (success == true) // if the login is successfull
                {
                    this.Hide();
                    Main mainForm = new Main(); //open the main form
                    mainForm.ShowDialog();
                }
                else if (success == false) //log in failed so give error message
                {
                    MessageBox.Show("Incorrect username or password.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (txtPassword.Text.Length < 5)
            {
                MessageBox.Show("Password is too short.", "Warning");
            }
            else
            {
                MessageBox.Show("Filed/s Empty!", "Warning"); // when fields are empty
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

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.Style = MetroFramework.MetroColorStyle.Green;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.Style = MetroFramework.MetroColorStyle.Default;
        }
    }
}
