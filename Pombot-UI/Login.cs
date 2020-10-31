using Pombot_UI.RobotLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI
{
    public partial class PomBot : Form
    {
        private bool hiddenPass = true;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private static PrivateFontCollection fonts = new PrivateFontCollection();

        public PomBot()
        {
            InitializeComponent();
            nameApp.Text = Program.appName + " - V" + Program.appVersion.Major + "." + Program.appVersion.Minor; //Retrieve app name from Program.cs and set into UI;
            Pombos.loginForm = this;
            wrongLB.Visible = false;
            LoadFont(this);
        }

        internal static void LoadFont(Form sender)
        {
            byte[] fontData = Properties.Resources.Earth_2073;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Earth_2073.Length, IntPtr.Zero, ref dummy);
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Earth_2073.Length);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            foreach (Control ctr in sender.Controls)
            {
                if (ctr.HasChildren) ApplyFont(ctr);
                float size = ctr.Font.Size;
                Font font = new Font(fonts.Families[0], size);
                ctr.Font = font;
            }
        }
        internal static void ApplyFont(Control ctrl)
        {
            foreach (Control gb in ctrl.Controls)
            {
                if (gb.HasChildren) ApplyFont(gb);
                float size = gb.Font.Size;
                Font font = new Font(fonts.Families[0], size);
                gb.Font = font;
            }
        }

        private void RegisterBT_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }
        private void SignInBT_Click(object sender, EventArgs e)
        {
            SignInBT.Enabled = false;
            CheckUser();
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
        internal void WrongUser(string txt)
        {
            wrongLB.Invoke((MethodInvoker)delegate { wrongLB.Visible = true; });
            wrongLB.Invoke((MethodInvoker)delegate { wrongLB.Text = txt; });
            //wrongLB.Visible = true;
            //wrongLB.Text = txt;
        }
        private void CheckUser()
        {
            if (UserNameTB.Text != "username" && PasswordTB.Text != "password")
            {
                try
                {
                    Pombos.ConnectPombosDB();
                    if (Pombos.CheckUser(UserNameTB.Text, PasswordTB.Text))
                    {
                        PomBotAppForm mainAppForm = new PomBotAppForm();
                        mainAppForm.Show();
                        Program.userName = UserNameTB.Text;
                        this.Hide();
                        Pombos.DisconnectPombosDB();
                    }
                    else
                    {
                        WrongUser("WRONG USER OR PASSWORD");
                    }
                }
                catch (SqlException)
                {
                    WrongUser("404, NO INTERNET CONNECTION");
                }
            }
            else
            {
                WrongUser("WRONG USER OR PASSWORD");
            }
            SignInBT.Enabled = true;
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

            wrongLB.Visible = false;
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

            //wrongTB.Visible = false;
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

            wrongLB.Visible = false;
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

            //wrongTB.Visible = false;
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
