using Pombot_UI.Properties;
using System;
using System.Collections.Generic;
using System.Threading;
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
using System.Windows.Forms.VisualStyles;

namespace Pombot_UI
{
    public partial class PomBotAppForm : Form
    {
        #region DashBoard and DDE Attributes
        private DdeClient client;
        private bool connectedDDE;
        private Dashboard dashB = Dashboard.GetInstance(); //access to the class Dashboard
        private bool buyKey = false;
        private bool sellkey = false;
        private bool zeroKey = false;
        #endregion

        #region Form Attributes 
        private float winOpacity = 1;
        public string user; //user name to be shown on the top part of the form
        private int mov, movY, movX; //variables to handle the Drag and Move method of the form 
        private List<Panel> menuItems = new List<Panel>(); //menu items - to handle which one will be on focus and activated
        private List<Bot> botsList = new List<Bot>(); //BOT LIST -->> check is will be possible to implement more than 1 bot
        private int countdown = 0;
        #endregion

        #region Bot parameters Multi Thread
        internal delegate void UpdateTextBoxDelegate(string textBoxNewText);
        internal UpdateTextBoxDelegate textBoxDelegate; //delegate object
        internal delegate void UpdateRSIDelegate(string rsi, string plot);
        internal UpdateRSIDelegate rsiDelegate; //delegate object
        private bool updateRSI;
        internal delegate void UpdateTableDelegate(string item2, double item3);
        internal UpdateTableDelegate updateTableDelegate; //delegate object
        private bool addTableItem;
        #endregion

        public PomBotAppForm()
        {
            InitializeComponent();

            InitializeMainForm();
        }

        //timer used for connection checker and date update
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm");
            usernameLB.Text = Program.userName;
            DDEupdateStatus();
        }

        #region Form Initialization
        private void InitializeMainForm()
        {
            this.client = new DdeClient(dashB.app, dashB.service);
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy-HH:mm ");
            timer1.Start(); //Timer to control clock and DDE connection status update
            winNameLB.Text = Program.appName; //Retrieve app name from Program.cs and set into UI

            #region BOTs initialization
            //TODO - If not possible to instantiate more than 1 bot, remove list and create just one object
            Bot bot1 = new Bot();
            botsList.Add(bot1);
            //connect this form with all bots so bot have access to its directly
            foreach (Bot bot in botsList)
            {
                bot.mainForm = this;
            }
            //BOT forms initialization
            BOTsInit();
            #endregion

            #region MenuItems and UI elements
            menuItems.Add(dashboardPN);
            menuItems.Add(bot1PN);
            menuItems.Add(bot2PN);
            menuItems.Add(bot2PN);
            menuItems.Add(bot3PN);
            menuItems.Add(bot4PN);
            menuItems.Add(helpPN);
            menuItems.Add(aboutPN);
            foreach (Panel item in menuItems)
            {
                item.Location = new Point(166, 67);
                item.Visible = false;
            }

            dashboardBT.Focus();
            dashboardPN.Visible = true;
            #endregion

            ReadSavedData();

            //DDE Connection
            ConnectDDE();
            DDEupdateStatus();
            UpdateDDEStrategy();

            //Delegates 
            StartMultiThread();

            //Keys Bindings
            InitializeKeys();

            calibrationBot1Bar.Maximum = dashB.historySize;
        }

        #region ReadSavedData
        private void ReadSavedData()
        {
            List<int> dashItems = new List<int>();
            dashItems = DataBase.ReadDashParameters();
            List<string> botItems = new List<string>();
            botItems = DataBase.ReadBotParameters();
            SetDashParam(dashItems);
            SetBotParam(botItems);
        }
        private void SetDashParam(List<int> list)
        {
            if (list.Count() > 0)
            {
                dashB.renkoPeriod = list[0];
                dashB.historySize = list[1];
                dashB.plot3Size = list[2];
                SetInversionStrategy(list[3]);
                SetOpacity(list[4]);
                TopMostApp(list[5]);
            }
        }
        private void SetBotParam(List<string> list)
        {
            if (list.Count() > 0)
            {
                this.tickerTB.Text = list[0];
                SetCalibration(Convert.ToInt32(list[1]));
                this.autoStartCheck.Checked = list[2] == "True" ? true : false;
            }
        }
        #endregion

        #endregion

