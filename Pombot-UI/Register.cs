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
        public Register()
        {
            InitializeComponent();
        }


        private void RegisterUserBT_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit(); //BE REMOVED
        }

        private void nameTB_Click(object sender, EventArgs e)
        {
            nameTB.Clear();
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
            userTB.Clear();
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
            mailTB.Clear();
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
            passwordTB.Clear();
            passwordTB.PasswordChar = '*';
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
            nameTB.Clear();
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
            userTB.Clear();
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
            mailTB.Clear();
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
            passwordTB.Clear();
            passwordTB.PasswordChar = '*';
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
    }
}
