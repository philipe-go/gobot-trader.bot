using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI
{
    public partial class PomBot : Form
    {
        private bool hiddenPass = true;

        public PomBot()
        {
            InitializeComponent();
        }


        private void RegisterBT_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }


        private void SignInBT_Click(object sender, EventArgs e)
        {
            if (UserNameTB.Text == "philipeng" && PasswordTB.Text == "12345")
            {
                PomBotApp mainAppForm = new PomBotApp();
                mainAppForm.Show();
                this.Hide();
            }
            else
            {
                wrongTB.Visible = true;
            }
        }

        private void passwordShowBT_Click(object sender, EventArgs e)
        {
            if (hiddenPass)
            {
                PasswordTB.UseSystemPasswordChar = false;
                passwordShowBT.BackgroundImage = Properties.Resources.passwordShow2;
                hiddenPass = false;
            }
            else
            {
                PasswordTB.UseSystemPasswordChar = true;
                passwordShowBT.BackgroundImage = Properties.Resources.passwordShow;
                hiddenPass = true;
            }
        }

        private void closeBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region Fields Behaviours
        //UserName TextBox
        private void UserNameTB_Click(object sender, EventArgs e)
        {
            if (UserNameTB.Text == "username") UserNameTB.Clear();
            UserNameTB.SelectAll();
            UserNameTB.Focus();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            UserNameLn.BackColor = Color.White;
            UserNameTB.ForeColor = Color.White;

            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;

            wrongTB.Visible = false;
        }
        private void UserNameTB_Enter(object sender, EventArgs e)
        {
            if (UserNameTB.Text == "username") UserNameTB.Clear();
            UserNameTB.SelectAll();
            UserNameTB.Focus();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            UserNameLn.BackColor = Color.White;
            UserNameTB.ForeColor = Color.White;

            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;

            wrongTB.Visible = false;
        }
        private void UserNameTB_Leave(object sender, EventArgs e)
        {
            if (UserNameTB.Text == "") UserNameTB.Text = "username";
            UserNameTB.DeselectAll();
        }
        
        //Password TextBox
        private void PasswordTB_Click(object sender, EventArgs e)
        {
            if (PasswordTB.Text == "password") PasswordTB.Clear();
            if (hiddenPass) PasswordTB.UseSystemPasswordChar = true;
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            PasswordTB.SelectAll();
            PasswordTB.Focus();
            PasswordLn.BackColor = Color.White;
            PasswordTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;

            wrongTB.Visible = false;
        }
        private void PasswordTB_Enter(object sender, EventArgs e)
        {
            if (PasswordTB.Text == "password") PasswordTB.Clear();
            if (hiddenPass) PasswordTB.UseSystemPasswordChar = true;
            PasswordTB.SelectAll();
            PasswordTB.Focus();
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            PasswordLn.BackColor = Color.White;
            PasswordTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;

            wrongTB.Visible = false;
        }
        private void PasswordTB_Leave(object sender, EventArgs e)
        {
            if (PasswordTB.Text == "")
            {
                PasswordTB.Text = "password";
                PasswordTB.UseSystemPasswordChar = false;
            }
            PasswordTB.DeselectAll();
        }

        //Buttons
        private void SignInBT_Enter(object sender, EventArgs e)
        {
            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;

        }

        private void closeBT_Enter(object sender, EventArgs e)
        {
            closeBT.BackgroundImage = Properties.Resources.close2;
        }

        private void closeBT_Leave(object sender, EventArgs e)
        {
            closeBT.BackgroundImage = Properties.Resources.close;
        }
        #endregion
    }
}