        #region DDE interface
        private bool ConnectDDE()
        {
            client.Disconnected += OnDisconnected;
            try
            {
                client.Connect();
            }
            catch (NDde.DdeException)
            {
                connectedDDE = false;
                return false;
            }
            connectedDDE = true;
            botsList[0].SetProcess(); //---> was accessed straight to strategy before
            return true;
        }
        private void OnDisconnected(object sender, DdeDisconnectedEventArgs args)
        {
            connectedDDE = false;
        }
        internal void Advise(string ticker, string col)
        {
            try
            {
                client.StartAdvise($"{ticker}.{col}", 1, true, 500);
                client.Advise += OnAdvise;
            }
            catch (NDde.DdeException)
            {
                resetCalibBT.ForeColor = Color.Lime;
                tickerLB.ForeColor = Color.Red;
            }
            finally
            {
                resetCalibBT.Focus();
            }
        }
        internal void OnAdvise(object sender, DdeAdviseEventArgs args)
        {
            //---> was accessed straight to strategy before

            botsList[0].Advise(float.Parse(args.Text) / 100); 
            this.Invoke(textBoxDelegate, botsList[0].GetTemp()); //Delegate to update bot form current measure
            if (updateRSI)
            {
                this.Invoke(rsiDelegate, botsList[0].GetRSIMean(), botsList[0].GetPlotMean()); //Delegate to update RSI and Plot measures
            }
            if (addTableItem)
            {
                this.Invoke(updateTableDelegate, botsList[0].GetAction(), botsList[0].GetPrice()); //Delegate to update operations table with last bot Operations
                AddTableItem(false);
            }
        }
        #endregion

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
            PomBotAppForm.ActiveForm.Opacity = winOpacity;
        }
        #endregion

        #region BOT panel initialization
        private void BOTsInit()
        {
            startBot1BT.Enabled = false;
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

        #region MULTITHREAD OPERATIONS
        //*************DELEGATE METHODS FOR THREADS************//
        //*****************************************************//
        private void StartMultiThread()
        {
            textBoxDelegate = new UpdateTextBoxDelegate(UpdateCurrentMeasure); 
            rsiDelegate = new UpdateRSIDelegate(UpdateRSIandPlot);
            //updateRSI = false; //--->COMMENTED TO SHOW THE CURRENT RSI AND PLOT instead of the one when the period closes
            updateTableDelegate = new UpdateTableDelegate(UpdateTableItems);
        }
        private void UpdateCurrentMeasure(string text)
        {
            ddeCurrentMeasureBot1.Text = text;
        }
        private void UpdateRSIandPlot(string rsi, string plot)
        {
            ddeRSIMeasureBot1.Text = rsi;
            ddePlotMeasureBot1.Text = plot;
        }
        private void UpdateTableItems(string item2, double item3)
        {
            ListViewItem newItem = new ListViewItem($"{DateTime.Now.ToString("HH:mm:ss")}");
            newItem.SubItems.Add(item2);
            newItem.SubItems.Add(item3.ToString("0.00"));

            bot1Operations.Items.Insert(0, newItem); //-->> Previouslly was Add not Insert
        }
        internal void UpdateRSI(bool val)
        {
            this.updateRSI = val;
        }
        internal void AddTableItem(bool val)
        {
            this.addTableItem = val;
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
            tickerTB.Focus();
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

            aboutPN.Visible = true;
        }
        private void howToBT_Click(object sender, EventArgs e)
        {
            foreach (Panel item in menuItems)
            {
                item.Visible = false;
            }

            helpPN.Visible = true;
        }
        private void exitBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region DashBoard Items
        //************************ DDE ************************//
        //*****************************************************//
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
        private void reconnectDDE_Click(object sender, EventArgs e) //RECONNECT DDE if Desconnected
        {
            DDEupdateStatus();
            openProfit.Visible = (connectedDDE == true) ? false : true;
        }
        private void DDEupdateStatus() //FEEDBACK ON DDE CONNECTION - Get from Dashboard DDE interface
        {
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
                ConnectDDE();
            }
        }
        //*************STRATEGY DASHBOARD INPUT****************//
        //*****************************************************//
        private void UpdateDDEStrategy() //RETRIEVE SAVED INFORMATIONS
        {
            renkoInput.Value = dashB.renkoPeriod;
            renkoTB.Text = renkoInput.Value.ToString() + "R";
            rsiHistoryInput.Value = dashB.historySize;
            rsiHistoryTB.Text = rsiHistoryInput.Value.ToString();
            plot3Input.Value = dashB.plot3Size;
            plot3TB.Text = plot3Input.Value.ToString();
        }
        //***********STRATEGY PARAMETERS CONTROL***************//
        //*****************************************************//
        private void renkoInput_Scroll(object sender, ScrollEventArgs e)
        {
            renkoTB.Text = renkoInput.Value.ToString() + "R";
            dashB.renkoPeriod = renkoInput.Value;
        }
        private void SetInversionStrategy(int val)
        {
            dashB.inversionStrategy = val == 1 ? true : false;
            inversionLB.Text = val == 1 ? "YES" : "NO";
            inversionLB.ForeColor = val == 1 ? Color.Lime : Color.Red;
            inversionSelector.Value = val;
        }
        private void inversionSelector_Scroll(object sender, ScrollEventArgs e)
        {
            SetInversionStrategy(e.NewValue);
        }
        private void rsiHistoryInput_Scroll(object sender, ScrollEventArgs e)
        {
            rsiHistoryTB.Text = rsiHistoryInput.Value.ToString();
            dashB.historySize = rsiHistoryInput.Value;
            //bots manual input txt
            manualCalibBot1Txt.Text = $"Enter the closing price of the last {dashB.historySize.ToString()} periods from the newest to the oldest:";
        }
        private void plot3Input_Scroll(object sender, ScrollEventArgs e)
        {
            plot3TB.Text = plot3Input.Value.ToString();
            dashB.plot3Size = plot3Input.Value;
        }
        //*****************DASHBOARD BUTTONS*******************//
        //*****************************************************//
        private void saveBT_Click(object sender, EventArgs e)
        {
            countdown = 15;
            saveBT.ForeColor = Color.Lime;
            saveBT.Text = "Saved";
            timer2.Start();

            List<int> temp = new List<int>();
            temp = DataBase.ConvertToListInt(renkoInput.Value, rsiHistoryInput.Value, plot3Input.Value, inversionSelector.Value, opacityScrollBar.Value, topMostSelector.Value);
            DataBase.SaveDashParameters(temp);
        }
        private void stopBotsBT_Click(object sender, EventArgs e) //---->> NOT WORKING, WHY?
        {
            stopBot1BT_Click(sender, e);
        }
        //*************KEYBOARD INPUT MANAGER******************//
        //*****************************************************//
        private void InitializeKeys()
        {
            //buy
            dashB.buyKeyboard.Add(Keys.Shift);
            dashB.buyKeyboard.Add(Keys.C);
            buyLB.Text = "";
            foreach (Keys key in dashB.buyKeyboard)
            {
                buyLB.Text += key.ToString() + "  ";
            }
            //sell
            dashB.sellKeyboard.Add(Keys.Shift);
            dashB.sellKeyboard.Add(Keys.S);
            sellLB.Text = "";
            foreach (Keys key in dashB.sellKeyboard)
            {
                sellLB.Text += key.ToString() + "  ";
            }
            //zero
            dashB.zeroKeyboard.Add(Keys.Shift);
            dashB.zeroKeyboard.Add(Keys.Z);
            zeroLB.Text = "";
            foreach (Keys key in dashB.zeroKeyboard)
            {
                zeroLB.Text += key.ToString() + "  ";
            }
        }
        private void buyKeyPress_Click(object sender, EventArgs e)
        {
            buyKey = true;
            dashB.buyKeyboard.Clear();
            buyLB.Text = "waiting";
        }
        private void buyKeyPress_KeyUp(object sender, KeyEventArgs e)
        {
            if (buyKey)
            {
                if (e.Modifiers != Keys.None)
                {
                    dashB.buyKeyboard.Add((Keys)e.Modifiers);
                    dashB.buyKeyboard.Add(e.KeyCode);
                }
                else
                    dashB.buyKeyboard.Add(e.KeyCode);
                buyKey = false;
            }
            buyLB.Text = "done";
        }
        private void buyKeyPress_Leave(object sender, EventArgs e)
        {
            if (dashB.buyKeyboard.Count() == 0)
            {
                buyLB.Text = "Key";
            }
            else
            {
                buyLB.Text = "";
                foreach (Keys key in dashB.buyKeyboard)
                {
                    buyLB.Text += key.ToString() + "  ";
                }
            }
            buyKey = false;
        }
        private void sellKeyPress_Click(object sender, EventArgs e)
        {
            sellkey = true;
            dashB.sellKeyboard.Clear();
            sellLB.Text = "waiting";
        }
        private void sellKeyPress_KeyUp(object sender, KeyEventArgs e)
        {
            if (sellkey)
            {
                if (e.Modifiers != Keys.None)
                {
                    dashB.sellKeyboard.Add((Keys)e.Modifiers);
                    dashB.sellKeyboard.Add(e.KeyCode);
                }
                else
                    dashB.sellKeyboard.Add(e.KeyCode);
                sellkey = false;
            }
            sellLB.Text = "done";
        }
        private void sellKeyPress_Leave(object sender, EventArgs e)
        {
            if (dashB.sellKeyboard.Count() == 0)
            {
                sellLB.Text = "Key";
            }
            else
            {
                sellLB.Text = "";
                foreach (Keys key in dashB.sellKeyboard)
                {
                    sellLB.Text += key.ToString() + "  ";
                }
            }
            sellkey = false;
        }
        private void zeroKeyPress_Click(object sender, EventArgs e)
        {
            zeroKey = true;
            dashB.zeroKeyboard.Clear();
            zeroLB.Text = "waiting";
        }
        private void zeroKeyPress_KeyUp(object sender, KeyEventArgs e)
        {
            if (zeroKey)
            {
                if (e.Modifiers != Keys.None)
                {
                    dashB.zeroKeyboard.Add((Keys)e.Modifiers);
                    dashB.zeroKeyboard.Add(e.KeyCode);
                }
                else
                    dashB.zeroKeyboard.Add(e.KeyCode);
                zeroKey = false;
            }
            zeroLB.Text = "done";
        }
        private void zeroKeyPress_Leave(object sender, EventArgs e)
        {
            if (dashB.zeroKeyboard.Count() == 0)
            {
                zeroLB.Text = "Key";
            }
            else
            {
                zeroLB.Text = "";
                foreach (Keys key in dashB.zeroKeyboard)
                {
                    zeroLB.Text += key.ToString() + "  ";
                }
            }
            zeroKey = false;
        }
        #endregion

        #region BOT1
        //*************TICKER INPUT FIELD HANDLER**************//
        //*****************************************************//
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
                botsList[0].ticker = tickerTB.Text.ToUpper();
                tickerTB.Text = botsList[0].ticker;
                tickerTB.Enabled = false;
            }
            else
            {
                tickerTB.Enabled = true;
            }
        }
        //****************CALIBRATION SELECTOR*****************//
        //*****************************************************//
        private void SetCalibration(int val) //0 = AUTO; 1 = MANUAL
        {
            botsList[0].manualCalib = val == 1 ? true : false;
            lastperOpenTB.Enabled = val == 1 ? false : true;
            lastPerCloseTB.Enabled = val == 1 ? false : true;
            manCalibBot1TB.Enabled = val == 1 ? true : false;
            calibrationLB.Text = val == 1 ? "MANUAL" : "AUTO";
        }
        private void calibrationSelector_Scroll(object sender, ScrollEventArgs e)
        {
            SetCalibration(e.NewValue);
        }
        //***AUTO CALIBRATION INPUT FIELD OPEN PRICE HANDLER***//
        //*****************************************************//
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
                botsList[0].InitialBrick(System.Convert.ToDouble(lastperOpenTB.Text));
                lastperOpenTB.Text = botsList[0].GetInitialBrick().ToString();
                lastperOpenTB.Enabled = false;
            }
        }
        private void lastperOpenTB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.lastperOpenTB.Text))
            {
                this.lastperOpenTB.Text = "";
                this.lastperOpenTB.Focus();
            }
        }
        //***AUTO CALIBRATION INPUT FIELD CLOSE PRICE HANDLER***//
        //******************************************************//
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
                botsList[0].FinalBrick(Convert.ToDouble(lastPerCloseTB.Text));
                lastPerCloseTB.Text = botsList[0].GetFinalBrick().ToString();
                lastPerCloseTB.Enabled = false;
            }
        }
        private void lastPerCloseTB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.lastPerCloseTB.Text))
            {
                this.lastPerCloseTB.Text = "";
                this.lastPerCloseTB.Focus();
            }
        }
        //*******MANUAL CALIBRATION INPUT FIELD HANDLER********//
        //*****************************************************//
        private void manCalibBot1TB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && manCalibBot1TB.Text != "")
            {
                botsList[0].ManualCalibration(Convert.ToDouble(manCalibBot1TB.Text));
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
        private void manCalibBot1TB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.manCalibBot1TB.Text))
            {
                this.manCalibBot1TB.Text = "";
                this.manCalibBot1TB.Focus();
            }
        }
        //******************BOT BUTTONS************************//
        //*****************************************************//
        private void resetCalibBT_Click(object sender, EventArgs e)
        {
            resetCalibBT.ForeColor = Color.Black;
            tickerLB.ForeColor = Color.Black;
            botsList[0].ResetCalibration();
            calibrationBot1Bar.Value = 0;
            lastperOpenTB.Clear();
            lastperOpenTB.Enabled = true;
            lastPerCloseTB.Clear();
            lastPerCloseTB.Enabled = true;
            tickerTB.Clear();
            tickerTB.Enabled = true;
            calibBot1.Enabled = true;
            calibrateBot1BT.Enabled = true;
            tickerTB.Focus();
        }
        private void saveParamsBot1BT_Click(object sender, EventArgs e)
        {
            countdown = 15;
            saveParamsBot1BT.ForeColor = Color.Lime;
            saveParamsBot1BT.Text = "Saved";
            timerBot1.Start();

            List<string> temp = DataBase.ConvertToListString(tickerTB.Text, calibSelectorScroll.Value, autoStartCheck.Checked);
            DataBase.SaveBotParameters(temp);
        }
        //******TIMER to control saved button change status******//
        //*****************************************************//
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
        //*****************CALIBRATION BUTTON******************//
        //*****************************************************//
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
                botsList[0].Calibrate();

                calibBot1.Start();
                calibrateBot1BT.Enabled = false;
                manCalibBot1TB.Enabled = false;
                lastperOpenTB.Enabled = false;
                lastPerCloseTB.Enabled = false;
                tickerTB.Enabled = false;

                rsiHistoryInput.Enabled = false;
                plot3Input.Enabled = false;
                renkoInput.Enabled = false;
            }
        }
        //*********TIMER FOR CALIBRATION LOAD BAR**************//
        //*****************************************************//
        private void calibBot1_Tick(object sender, EventArgs e)
        {
            if (botsList[0].GetBotActive() == true)
            {
                startBot1BT.Enabled = true;
                if (autoStartCheck.Checked) startBot1BT.PerformClick();
                calibBot1.Stop();
            }
            else
            {
                calibrationBot1Bar.Value = botsList[0].GetCalibrationBalance() >= calibrationBot1Bar.Maximum ?
                    calibrationBot1Bar.Maximum : botsList[0].GetCalibrationBalance();
            }
        }
        //***************START BOT OPERATIONS******************//
        //*****************************************************//
        private void startBot1BT_Click(object sender, EventArgs e)
        {
            robot1PB.BackgroundImage = Resources.robotIcon2;
            bot1Status.ForeColor = Color.Lime;
            bot1Status.Text = "CONNECTED";

            botsList[0].Connect(true); //connect bot actions (buy, sell, zero)

            //deactivate items
            startBot1BT.Enabled = false;
            calibrateBot1BT.Enabled = false;
        }
        //***************STOP BOT OPERATIONS ******************//
        //*****************************************************//
        private void stopBot1BT_Click(object sender, EventArgs e)
        {
            botsList[0].Connect(false); //disconnect bot actions (buy, sell, zero) but maintain the RSI history building

            robot1PB.BackgroundImage = Resources.robotIcon;
            bot1Status.ForeColor = Color.Red;
            bot1Status.Text = "DISCONNECTED";

            startBot1BT.Enabled = true;
        }
        #endregion

        #region CONFIGURATION PANEL
        private void opacityScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            SetOpacity(opacityScrollBar.Value);
        }
        private void topMostSelector_Scroll(object sender, ScrollEventArgs e)
        {
            TopMostApp(e.NewValue);
        }
        private void SetOpacity(int val)
        {
            winOpacity = (float)val / 10;
            this.Opacity = winOpacity;
            opacityScrollBar.Value = val;
        }
        private void TopMostApp(int val)
        {
            topMostSelector.Value = val;
            this.TopMost = val == 1 ? true : false;
            topMostLB.Text = val == 1 ? "ON" : "OFF";
        }
        #endregion
    }
}
