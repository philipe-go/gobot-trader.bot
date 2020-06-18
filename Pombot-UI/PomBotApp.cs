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
        private List<Bot> botsList = new List<Bot>();

        private Dashboard dashB = Dashboard.GetInstance(); //access to the class Dashboard
        private bool connectedDDE;

        private int countdown = 0;

        #region Bot parameters

        #endregion


        public PomBotAppForm()
        {
            InitializeComponent();
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm ");
            dashboardBT.Focus();
            dashboardPN.Visible = true;
            timer1.Start(); //Timer to control clock and DDE connection status update
            winNameLB.Text = Program.appName; //Retrieve app name from Program.cs and set into UI

            Bot bot1 = new Bot();
            Bot bot2 = new Bot();
            Bot bot3 = new Bot();
            Bot bot4 = new Bot();
            botsList.Add(bot1);
            botsList.Add(bot2);
            botsList.Add(bot3);
            botsList.Add(bot4);

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

            UpdateDDEStrategy();
            DDEupdateStatus();

            //BOT forms
            BOTsInit();
        }
        //timer used for connection checker and date update
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm");
            usernameLB.Text = Program.userName;
            DDEupdateStatus();
        }
        //timer used for countdowns to saved label
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            countdown--;
            if (countdown <= 0)
            {
                saveBT.ForeColor = Color.Black;
                saveBT.Text = "Save Parameters";
                timer2.Stop();
            }
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
        //DDE
        private void reconnectDDE_Click(object sender, EventArgs e)
        {
            DDEupdateStatus();
            openProfit.Visible = (connectedDDE == true) ? false : true;
        }
        private void DDEupdateStatus()
        {
            connectedDDE = dashB.ConnectDDE();
            if (connectedDDE)
            {
                ddeConnectLB.ForeColor = Color.Lime;
                ddeConnectLB.Text = "CONNECTED";
                connectionPic.BackgroundImage = Resources.connectionIcon2;
                openProfit.Visible = false;
            }
            else
            {
                ddeConnectLB.ForeColor = Color.Red; ddeConnectLB.Text = "DISCONNECTED";
                connectionPic.BackgroundImage = Resources.connectionIcon;
            }
        }

        //STRATEGY
        private void UpdateDDEStrategy() //RETRIEVE SAVED INFORMATIONS - should change later to retrieve from xml file and set to dashB
        {
            renkoInput.Value = dashB.renkoPeriod;
            renkoTB.Text = renkoInput.Value.ToString() + "R";
            rsiHistoryInput.Value = dashB.historySize;
            rsiHistoryTB.Text = rsiHistoryInput.Value.ToString();
            plot3Input.Value = dashB.plot3Size;
            plot3TB.Text = plot3Input.Value.ToString();
        }
        private void buyKeyboardTB_TextChanged(object sender, EventArgs e) //KEYBOARD BINDING TO BUY
        {
            dashB.buyKeyboard = buyKeyboardTB.Text;
        }
        private void sellKeyboardTB_TextChanged(object sender, EventArgs e) //KEYBOARD BINDING TO SELL
        {
            dashB.sellKeyboard = sellKeyboardTB.Text;
        }
        private void zeroKeyboardTB_TextChanged(object sender, EventArgs e) //KEYBOARD BINDING TO ZERO
        {
            dashB.zeroKeyboard = zeroKeyboardTB.Text;
        }
        private void renkoInput_Scroll(object sender, ScrollEventArgs e)
        {
            renkoTB.Text = renkoInput.Value.ToString() + "R";
            dashB.renkoPeriod = renkoInput.Value;
        }
        private void inversionStrategyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (inversionStrategyCB.Checked) dashB.inversionStrategy = true;
            else dashB.inversionStrategy = false;
        }
        private void rsiHistoryInput_Scroll(object sender, ScrollEventArgs e)
        {
            rsiHistoryTB.Text = rsiHistoryInput.Value.ToString();
            dashB.historySize = rsiHistoryInput.Value;

            //bots manual txt
            manualCalibBot1Txt.Text = $"Enter the closing price of the last {dashB.historySize.ToString()} periods from the newest to the oldest:";
        }
        private void plot3Input_Scroll(object sender, ScrollEventArgs e)
        {
            plot3TB.Text = plot3Input.Value.ToString();
            dashB.plot3Size = plot3Input.Value;
        }

        private void saveBT_Click(object sender, EventArgs e)
        {
            countdown = 15;
            saveBT.ForeColor = Color.Lime;
            saveBT.Text = "Saved";
            timer2.Start();
        }
        private void stopBotsBT_Click(object sender, EventArgs e)
        {
            stopBot1BT.PerformClick();
        }
        #endregion

        private void BOTsInit()
        {
            startBot1BT.Enabled = false;
            autoCalibBot1Check.Select();
            manCalibBot1TB.Enabled = false;
            botsList[0].strategy.feedbackTable = bot1Operations;
            botsList[0].currentMeasure = ddeCurrentMeasureBot1;
            botsList[0].strategy.calibBar = calibrationBot1Bar;
        }

        #region BOT1
        private void tickerTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tickerTB_Leave(sender, e);
            }
        }
        private void tickerTB_Leave(object sender, EventArgs e)
        {
            if (tickerTB.Text != "")
            {
                botsList[0].ticker = tickerTB.Text;
                tickerInput.Text = botsList[0].ticker;
                tickerTB.Clear();
            }
        }
        private void autoCalibBot1Check_MouseClick(object sender, MouseEventArgs e)
        {
            manualCalibBot1Check.Checked = false;
            autoCalibBot1Check.Select();
            botsList[0].manualCalib = false;
            lastperOpenTB.Enabled = true;
            lastPerCloseTB.Enabled = true;
            manCalibBot1TB.Enabled = false;
        }
        private void manualCalibBot1Check_MouseClick(object sender, MouseEventArgs e)
        {
            manualCalibBot1Check.Select();
            autoCalibBot1Check.Checked = false;
            botsList[0].manualCalib = true;
            manCalibBot1TB.Enabled = true;
            lastperOpenTB.Enabled = false;
            lastPerCloseTB.Enabled = false;
        }
        private void lastperOpenTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastperOpenTB_Leave(sender, e);
            }
        }
        private void lastperOpenTB_Leave(object sender, EventArgs e)
        {
            if (lastperOpenTB.Text != "")
            {
                calibrationBot1Bar.Value++;
                botsList[0].InitialBrick(System.Convert.ToDouble(lastperOpenTB.Text));
                enteredBot1OpenTB.Text = botsList[0].GetInitialBrick().ToString();
                lastperOpenTB.Clear();
            }
        }
        private void lastPerCloseTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastPerCloseTB_Leave(sender, e);
            }
        }
        private void lastPerCloseTB_Leave(object sender, EventArgs e)
        {
            if (lastPerCloseTB.Text != "")
            {
                calibrationBot1Bar.Value++;
                botsList[0].FinalBrick(System.Convert.ToDouble(lastPerCloseTB.Text));
                enteredBot1CloseTB.Text = botsList[0].GetFinalBrick().ToString();
                lastPerCloseTB.Clear();
            }
        }
        private void manCalibBot1TB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && manCalibBot1TB.Text != "")
            {
                botsList[0].ManualCalibration(System.Convert.ToDouble(manCalibBot1TB.Text));
                if (dashB.historySize - (botsList[0].GetHistorySize()) >= -1)
                {
                    manualEnterNrTB.Text = (botsList[0].GetHistorySize()).ToString();
                }
                else
                {
                    calibrateBot1BT.PerformClick();
                }
                manCalibBot1TB.Text = "";
                calibrationBot1Bar.Value++;
            }
        }
        //BOT BUTTONS
        private void resetCalibBT_Click(object sender, EventArgs e)
        {
            botsList[0].ResetCalibration();
            calibrationBot1Bar.Value = 0;
            enteredBot1OpenTB.Text = botsList[0].GetInitialBrick().ToString();
            enteredBot1CloseTB.Text = botsList[0].GetFinalBrick().ToString();
            lastperOpenTB.Clear();
            lastPerCloseTB.Clear();
        }
        private void saveParamsBot1BT_Click(object sender, EventArgs e)
        {
            countdown = 15;
            saveParamsBot1BT.ForeColor = Color.Lime;
            saveParamsBot1BT.Text = "Saved";
            timerBot1.Start();
        }
        private void timerBot1_Tick(object sender, EventArgs e)
        {
            countdown--;
            if (countdown <= 0)
            {
                saveParamsBot1BT.ForeColor = Color.Black;
                saveParamsBot1BT.Text = "Save Parameters";
                timerBot1.Stop();
            }
        }
        private void calibrateBot1BT_Click(object sender, EventArgs e)
        {
            if (botsList[0].ticker == "")
            {
                tickerTB.Focus();
            }
            else if (botsList[0].GetFinalBrick() == 0)
            {
                lastPerCloseTB.Focus();
            }
            else if (botsList[0].GetInitialBrick() == 0)
            {
                lastperOpenTB.Focus();
            }
            else
            {
                calibBot1.Start();
                calibrateBot1BT.Enabled = false;
                manCalibBot1TB.Enabled = false;
                lastperOpenTB.Enabled = false;
                lastPerCloseTB.Enabled = false;
                tickerTB.Enabled = false;
            }
        }
        private void calibBot1_Tick(object sender, EventArgs e) //CALIBRATION LOAD BAR TIMER
        {
            //calibrationBot1Bar.Value++;
            if (botsList[0].strategy.botActive == true)
            {
                startBot1BT.Enabled = true;
                if (autoStartCheck.Checked) startBot1BT.PerformClick();
                calibBot1.Stop();
            }
        }
        private void startBot1BT_Click(object sender, EventArgs e)
        {
            robot1PB.BackgroundImage = Resources.robotIcon2;
            bot1Status.ForeColor = Color.Lime;
            bot1Status.Text = "CONNECTED";
            botsList[0].Connect(true); //connect bot actions (buy, sell, zero)

            //deactivate items
            calibrateBot1BT.Enabled = false;
            rsiHistoryInput.Enabled = false;
            plot3Input.Enabled = false;
            renkoInput.Enabled = false;
        }
        private void stopBot1BT_Click(object sender, EventArgs e)
        {
            botsList[0].Connect(false); //disconnect bot actions (buy, sell, zero) but maintain the RSI history building

            robot1PB.BackgroundImage = Resources.robotIcon;
            bot1Status.ForeColor = Color.Red;
            bot1Status.Text = "DISCONNECTED";
            calibrationBot1Bar.Value = 0;
        }
        #endregion
    }
}
