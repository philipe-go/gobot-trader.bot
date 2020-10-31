using Pombot_UI.RobotLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI
{
    public partial class Register : Form
    {
        private bool hiddenPass = true;
        private int mov, movY, movX;

        public Register()
        {
            InitializeComponent();
            nameApp.Text = Program.appName + " - V" + Program.appVersion.Major + "." + Program.appVersion.Minor; //Retrieve app name from Program.cs and set into UI;
            Pombos.registerForm = this;
            feedBackLB.Visible = false;

            #region Set native Font
            PomBot.LoadFont(this);
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            feedBackLB.Text = "";
            feedBackLB.Visible = false;
        }

        private void RegisterUserBT_Click(object sender, EventArgs e)
        {
            if (userTB.Text == "username" || passwordTB.Text == "password")
            {
                FeedbackTxt("Wrong Data", Color.Red);
            }
            else
            {
                Pombos.AddUser();
                timer1.Start();
                timer1.Interval = 2000;
            }
        }
        private void closeBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void backBT_Click(object sender, EventArgs e)
        {
            PomBot login = new PomBot();
            login.Show();
            this.Close();
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

        private void nameTB_Click(object sender, EventArgs e)
        {
            if (nameTB.Text == "full name") nameTB.Clear();
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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
            feedBackLB.Visible = false;
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

        internal void FeedbackTxt(string txt, Color col)
        {
            feedBackLB.Visible = true;
            feedBackLB.ForeColor = col;
            feedBackLB.Text = txt;
        }

        #region Move Window
        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movY = e.Y;
            movX = e.X;
            this.Opacity = 0.5f;
        }
        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        private void Register_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
            this.Opacity = 1;
        }
        #endregion
    }
}
