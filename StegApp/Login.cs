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
