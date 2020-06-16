using Pombot_UI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDde.Client;
using Pombot_UI.RobotLibrary;

namespace Pombot_UI
{
    public partial class PomBotAppForm : Form
    {
        public string user;
        private int mov, movY, movX;
        private List<Panel> menuItems = new List<Panel>();

        private Dashboard dashB = Dashboard.GetInstance(); //access to the class Dashboard
        private bool connectedDDE;


        public PomBotAppForm()
        {
            InitializeComponent();
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm ");
            dashboardBT.Focus();
            dashboardPN.Visible = true;
            timer1.Start();
            winNameLB.Text = Program.appName;

            menuItems.Add(dashboardPN);
            menuItems.Add(bot1PN);
            menuItems.Add(bot2PN);
            menuItems.Add(bot2PN);
            menuItems.Add(bot3PN);
            menuItems.Add(bot4PN);
            menuItems.Add(howToPN);
            menuItems.Add(configPN);
            foreach (Panel item in menuItems)
            {
                //item.Parent = PomBotAppForm.ActiveForm;
                item.Location = new Point(166, 67);
                item.Visible = false;
            }

            dashboardPN.Visible = true;

                connectedDDE = dashB.ConnectDDE();
            if (connectedDDE) { ddeConnectLB.ForeColor = Color.Lime; ddeConnectLB.Text = "CONNECTED"; }
            else
            {
                ddeConnectLB.ForeColor = Color.Red; ddeConnectLB.Text = "DISCONNECTED";

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm");
            usernameLB.Text = Program.userName;
            DDEupdateStatus();
        }

        #region MoveWindow
        private void formNamePN_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movY = e.Y;
            movX = e.X;
            PomBotAppForm.ActiveForm.Opacity = 0.5f;
        }
        private void formNamePN_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        private void formNamePN_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
            PomBotAppForm.ActiveForm.Opacity = 1;
        }
        #endregion

        #region highlight buttons
        private void dashboardBT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, dashboardBT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, dashboardBT.Location.Y + 44);
        }
        private void bot1BT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, bot1BT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, bot1BT.Location.Y + 44);
        }
        private void bot2BT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, bot2BT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, bot2BT.Location.Y + 44);
        }
        private void bot3BT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, bot3BT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, bot3BT.Location.Y + 44);
        }
        private void bot4BT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, bot4BT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, bot4BT.Location.Y + 44);
        }
        private void howToBT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, howToBT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, howToBT.Location.Y + 44);
        }
        private void configBT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, configBT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, configBT.Location.Y + 44);
        }
        private void exitBT_Enter(object sender, EventArgs e)
        {
            buttonHL.Location = new Point(buttonHL.Location.X, exitBT.Location.Y);
            panelHL.Location = new Point(panelHL.Location.X, exitBT.Location.Y + 44);
        }
        #endregion

        #region Menu Buttons Click
        private void dashboardBT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            dashboardPN.Visible = true;
        }
        private void bot1BT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            bot1PN.Visible = true;
        }
        private void bot2BT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            bot2PN.Visible = true;
        }
        private void bot3BT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            bot3PN.Visible = true;
        }
        private void bot4BT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            bot4PN.Visible = true;

        }
        private void configBT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            configPN.Visible = true;
        }
        private void howToBT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            howToPN.Visible = true;
        }
        private void exitBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region DashBoard Items
        private void reconnectDDE_Click(object sender, EventArgs e)
        {
            DDEupdateStatus();
            openProfit.Visible = (connectedDDE == true) ?  false : true;
        }
        private void DDEupdateStatus()
        {
            connectedDDE = dashB.ConnectDDE();
            if (connectedDDE)
            {
                ddeConnectLB.ForeColor = Color.Lime;
                ddeConnectLB.Text = "CONNECTED";
                openProfit.Visible = false;
            }
            else
            {
                ddeConnectLB.ForeColor = Color.Red; ddeConnectLB.Text = "DISCONNECTED";
            }
        }

        #endregion
    }
}
