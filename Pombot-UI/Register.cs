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
    public partial class Register : Form
    {
        private bool hiddenPass = true;

        public Register()
        {
            InitializeComponent();
        }

        private void RegisterUserBT_Click(object sender, EventArgs e)
        {
            PomBot login = new PomBot();
            login.Show();
            this.Hide();
        }

        private void nameTB_Click(object sender, EventArgs e)
        {
            if (nameTB.Text == "full name") nameTB.Clear();
            nameTB.SelectAll();
            nameTB.Focus();
            namePic.BackgroundImage = Properties.Resources.userName2;
            nameLn.BackColor = Color.White;
            nameTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void userTB_Click(object sender, EventArgs e)
        {
            if (userTB.Text == "username") userTB.Clear();
            userTB.SelectAll();
            userTB.Focus();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            userLn.BackColor = Color.White;
            userTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void mailTB_Click(object sender, EventArgs e)
        {
            if (mailTB.Text == "e-mail") mailTB.Clear();
            mailTB.SelectAll();
            mailTB.Focus();
            mailPic.BackgroundImage = Properties.Resources.userEmail2;
            mailLn.BackColor = Color.White;
            mailTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void passwordTB_Click(object sender, EventArgs e)
        {
            if (passwordTB.Text == "password") passwordTB.Clear();
            if (hiddenPass) passwordTB.UseSystemPasswordChar = true;
            passwordTB.SelectAll();
            passwordTB.Focus();
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            passwordLn.BackColor = Color.White;
            passwordTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
        }

        private void nameTB_Enter(object sender, EventArgs e)
        {
            if (nameTB.Text == "full name") nameTB.Clear();
            nameTB.SelectAll();
            nameTB.Focus();
            namePic.BackgroundImage = Properties.Resources.userName2;
            nameLn.BackColor = Color.White;
            nameTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void userTB_Enter(object sender, EventArgs e)
        {
            if (userTB.Text == "username") userTB.Clear();
            userTB.SelectAll();
            userTB.Focus();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            userLn.BackColor = Color.White;
            userTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void mailTB_Enter(object sender, EventArgs e)
        {
            if (mailTB.Text == "e-mail") mailTB.Clear();
            mailTB.SelectAll();
            mailTB.Focus();
            mailPic.BackgroundImage = Properties.Resources.userEmail2;
            mailLn.BackColor = Color.White;
            mailTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
            passPic.BackgroundImage = Properties.Resources.userPassword;
            passwordLn.BackColor = Color.Black;
            passwordTB.ForeColor = Color.Black;
        }

        private void passwordTB_Enter(object sender, EventArgs e)
        {
            if (passwordTB.Text == "password") passwordTB.Clear();
            if (hiddenPass) passwordTB.UseSystemPasswordChar = true;
            passwordTB.SelectAll();
            passwordTB.Focus();
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            passwordLn.BackColor = Color.White;
            passwordTB.ForeColor = Color.White;

            mailPic.BackgroundImage = Properties.Resources.userEmail;
            mailLn.BackColor = Color.Black;
            mailTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            userLn.BackColor = Color.Black;
            userTB.ForeColor = Color.Black;
            namePic.BackgroundImage = Properties.Resources.userName;
            nameLn.BackColor = Color.Black;
            nameTB.ForeColor = Color.Black;
        }

        private void closeBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void passwordShowBT_Click(object sender, EventArgs e)
        {
            if (hiddenPass)
            {
                passwordTB.UseSystemPasswordChar = false;
                passwordShowBT.BackgroundImage = Properties.Resources.passwordShow2;
                hiddenPass = false;
            }
            else
            {
                passwordTB.UseSystemPasswordChar = true;
                passwordShowBT.BackgroundImage = Properties.Resources.passwordShow;
                hiddenPass = true;
            }
        }

        private void passwordTB_Leave(object sender, EventArgs e)
        {
            if (passwordTB.Text == "")
            {
                passwordTB.Text = "password";
                passwordTB.UseSystemPasswordChar = false;
            }
            passwordTB.DeselectAll();
        }

        private void nameTB_Leave(object sender, EventArgs e)
        {
            if (nameTB.Text == "") nameTB.Text = "full name";
            nameTB.DeselectAll();
        }

        private void userTB_Leave(object sender, EventArgs e)
        {
            if (userTB.Text == "") userTB.Text = "username";
            userTB.DeselectAll();
        }

        private void mailTB_Leave(object sender, EventArgs e)
        {
            if (mailTB.Text == "") mailTB.Text = "e-mail";
            mailTB.DeselectAll();
        }
    }
}
