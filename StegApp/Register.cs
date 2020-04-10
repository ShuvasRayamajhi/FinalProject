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
        Database db;
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty
                && txtConfirm.Text != string.Empty)
            {
                if (txtPassword.Text == txtConfirm.Text)
                {
                    String password = txtPassword.Text;
                    String username = txtUsername.Text;
                    db = new Database();
                    db.CreateDatabase();
                    db.GetConnection();

                    Boolean exist = db.VerifyUser(username, password);  //does the account exist already, check!

                    if (exist == true)
                    {
                        MessageBox.Show("User already exists.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else if (exist == false)
                    {
                        
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
    }
}
