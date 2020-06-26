namespace Pombot_UI
{
    partial class Register
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
            this.components = new System.ComponentModel.Container();
            this.RegisterUserBT = new System.Windows.Forms.Button();
            this.nameApp = new System.Windows.Forms.TextBox();
            this.passwordLn = new System.Windows.Forms.Panel();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.mailLn = new System.Windows.Forms.Panel();
            this.mailTB = new System.Windows.Forms.TextBox();
            this.userLn = new System.Windows.Forms.Panel();
            this.userTB = new System.Windows.Forms.TextBox();
            this.nameLn = new System.Windows.Forms.Panel();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.namePic = new System.Windows.Forms.PictureBox();
            this.userPic = new System.Windows.Forms.PictureBox();
            this.passPic = new System.Windows.Forms.PictureBox();
            this.mailPic = new System.Windows.Forms.PictureBox();
            this.winNameLB = new System.Windows.Forms.Label();
            this.closeBT = new System.Windows.Forms.Button();
            this.formNamePN = new System.Windows.Forms.Panel();
            this.PomBotIcon = new System.Windows.Forms.PictureBox();
            this.passwordShowBT = new System.Windows.Forms.Button();
            this.feedBackLB = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.namePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailPic)).BeginInit();
            this.formNamePN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PomBotIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // RegisterUserBT
            // 
            this.RegisterUserBT.BackColor = System.Drawing.Color.Black;
            this.RegisterUserBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegisterUserBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RegisterUserBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.RegisterUserBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterUserBT.Font = new System.Drawing.Font("Earth 2073", 9.75F);
            this.RegisterUserBT.ForeColor = System.Drawing.Color.White;
            this.RegisterUserBT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RegisterUserBT.Location = new System.Drawing.Point(90, 425);
            this.RegisterUserBT.Name = "RegisterUserBT";
            this.RegisterUserBT.Size = new System.Drawing.Size(166, 37);
            this.RegisterUserBT.TabIndex = 5;
            this.RegisterUserBT.Text = "register user";
            this.RegisterUserBT.UseVisualStyleBackColor = false;
            this.RegisterUserBT.Click += new System.EventHandler(this.RegisterUserBT_Click);
            // 
            // nameApp
            // 
            this.nameApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameApp.Font = new System.Drawing.Font("Earth 2073", 8.25F);
            this.nameApp.Location = new System.Drawing.Point(255, 537);
            this.nameApp.Name = "nameApp";
            this.nameApp.Size = new System.Drawing.Size(81, 13);
            this.nameApp.TabIndex = 101;
            this.nameApp.TabStop = false;
            this.nameApp.Tag = "1";
            this.nameApp.Text = "PomBot v1.0";
            // 
            // passwordLn
            // 
            this.passwordLn.BackColor = System.Drawing.Color.Black;
            this.passwordLn.ForeColor = System.Drawing.Color.White;
            this.passwordLn.Location = new System.Drawing.Point(38, 381);
            this.passwordLn.Name = "passwordLn";
            this.passwordLn.Size = new System.Drawing.Size(273, 1);
            this.passwordLn.TabIndex = 107;
            // 
            // passwordTB
            // 
            this.passwordTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.passwordTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passwordTB.Font = new System.Drawing.Font("Earth 2073", 12F);
            this.passwordTB.HideSelection = false;
            this.passwordTB.Location = new System.Drawing.Point(81, 355);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(204, 19);
            this.passwordTB.TabIndex = 4;
            this.passwordTB.Tag = "3";
            this.passwordTB.Text = "password";
            this.passwordTB.Click += new System.EventHandler(this.passwordTB_Click);
            this.passwordTB.Enter += new System.EventHandler(this.passwordTB_Enter);
            this.passwordTB.Leave += new System.EventHandler(this.passwordTB_Leave);
            // 
            // mailLn
            // 
            this.mailLn.BackColor = System.Drawing.Color.Black;
            this.mailLn.ForeColor = System.Drawing.Color.White;
            this.mailLn.Location = new System.Drawing.Point(38, 329);
            this.mailLn.Name = "mailLn";
            this.mailLn.Size = new System.Drawing.Size(273, 1);
            this.mailLn.TabIndex = 105;
            // 
            // mailTB
            // 
            this.mailTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mailTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mailTB.Font = new System.Drawing.Font("Earth 2073", 12F);
            this.mailTB.HideSelection = false;
            this.mailTB.Location = new System.Drawing.Point(81, 303);
            this.mailTB.Name = "mailTB";
            this.mailTB.Size = new System.Drawing.Size(230, 19);
            this.mailTB.TabIndex = 3;
            this.mailTB.Tag = "2";
            this.mailTB.Text = "e-mail";
            this.mailTB.Click += new System.EventHandler(this.mailTB_Click);
            this.mailTB.Enter += new System.EventHandler(this.mailTB_Enter);
            this.mailTB.Leave += new System.EventHandler(this.mailTB_Leave);
            // 
            // userLn
            // 
            this.userLn.BackColor = System.Drawing.Color.Black;
            this.userLn.ForeColor = System.Drawing.Color.White;
            this.userLn.Location = new System.Drawing.Point(38, 279);
            this.userLn.Name = "userLn";
            this.userLn.Size = new System.Drawing.Size(273, 1);
            this.userLn.TabIndex = 110;
            // 
            // userTB
            // 
            this.userTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.userTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userTB.Font = new System.Drawing.Font("Earth 2073", 12F);
            this.userTB.HideSelection = false;
            this.userTB.Location = new System.Drawing.Point(81, 253);
            this.userTB.Name = "userTB";
            this.userTB.Size = new System.Drawing.Size(230, 19);
            this.userTB.TabIndex = 2;
            this.userTB.Tag = "2";
            this.userTB.Text = "username";
            this.userTB.Click += new System.EventHandler(this.userTB_Click);
            this.userTB.Enter += new System.EventHandler(this.userTB_Enter);
            this.userTB.Leave += new System.EventHandler(this.userTB_Leave);
            // 
            // nameLn
            // 
            this.nameLn.BackColor = System.Drawing.Color.Black;
            this.nameLn.ForeColor = System.Drawing.Color.White;
            this.nameLn.Location = new System.Drawing.Point(38, 230);
            this.nameLn.Name = "nameLn";
            this.nameLn.Size = new System.Drawing.Size(273, 1);
            this.nameLn.TabIndex = 113;
            // 
            // nameTB
            // 
            this.nameTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameTB.Font = new System.Drawing.Font("Earth 2073", 12F);
            this.nameTB.HideSelection = false;
            this.nameTB.Location = new System.Drawing.Point(81, 204);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(230, 19);
            this.nameTB.TabIndex = 1;
            this.nameTB.Tag = "2";
            this.nameTB.Text = "full name";
            this.nameTB.Click += new System.EventHandler(this.nameTB_Click);
            this.nameTB.Enter += new System.EventHandler(this.nameTB_Enter);
            this.nameTB.Leave += new System.EventHandler(this.nameTB_Leave);
            // 
            // namePic
            // 
            this.namePic.BackgroundImage = global::Pombot_UI.Properties.Resources.userName;
            this.namePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.namePic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.namePic.Location = new System.Drawing.Point(38, 193);
            this.namePic.Name = "namePic";
            this.namePic.Size = new System.Drawing.Size(34, 34);
            this.namePic.TabIndex = 112;
            this.namePic.TabStop = false;
            // 
            // userPic
            // 
            this.userPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userLogin;
            this.userPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userPic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.userPic.Location = new System.Drawing.Point(38, 242);
            this.userPic.Name = "userPic";
            this.userPic.Size = new System.Drawing.Size(34, 34);
            this.userPic.TabIndex = 109;
            this.userPic.TabStop = false;
            // 
            // passPic
            // 
            this.passPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userPassword;
            this.passPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.passPic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.passPic.Location = new System.Drawing.Point(38, 344);
            this.passPic.Name = "passPic";
            this.passPic.Size = new System.Drawing.Size(34, 34);
            this.passPic.TabIndex = 106;
            this.passPic.TabStop = false;
            // 
            // mailPic
            // 
            this.mailPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userEmail;
            this.mailPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mailPic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mailPic.Location = new System.Drawing.Point(38, 292);
            this.mailPic.Name = "mailPic";
            this.mailPic.Size = new System.Drawing.Size(34, 34);
            this.mailPic.TabIndex = 103;
            this.mailPic.TabStop = false;
            // 
            // winNameLB
            // 
            this.winNameLB.AutoSize = true;
            this.winNameLB.Font = new System.Drawing.Font("Earth 2073", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winNameLB.Location = new System.Drawing.Point(13, 6);
            this.winNameLB.Name = "winNameLB";
            this.winNameLB.Size = new System.Drawing.Size(76, 14);
            this.winNameLB.TabIndex = 102;
            this.winNameLB.Text = "REGISTER";
            // 
            // closeBT
            // 
            this.closeBT.BackColor = System.Drawing.Color.Transparent;
            this.closeBT.BackgroundImage = global::Pombot_UI.Properties.Resources.close;
            this.closeBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeBT.FlatAppearance.BorderSize = 0;
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
            this.formNamePN.TabIndex = 114;
            // 
            // PomBotIcon
            // 
            this.PomBotIcon.BackgroundImage = global::Pombot_UI.Properties.Resources.bird_icon;
            this.PomBotIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PomBotIcon.Location = new System.Drawing.Point(90, 46);
            this.PomBotIcon.Name = "PomBotIcon";
            this.PomBotIcon.Size = new System.Drawing.Size(166, 114);
            this.PomBotIcon.TabIndex = 103;
            this.PomBotIcon.TabStop = false;
            this.PomBotIcon.Tag = "1";
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
            this.passwordShowBT.Location = new System.Drawing.Point(291, 355);
            this.passwordShowBT.Name = "passwordShowBT";
            this.passwordShowBT.Size = new System.Drawing.Size(20, 19);
            this.passwordShowBT.TabIndex = 103;
            this.passwordShowBT.TabStop = false;
            this.passwordShowBT.UseVisualStyleBackColor = false;
            this.passwordShowBT.Click += new System.EventHandler(this.passwordShowBT_Click);
            // 
            // feedBackLB
            // 
            this.feedBackLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.feedBackLB.AutoSize = true;
            this.feedBackLB.Font = new System.Drawing.Font("Earth 2073", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedBackLB.ForeColor = System.Drawing.Color.Lime;
            this.feedBackLB.Location = new System.Drawing.Point(74, 400);
            this.feedBackLB.Name = "feedBackLB";
            this.feedBackLB.Size = new System.Drawing.Size(199, 12);
            this.feedBackLB.TabIndex = 115;
            this.feedBackLB.Text = "REGISTRATION REQUEST SENT";
            this.feedBackLB.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(348, 562);
            this.Controls.Add(this.feedBackLB);
            this.Controls.Add(this.passwordShowBT);
            this.Controls.Add(this.PomBotIcon);
            this.Controls.Add(this.formNamePN);
            this.Controls.Add(this.nameLn);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.namePic);
            this.Controls.Add(this.userLn);
            this.Controls.Add(this.userTB);
            this.Controls.Add(this.userPic);
            this.Controls.Add(this.passwordLn);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.mailLn);
            this.Controls.Add(this.passPic);
            this.Controls.Add(this.mailTB);
            this.Controls.Add(this.mailPic);
            this.Controls.Add(this.nameApp);
            this.Controls.Add(this.RegisterUserBT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            ((System.ComponentModel.ISupportInitialize)(this.namePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailPic)).EndInit();
            this.formNamePN.ResumeLayout(false);
            this.formNamePN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PomBotIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RegisterUserBT;
        private System.Windows.Forms.TextBox nameApp;
        private System.Windows.Forms.Panel passwordLn;
        private System.Windows.Forms.Panel mailLn;
        private System.Windows.Forms.PictureBox passPic;
        private System.Windows.Forms.PictureBox mailPic;
        private System.Windows.Forms.Panel userLn;
        private System.Windows.Forms.PictureBox userPic;
        private System.Windows.Forms.Panel nameLn;
        private System.Windows.Forms.PictureBox namePic;
        private System.Windows.Forms.Label winNameLB;
        private System.Windows.Forms.Button closeBT;
        private System.Windows.Forms.Panel formNamePN;
        private System.Windows.Forms.PictureBox PomBotIcon;
        private System.Windows.Forms.Button passwordShowBT;
        internal System.Windows.Forms.TextBox nameTB;
        internal System.Windows.Forms.TextBox passwordTB;
        internal System.Windows.Forms.TextBox mailTB;
        internal System.Windows.Forms.TextBox userTB;
        private System.Windows.Forms.Label feedBackLB;
        private System.Windows.Forms.Timer timer1;
    }
}