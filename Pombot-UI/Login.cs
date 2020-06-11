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
        public PomBot()
        {
            InitializeComponent();
        }

        Register registerForm = new Register();

        private void RegisterBT_Click(object sender, EventArgs e)
        {
            registerForm.Show();
            this.Hide();
        }

        private void PasswordTB_Click(object sender, EventArgs e)
        {
            PasswordTB.Clear();
            PasswordTB.PasswordChar = '*';
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            PasswordLn.BackColor = Color.White;
            PasswordTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;
        }

        private void UserNameTB_Click(object sender, EventArgs e)
        {
            UserNameTB.Clear();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            UserNameLn.BackColor = Color.White;
            UserNameTB.ForeColor = Color.White;

            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;
        }

        private void SignInBT_Enter(object sender, EventArgs e)
        {
            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;
            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;

        }

        private void UserNameTB_Enter(object sender, EventArgs e)
        {
            UserNameTB.Clear();
            userPic.BackgroundImage = Properties.Resources.userLogin2;
            UserNameLn.BackColor = Color.White;
            UserNameTB.ForeColor = Color.White;

            passPic.BackgroundImage = Properties.Resources.userPassword;
            PasswordLn.BackColor = Color.Black;
            PasswordTB.ForeColor = Color.Black;
        }

        private void PasswordTB_Enter(object sender, EventArgs e)
        {
            PasswordTB.Clear();
            PasswordTB.PasswordChar = '*';
            passPic.BackgroundImage = Properties.Resources.userPassword2;
            PasswordLn.BackColor = Color.White;
            PasswordTB.ForeColor = Color.White;

            userPic.BackgroundImage = Properties.Resources.userLogin;
            UserNameLn.BackColor = Color.Black;
            UserNameTB.ForeColor = Color.Black;
        }

        private void closeBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeBT_Enter(object sender, EventArgs e)
        {
            closeBT.BackgroundImage = Properties.Resources.close2;
        }

        private void closeBT_Leave(object sender, EventArgs e)
        {
            closeBT.BackgroundImage = Properties.Resources.close;
        }
    }
}
