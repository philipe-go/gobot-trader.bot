
namespace Pombot_UI
{
    partial class PomBot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserNameTB = new System.Windows.Forms.TextBox();
            this.UserNameLn = new System.Windows.Forms.Panel();
            this.PasswordLn = new System.Windows.Forms.Panel();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.SignInBT = new System.Windows.Forms.Button();
            this.RegisterBT = new System.Windows.Forms.Button();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.formNamePN = new System.Windows.Forms.Panel();
            this.winNameLB = new System.Windows.Forms.Label();
            this.closeBT = new System.Windows.Forms.Button();
            this.passwordShowBT = new System.Windows.Forms.Button();
            this.passPic = new System.Windows.Forms.PictureBox();
            this.userPic = new System.Windows.Forms.PictureBox();
            this.PomBotIcon = new System.Windows.Forms.PictureBox();
            this.wrongLB = new System.Windows.Forms.Label();
            this.formNamePN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PomBotIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // UserNameTB
            // 
            this.UserNameTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.UserNameTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserNameTB.Font = new System.Drawing.Font("Earth 2073", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameTB.HideSelection = false;
            this.UserNameTB.Location = new System.Drawing.Point(81, 190);
            this.UserNameTB.Name = "UserNameTB";
            this.UserNameTB.Size = new System.Drawing.Size(230, 19);
            this.UserNameTB.TabIndex = 1;
            this.UserNameTB.Tag = "2";
            this.UserNameTB.Text = "username";
            this.UserNameTB.Click += new System.EventHandler(this.UserNameTB_Click);
            this.UserNameTB.Enter += new System.EventHandler(this.UserNameTB_Enter);
            this.UserNameTB.Leave += new System.EventHandler(this.UserNameTB_Leave);
            // 
            // UserNameLn
            // 
            this.UserNameLn.BackColor = System.Drawing.Color.Black;
            this.UserNameLn.ForeColor = System.Drawing.Color.White;
            this.UserNameLn.Location = new System.Drawing.Point(38, 216);
            this.UserNameLn.Name = "UserNameLn";
            this.UserNameLn.Size = new System.Drawing.Size(273, 1);
            this.UserNameLn.TabIndex = 3;
            // 
            // PasswordLn
            // 
            this.PasswordLn.BackColor = System.Drawing.Color.Black;
            this.PasswordLn.ForeColor = System.Drawing.Color.White;
            this.PasswordLn.Location = new System.Drawing.Point(38, 268);
            this.PasswordLn.Name = "PasswordLn";
            this.PasswordLn.Size = new System.Drawing.Size(273, 1);
            this.PasswordLn.TabIndex = 6;
            // 
            // PasswordTB
            // 
            this.PasswordTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PasswordTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordTB.Font = new System.Drawing.Font("Earth 2073", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTB.HideSelection = false;
            this.PasswordTB.Location = new System.Drawing.Point(81, 242);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.Size = new System.Drawing.Size(204, 19);
            this.PasswordTB.TabIndex = 2;
            this.PasswordTB.Tag = "3";
            this.PasswordTB.Text = "password";
            this.PasswordTB.Click += new System.EventHandler(this.PasswordTB_Click);
            this.PasswordTB.Enter += new System.EventHandler(this.PasswordTB_Enter);
            this.PasswordTB.Leave += new System.EventHandler(this.PasswordTB_Leave);
            // 
            // SignInBT
            // 
            this.SignInBT.BackColor = System.Drawing.Color.Black;
            this.SignInBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SignInBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SignInBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.SignInBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignInBT.Font = new System.Drawing.Font("Earth 2073", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInBT.ForeColor = System.Drawing.Color.White;
            this.SignInBT.Location = new System.Drawing.Point(88, 303);
            this.SignInBT.Name = "SignInBT";
            this.SignInBT.Size = new System.Drawing.Size(166, 37);
            this.SignInBT.TabIndex = 3;
            this.SignInBT.Text = "sign in";
            this.SignInBT.UseVisualStyleBackColor = false;
            this.SignInBT.Click += new System.EventHandler(this.SignInBT_Click);
            this.SignInBT.Enter += new System.EventHandler(this.SignInBT_Enter);
            // 
            // RegisterBT
            // 
            this.RegisterBT.BackColor = System.Drawing.Color.Transparent;
            this.RegisterBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegisterBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RegisterBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.RegisterBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterBT.Font = new System.Drawing.Font("Earth 2073", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBT.ForeColor = System.Drawing.Color.White;
            this.RegisterBT.Location = new System.Drawing.Point(88, 358);
            this.RegisterBT.Name = "RegisterBT";
            this.RegisterBT.Size = new System.Drawing.Size(166, 28);
            this.RegisterBT.TabIndex = 4;
            this.RegisterBT.Text = "register";
            this.RegisterBT.UseVisualStyleBackColor = false;
            this.RegisterBT.Click += new System.EventHandler(this.RegisterBT_Click);
            // 
            // nameApp
            // 
            this.nameApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Earth 2073", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameApp.Location = new System.Drawing.Point(263, 412);
            this.nameApp.Name = "nameApp";
            this.nameApp.Size = new System.Drawing.Size(81, 13);
            this.nameApp.TabIndex = 100;
            this.nameApp.TabStop = false;
            this.nameApp.Tag = "1";
            this.nameApp.Text = "PomBot v1.0";
            // 
            // formNamePN
            // 
            this.formNamePN.BackColor = System.Drawing.Color.Gray;
            this.formNamePN.Controls.Add(this.winNameLB);
            this.formNamePN.Controls.Add(this.closeBT);
            this.formNamePN.Dock = System.Windows.Forms.DockStyle.Top;
            this.formNamePN.Location = new System.Drawing.Point(0, 0);
            this.formNamePN.Name = "formNamePN";
            this.formNamePN.Size = new System.Drawing.Size(348, 26);
            this.formNamePN.TabIndex = 104;
            // 
            // winNameLB
            // 
            this.winNameLB.AutoSize = true;
            this.winNameLB.Font = new System.Drawing.Font("Earth 2073", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winNameLB.Location = new System.Drawing.Point(13, 5);
            this.winNameLB.Name = "winNameLB";
            this.winNameLB.Size = new System.Drawing.Size(50, 14);
            this.winNameLB.TabIndex = 102;
            this.winNameLB.Text = "LOGIN";
            // 
            // closeBT
            // 
            this.closeBT.BackColor = System.Drawing.Color.Transparent;
            this.closeBT.BackgroundImage = global::Pombot_UI.Properties.Resources.close;
            this.closeBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeBT.FlatAppearance.BorderSize = 0;
            this.closeBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBT.Font = new System.Drawing.Font("Earth 2073", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBT.ForeColor = System.Drawing.Color.White;
            this.closeBT.Location = new System.Drawing.Point(324, 3);
            this.closeBT.Name = "closeBT";
            this.closeBT.Size = new System.Drawing.Size(20, 19);
            this.closeBT.TabIndex = 101;
            this.closeBT.TabStop = false;
            this.closeBT.UseVisualStyleBackColor = false;
            this.closeBT.Click += new System.EventHandler(this.closeBT_Click);
            this.closeBT.Enter += new System.EventHandler(this.closeBT_Enter);
            this.closeBT.Leave += new System.EventHandler(this.closeBT_Leave);
            this.closeBT.MouseLeave += new System.EventHandler(this.closeBT_Leave);
            this.closeBT.MouseHover += new System.EventHandler(this.closeBT_Enter);
            // 
            // passwordShowBT
            // 
            this.passwordShowBT.BackColor = System.Drawing.Color.Transparent;
            this.passwordShowBT.BackgroundImage = global::Pombot_UI.Properties.Resources.passwordShow;
            this.passwordShowBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.passwordShowBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.passwordShowBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.passwordShowBT.FlatAppearance.BorderSize = 0;
            this.passwordShowBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.passwordShowBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.passwordShowBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passwordShowBT.Font = new System.Drawing.Font("Earth 2073", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordShowBT.ForeColor = System.Drawing.Color.White;
            this.passwordShowBT.Location = new System.Drawing.Point(291, 238);
            this.passwordShowBT.Name = "passwordShowBT";
            this.passwordShowBT.Size = new System.Drawing.Size(20, 19);
            this.passwordShowBT.TabIndex = 102;
            this.passwordShowBT.TabStop = false;
            this.passwordShowBT.UseVisualStyleBackColor = false;
            this.passwordShowBT.Click += new System.EventHandler(this.passwordShowBT_Click);
            // 
            // passPic
            // 
            this.passPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userPassword;
            this.passPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.passPic.Location = new System.Drawing.Point(38, 230);
            this.passPic.Name = "passPic";
            this.passPic.Size = new System.Drawing.Size(34, 34);
            this.passPic.TabIndex = 5;
            this.passPic.TabStop = false;
            // 
            // userPic
            // 
            this.userPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userLogin;
            this.userPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userPic.Location = new System.Drawing.Point(38, 179);
            this.userPic.Name = "userPic";
            this.userPic.Size = new System.Drawing.Size(34, 34);
            this.userPic.TabIndex = 1;
            this.userPic.TabStop = false;
            // 
            // PomBotIcon
            // 
            this.PomBotIcon.BackgroundImage = global::Pombot_UI.Properties.Resources.bird_icon;
            this.PomBotIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PomBotIcon.Location = new System.Drawing.Point(90, 46);
            this.PomBotIcon.Name = "PomBotIcon";
            this.PomBotIcon.Size = new System.Drawing.Size(166, 114);
            this.PomBotIcon.TabIndex = 0;
            this.PomBotIcon.TabStop = false;
            this.PomBotIcon.Tag = "1";
            // 
            // wrongLB
            // 
            this.wrongLB.AutoSize = true;
            this.wrongLB.Font = new System.Drawing.Font("Earth 2073", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wrongLB.ForeColor = System.Drawing.Color.Red;
            this.wrongLB.Location = new System.Drawing.Point(82, 281);
            this.wrongLB.Name = "wrongLB";
            this.wrongLB.Size = new System.Drawing.Size(178, 12);
            this.wrongLB.TabIndex = 105;
            this.wrongLB.Text = "WRONG USER OR PASSWORD";
            // 
            // PomBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(348, 435);
            this.Controls.Add(this.wrongLB);
            this.Controls.Add(this.formNamePN);
            this.Controls.Add(this.passwordShowBT);
            this.Controls.Add(this.nameApp);
            this.Controls.Add(this.RegisterBT);
            this.Controls.Add(this.SignInBT);
            this.Controls.Add(this.PasswordLn);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.UserNameLn);
            this.Controls.Add(this.passPic);
            this.Controls.Add(this.UserNameTB);
            this.Controls.Add(this.userPic);
            this.Controls.Add(this.PomBotIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PomBot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PomBot-Login";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.SlateGray;
            this.formNamePN.ResumeLayout(false);
            this.formNamePN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.passPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PomBotIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PomBotIcon;
        private System.Windows.Forms.PictureBox userPic;
        private System.Windows.Forms.Panel UserNameLn;
        private System.Windows.Forms.Panel PasswordLn;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.PictureBox passPic;
        private System.Windows.Forms.Button SignInBT;
        private System.Windows.Forms.Button RegisterBT;
        private System.Windows.Forms.TextBox nameApp;
        private System.Windows.Forms.Button closeBT;
        private System.Windows.Forms.Button passwordShowBT;
        private System.Windows.Forms.Panel formNamePN;
        private System.Windows.Forms.Label winNameLB;
        private System.Windows.Forms.Label wrongLB;
        internal System.Windows.Forms.TextBox UserNameTB;
    }
}

