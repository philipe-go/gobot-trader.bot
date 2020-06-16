namespace Pombot_UI
{
    partial class PomBotAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PomBotAppForm));
            this.winNameLB = new System.Windows.Forms.Label();
            this.formNamePN = new System.Windows.Forms.Panel();
            this.userPic = new System.Windows.Forms.PictureBox();
            this.dateLB = new System.Windows.Forms.Label();
            this.usernameLB = new System.Windows.Forms.Label();
            this.menuPN = new System.Windows.Forms.Panel();
            this.exitBT = new System.Windows.Forms.Button();
            this.configBT = new System.Windows.Forms.Button();
            this.howToBT = new System.Windows.Forms.Button();
            this.bot4BT = new System.Windows.Forms.Button();
            this.bot3BT = new System.Windows.Forms.Button();
            this.bot2BT = new System.Windows.Forms.Button();
            this.bot1BT = new System.Windows.Forms.Button();
            this.dashboardBT = new System.Windows.Forms.Button();
            this.buttonHL = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelHL = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dashboardPN = new System.Windows.Forms.Panel();
            this.ddeConnectLB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.reconnectDDE = new System.Windows.Forms.Button();
            this.bashboardLB = new System.Windows.Forms.Label();
            this.bot3PN = new System.Windows.Forms.Panel();
            this.bot3LB = new System.Windows.Forms.Label();
            this.bot4PN = new System.Windows.Forms.Panel();
            this.bot4LB = new System.Windows.Forms.Label();
            this.bot1PN = new System.Windows.Forms.Panel();
            this.bot1LB = new System.Windows.Forms.Label();
            this.howToPN = new System.Windows.Forms.Panel();
            this.howToLB = new System.Windows.Forms.Label();
            this.configPN = new System.Windows.Forms.Panel();
            this.configLB = new System.Windows.Forms.Label();
            this.bot2PN = new System.Windows.Forms.Panel();
            this.bot2LB = new System.Windows.Forms.Label();
            this.openProfit = new System.Windows.Forms.Label();
            this.formNamePN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).BeginInit();
            this.menuPN.SuspendLayout();
            this.panel1.SuspendLayout();
            this.dashboardPN.SuspendLayout();
            this.bot3PN.SuspendLayout();
            this.bot4PN.SuspendLayout();
            this.bot1PN.SuspendLayout();
            this.howToPN.SuspendLayout();
            this.configPN.SuspendLayout();
            this.bot2PN.SuspendLayout();
            this.SuspendLayout();
            // 
            // winNameLB
            // 
            resources.ApplyResources(this.winNameLB, "winNameLB");
            this.winNameLB.ForeColor = System.Drawing.Color.Black;
            this.winNameLB.Name = "winNameLB";
            // 
            // formNamePN
            // 
            this.formNamePN.BackColor = System.Drawing.Color.Gray;
            this.formNamePN.Controls.Add(this.userPic);
            this.formNamePN.Controls.Add(this.dateLB);
            this.formNamePN.Controls.Add(this.usernameLB);
            this.formNamePN.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            resources.ApplyResources(this.formNamePN, "formNamePN");
            this.formNamePN.Name = "formNamePN";
            this.formNamePN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formNamePN_MouseDown);
            this.formNamePN.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formNamePN_MouseMove);
            this.formNamePN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formNamePN_MouseUp);
            // 
            // userPic
            // 
            this.userPic.BackgroundImage = global::Pombot_UI.Properties.Resources.userLogin;
            resources.ApplyResources(this.userPic, "userPic");
            this.userPic.Name = "userPic";
            this.userPic.TabStop = false;
            // 
            // dateLB
            // 
            resources.ApplyResources(this.dateLB, "dateLB");
            this.dateLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateLB.Name = "dateLB";
            // 
            // usernameLB
            // 
            resources.ApplyResources(this.usernameLB, "usernameLB");
            this.usernameLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.usernameLB.Name = "usernameLB";
            // 
            // menuPN
            // 
            this.menuPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuPN.Controls.Add(this.exitBT);
            this.menuPN.Controls.Add(this.configBT);
            this.menuPN.Controls.Add(this.howToBT);
            this.menuPN.Controls.Add(this.bot4BT);
            this.menuPN.Controls.Add(this.bot3BT);
            this.menuPN.Controls.Add(this.bot2BT);
            this.menuPN.Controls.Add(this.bot1BT);
            this.menuPN.Controls.Add(this.dashboardBT);
            this.menuPN.Controls.Add(this.buttonHL);
            resources.ApplyResources(this.menuPN, "menuPN");
            this.menuPN.Name = "menuPN";
            // 
            // exitBT
            // 
            this.exitBT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.exitBT, "exitBT");
            this.exitBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBT.FlatAppearance.BorderSize = 0;
            this.exitBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.exitBT.ForeColor = System.Drawing.Color.Black;
            this.exitBT.Name = "exitBT";
            this.exitBT.UseVisualStyleBackColor = false;
            this.exitBT.Click += new System.EventHandler(this.exitBT_Click);
            this.exitBT.Enter += new System.EventHandler(this.exitBT_Enter);
            // 
            // configBT
            // 
            this.configBT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.configBT, "configBT");
            this.configBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configBT.FlatAppearance.BorderSize = 0;
            this.configBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.configBT.ForeColor = System.Drawing.Color.Black;
            this.configBT.Name = "configBT";
            this.configBT.UseVisualStyleBackColor = false;
            this.configBT.Click += new System.EventHandler(this.configBT_Click);
            this.configBT.Enter += new System.EventHandler(this.configBT_Enter);
            // 
            // howToBT
            // 
            this.howToBT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.howToBT, "howToBT");
            this.howToBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.howToBT.FlatAppearance.BorderSize = 0;
            this.howToBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.howToBT.ForeColor = System.Drawing.Color.Black;
            this.howToBT.Name = "howToBT";
            this.howToBT.UseVisualStyleBackColor = false;
            this.howToBT.Click += new System.EventHandler(this.howToBT_Click);
            this.howToBT.Enter += new System.EventHandler(this.howToBT_Enter);
            // 
            // bot4BT
            // 
            this.bot4BT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bot4BT, "bot4BT");
            this.bot4BT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bot4BT.FlatAppearance.BorderSize = 0;
            this.bot4BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.bot4BT.ForeColor = System.Drawing.Color.Black;
            this.bot4BT.Name = "bot4BT";
            this.bot4BT.UseVisualStyleBackColor = false;
            this.bot4BT.Click += new System.EventHandler(this.bot4BT_Click);
            this.bot4BT.Enter += new System.EventHandler(this.bot4BT_Enter);
            // 
            // bot3BT
            // 
            this.bot3BT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bot3BT, "bot3BT");
            this.bot3BT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bot3BT.FlatAppearance.BorderSize = 0;
            this.bot3BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.bot3BT.ForeColor = System.Drawing.Color.Black;
            this.bot3BT.Name = "bot3BT";
            this.bot3BT.UseVisualStyleBackColor = false;
            this.bot3BT.Click += new System.EventHandler(this.bot3BT_Click);
            this.bot3BT.Enter += new System.EventHandler(this.bot3BT_Enter);
            // 
            // bot2BT
            // 
            this.bot2BT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bot2BT, "bot2BT");
            this.bot2BT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bot2BT.FlatAppearance.BorderSize = 0;
            this.bot2BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.bot2BT.ForeColor = System.Drawing.Color.Black;
            this.bot2BT.Name = "bot2BT";
            this.bot2BT.UseVisualStyleBackColor = false;
            this.bot2BT.Click += new System.EventHandler(this.bot2BT_Click);
            this.bot2BT.Enter += new System.EventHandler(this.bot2BT_Enter);
            // 
            // bot1BT
            // 
            this.bot1BT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.bot1BT, "bot1BT");
            this.bot1BT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bot1BT.FlatAppearance.BorderSize = 0;
            this.bot1BT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.bot1BT.ForeColor = System.Drawing.Color.Black;
            this.bot1BT.Name = "bot1BT";
            this.bot1BT.UseVisualStyleBackColor = false;
            this.bot1BT.Click += new System.EventHandler(this.bot1BT_Click);
            this.bot1BT.Enter += new System.EventHandler(this.bot1BT_Enter);
            // 
            // dashboardBT
            // 
            this.dashboardBT.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.dashboardBT, "dashboardBT");
            this.dashboardBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashboardBT.FlatAppearance.BorderSize = 0;
            this.dashboardBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.dashboardBT.ForeColor = System.Drawing.Color.Black;
            this.dashboardBT.Name = "dashboardBT";
            this.dashboardBT.UseVisualStyleBackColor = false;
            this.dashboardBT.Click += new System.EventHandler(this.dashboardBT_Click);
            this.dashboardBT.Enter += new System.EventHandler(this.dashboardBT_Enter);
            // 
            // buttonHL
            // 
            this.buttonHL.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.buttonHL, "buttonHL");
            this.buttonHL.Name = "buttonHL";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelHL
            // 
            this.panelHL.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.panelHL, "panelHL");
            this.panelHL.Name = "panelHL";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dashboardPN);
            this.panel1.Controls.Add(this.bot3PN);
            this.panel1.Controls.Add(this.bot4PN);
            this.panel1.Controls.Add(this.bot1PN);
            this.panel1.Controls.Add(this.howToPN);
            this.panel1.Controls.Add(this.configPN);
            this.panel1.Controls.Add(this.bot2PN);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dashboardPN
            // 
            this.dashboardPN.BackColor = System.Drawing.Color.Gray;
            this.dashboardPN.Controls.Add(this.openProfit);
            this.dashboardPN.Controls.Add(this.ddeConnectLB);
            this.dashboardPN.Controls.Add(this.label1);
            this.dashboardPN.Controls.Add(this.button3);
            this.dashboardPN.Controls.Add(this.button2);
            this.dashboardPN.Controls.Add(this.reconnectDDE);
            this.dashboardPN.Controls.Add(this.bashboardLB);
            resources.ApplyResources(this.dashboardPN, "dashboardPN");
            this.dashboardPN.Name = "dashboardPN";
            // 
            // ddeConnectLB
            // 
            resources.ApplyResources(this.ddeConnectLB, "ddeConnectLB");
            this.ddeConnectLB.ForeColor = System.Drawing.Color.Lime;
            this.ddeConnectLB.Name = "ddeConnectLB";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.button3, "button3");
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.button2, "button2");
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // reconnectDDE
            // 
            this.reconnectDDE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.reconnectDDE, "reconnectDDE");
            this.reconnectDDE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reconnectDDE.FlatAppearance.BorderSize = 0;
            this.reconnectDDE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.reconnectDDE.ForeColor = System.Drawing.Color.Black;
            this.reconnectDDE.Name = "reconnectDDE";
            this.reconnectDDE.UseVisualStyleBackColor = false;
            this.reconnectDDE.Click += new System.EventHandler(this.reconnectDDE_Click);
            // 
            // bashboardLB
            // 
            resources.ApplyResources(this.bashboardLB, "bashboardLB");
            this.bashboardLB.Name = "bashboardLB";
            // 
            // bot3PN
            // 
            this.bot3PN.BackColor = System.Drawing.Color.Gray;
            this.bot3PN.Controls.Add(this.bot3LB);
            resources.ApplyResources(this.bot3PN, "bot3PN");
            this.bot3PN.Name = "bot3PN";
            // 
            // bot3LB
            // 
            resources.ApplyResources(this.bot3LB, "bot3LB");
            this.bot3LB.Name = "bot3LB";
            // 
            // bot4PN
            // 
            this.bot4PN.BackColor = System.Drawing.Color.Gray;
            this.bot4PN.Controls.Add(this.bot4LB);
            resources.ApplyResources(this.bot4PN, "bot4PN");
            this.bot4PN.Name = "bot4PN";
            // 
            // bot4LB
            // 
            resources.ApplyResources(this.bot4LB, "bot4LB");
            this.bot4LB.Name = "bot4LB";
            // 
            // bot1PN
            // 
            this.bot1PN.BackColor = System.Drawing.Color.Gray;
            this.bot1PN.Controls.Add(this.bot1LB);
            resources.ApplyResources(this.bot1PN, "bot1PN");
            this.bot1PN.Name = "bot1PN";
            // 
            // bot1LB
            // 
            resources.ApplyResources(this.bot1LB, "bot1LB");
            this.bot1LB.Name = "bot1LB";
            // 
            // howToPN
            // 
            this.howToPN.BackColor = System.Drawing.Color.Gray;
            this.howToPN.Controls.Add(this.howToLB);
            resources.ApplyResources(this.howToPN, "howToPN");
            this.howToPN.Name = "howToPN";
            // 
            // howToLB
            // 
            resources.ApplyResources(this.howToLB, "howToLB");
            this.howToLB.Name = "howToLB";
            // 
            // configPN
            // 
            this.configPN.BackColor = System.Drawing.Color.Gray;
            this.configPN.Controls.Add(this.configLB);
            resources.ApplyResources(this.configPN, "configPN");
            this.configPN.Name = "configPN";
            // 
            // configLB
            // 
            resources.ApplyResources(this.configLB, "configLB");
            this.configLB.Name = "configLB";
            // 
            // bot2PN
            // 
            this.bot2PN.BackColor = System.Drawing.Color.Gray;
            this.bot2PN.Controls.Add(this.bot2LB);
            resources.ApplyResources(this.bot2PN, "bot2PN");
            this.bot2PN.Name = "bot2PN";
            // 
            // bot2LB
            // 
            resources.ApplyResources(this.bot2LB, "bot2LB");
            this.bot2LB.Name = "bot2LB";
            // 
            // openProfit
            // 
            resources.ApplyResources(this.openProfit, "openProfit");
            this.openProfit.ForeColor = System.Drawing.Color.Red;
            this.openProfit.Name = "openProfit";
            // 
            // PomBotAppForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHL);
            this.Controls.Add(this.menuPN);
            this.Controls.Add(this.formNamePN);
            this.Controls.Add(this.winNameLB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PomBotAppForm";
            this.formNamePN.ResumeLayout(false);
            this.formNamePN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPic)).EndInit();
            this.menuPN.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.dashboardPN.ResumeLayout(false);
            this.dashboardPN.PerformLayout();
            this.bot3PN.ResumeLayout(false);
            this.bot3PN.PerformLayout();
            this.bot4PN.ResumeLayout(false);
            this.bot4PN.PerformLayout();
            this.bot1PN.ResumeLayout(false);
            this.bot1PN.PerformLayout();
            this.howToPN.ResumeLayout(false);
            this.howToPN.PerformLayout();
            this.configPN.ResumeLayout(false);
            this.configPN.PerformLayout();
            this.bot2PN.ResumeLayout(false);
            this.bot2PN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label winNameLB;
        private System.Windows.Forms.Panel formNamePN;
        private System.Windows.Forms.Panel menuPN;
        private System.Windows.Forms.Button dashboardBT;
        private System.Windows.Forms.Panel buttonHL;
        private System.Windows.Forms.Button howToBT;
        private System.Windows.Forms.Button bot4BT;
        private System.Windows.Forms.Button bot3BT;
        private System.Windows.Forms.Button bot2BT;
        private System.Windows.Forms.Button bot1BT;
        private System.Windows.Forms.Label dateLB;
        private System.Windows.Forms.Label usernameLB;
        private System.Windows.Forms.Button configBT;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox userPic;
        private System.Windows.Forms.Button exitBT;
        private System.Windows.Forms.Panel panelHL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel bot3PN;
        private System.Windows.Forms.Label bot3LB;
        private System.Windows.Forms.Panel bot4PN;
        private System.Windows.Forms.Label bot4LB;
        private System.Windows.Forms.Panel bot1PN;
        private System.Windows.Forms.Label bot1LB;
        private System.Windows.Forms.Panel howToPN;
        private System.Windows.Forms.Label howToLB;
        private System.Windows.Forms.Panel configPN;
        private System.Windows.Forms.Label configLB;
        private System.Windows.Forms.Panel dashboardPN;
        private System.Windows.Forms.Label bashboardLB;
        private System.Windows.Forms.Panel bot2PN;
        private System.Windows.Forms.Label bot2LB;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button reconnectDDE;
        private System.Windows.Forms.Label ddeConnectLB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label openProfit;
    }
}