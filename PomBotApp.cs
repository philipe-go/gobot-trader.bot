using NDde.Client;
using Pombot_UI.RobotLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI
{
    public partial class PomBotAppForm : Form
    {
        #region DashBoard and DDE Attributes
        private DdeClient client;
        private DdeClient clientVWAP;
        private DdeClient clientHOR;
        private bool connectedDDE;
        private Dashboard dashB = Dashboard.GetInstance(); //access to the class Dashboard
        private bool buyKey = false;
        private bool sellkey = false;
        private bool zeroKey = false;
        private DateTime now;
        internal DateTime orderTime;
        #endregion

        #region Form Attributes 
        private float winOpacity = 1;
        public string user; //user name to be shown on the top part of the form
        private int mov, movY, movX; //variables to handle the Drag and Move method of the form 
        private List<Panel> menuItems = new List<Panel>(); //menu items - to handle which one will be on focus and activated
        private List<Bot> botsList = new List<Bot>(); //BOT LIST -->> check is will be possible to implement more than 1 bot
        internal static bool loadPossible = false;
        #endregion

        #region Bot parameters MultiThread
        internal delegate void UpdateTextBoxDelegate(string textBoxNewText);
        internal UpdateTextBoxDelegate textBoxDelegate; //delegate object
        internal delegate void UpdateVWAPDelegate(string vwapText);
        internal UpdateVWAPDelegate vwapDelegate;
        internal delegate void UpdateTableDelegate(string item2, double item3);
        internal UpdateTableDelegate updateTableDelegate; //delegate object
        private bool addTableItem;
        #endregion

        #region FORM INITIALIZATION
        public PomBotAppForm()
        {
            InitializeComponent();

            InitializeMainForm();
        }
        //timer used for connection checker and date update
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy - HH:mm:ss");
            usernameLB.Text = Program.userName;
            DDEupdateStatus();
        }
        private void InitializeMainForm()
        {
            dateLB.Text = DateTime.Now.ToString("dd/MMM/yyyy-HH:mm ");
            timer1.Start(); //Timer to control clock and DDE connection status update
            winNameLB.Text = Program.appName + " - V" + Program.appVersion.Major + "." + Program.appVersion.Minor; //Retrieve app name from Program.cs and set into UI
            versionLB.Text = $"Version: {Program.appVersion.ToString()}";
            //updateRSI = false;
            this.client = new DdeClient(dashB.app, dashB.service);
            this.clientVWAP = new DdeClient(dashB.app, dashB.service);
            this.clientHOR = new DdeClient(dashB.app, dashB.service);
            clientHOR.Disconnected += OnDisconnected;
            clientVWAP.Disconnected += OnDisconnected;
            client.Disconnected += OnDisconnected;

            #region Set native Font
            PomBot.LoadFont(this);
            #endregion

            #region BOTs initialization
            //TODO -> If not possible to instantiate more than 1 bot, remove list and create just one object
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
            //ConnectDDETask();
            //DDEupdateStatusAsync();
            DDEupdateStatus();
            UpdateDDEStrategy();

            //Delegates 
            StartMultiThread();

            //Keys Bindings
            InitializeKeys();

            calibrationBot1Bar.Maximum = 100;

            //Check for application Update
            Updater();
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
                strategyInput.Value = list[0];
                renkoTimeSelector.Value = list[1];
                dashB.renkoPeriod = list[2];
                dashB.upperRenkoPeriod = list[3];
                dashB.timeSpamPeriod = list[4];
                dashB.historySize = list[5];
                dashB.upperHistorySize = list[6];
                dashB.plot3Size = list[7];
                dashB.upperPlot3Size = list[8];
                dashB.plotExitSize = list[9];
                dashB.upperPlotExitSize = list[10];
                dashB.bollSize = list[11];
                dashB.upperBollSize = list[12];
                dashB.bollMeanSize = list[13];
                dashB.upperBollMeanSize = list[14];
                SetOpacity(list[15]);
                TopMostApp(list[16]);
            }
        }
        private void SetBotParam(List<string> list)
        {
            if (list.Count() > 0)
            {
                this.tickerTB.Text = list[0];
                this.autoStartCheck.Checked = list[1] == "True" ? true : false;
                this.replayScroll.Value = list[2] == "0" ? 0 : 1;
                this.zeroOpsSelectorInput.Value = list[3] == "0" ? 0 : 1;
                this.useVWAPCB.Checked = list[4] == "True" ? true : false;
                ReplaySelector();
            }
        }
        #endregion

        #region BOT panel initialization
        private void BOTsInit()
        {
            startBot1BT.Enabled = false;
        }
        #endregion

        #region Update Management
        private void Updater()
        {
            if (File.Exists(@".\GObot.msi")) File.Delete(@".\GObot.msi");
            if (Pombos.CheckUpdate())
            {
                howToBT.ForeColor = Color.Aqua;
                updateGB.Visible = true;
                downloadPB.Visible = false;
                installUpdateGB.Visible = false;
                updateVersionTB.Text = $"Version {Pombos.appversion} available";
            }
            else
            {
                howToBT.ForeColor = Color.Black;
                updateGB.Visible = false;
                installUpdateGB.Visible = false;
            }
        }
        private void updateBT_Click(object sender, EventArgs e) //Download Update
        {
            WebClient webclient = new WebClient();
            Uri uri = new Uri(Pombos.updateURL);
            string filename = $"GObot.msi";

            Thread thrd = new Thread(() =>
            {
                using (webclient = new WebClient())
                {
                    updateBT.Invoke((MethodInvoker)delegate { updateBT.Visible = false; });
                    downloadPB.Invoke((MethodInvoker)delegate { downloadPB.Visible = true; });

                    webclient.DownloadProgressChanged += DownloadProgressChanged;
                    webclient.DownloadFileCompleted += DownloadFileCompleted;
                    webclient.DownloadFileAsync(uri, filename);
                }
            });
            thrd.Start();
        }
        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Thread thrd = Thread.CurrentThread;
            downloadPB.Invoke((MethodInvoker)delegate { downloadPB.Value = 100; });
            howToBT.Invoke((MethodInvoker)delegate { howToBT.ForeColor = Color.Lime; });
            installUpdateGB.Invoke((MethodInvoker)delegate { installUpdateGB.Visible = true; });
            updateGB.Invoke((MethodInvoker)delegate { updateGB.Enabled = false; });

            thrd.Interrupt();
        }
        private void DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            howToBT.Invoke((MethodInvoker)delegate { howToBT.ForeColor = Color.Red; });
            downloadPB.Invoke((MethodInvoker)delegate { downloadPB.Maximum = 100; });
            downloadPB.Invoke((MethodInvoker)delegate { downloadPB.Value = e.ProgressPercentage; });
        }
        private void installUpdateBT_Click(object sender, EventArgs e)
        {
            string updateFile = @".\GObot.msi";
            System.Diagnostics.Process.Start(updateFile);
            Application.Exit();
        }
        #endregion
        #endregion

        #region DDE INTERFACE
        private bool ConnectDDE()
        {
            client.TryConnect();
            clientVWAP.TryConnect();
            clientHOR.TryConnect();
            if (!client.IsConnected)
            {
                connectedDDE = false;
                return false;
            }
            connectedDDE = true;
            botsList[0].SetProcess();
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
                client.StartAdvise($"{ticker}.{col}", 1, true, 10);
                client.Advise += OnAdvise;
                CalibrateBot(true);
            }
            catch (NDde.DdeException)
            {
                ddeCurrentMeasureBot1.ForeColor = Color.Red;
                ddeCurrentMeasureBot1.Text = "Error DDE";
                CalibrateBot(false);
            }
            finally
            {
                resetCalibBT.Focus();
            }
        }
        internal void AdviseVWAP(string ticker, string col)
        {
            try
            {
                clientVWAP.StartAdvise($"{ticker}.{col}", 1, true, 10);
                clientVWAP.Advise += OnAdviseVWAP;
                CalibrateBot(true);
            }
            catch (NDde.DdeException)
            {
                ddeVWAP.ForeColor = Color.Red;
                ddeVWAP.Text = "Error DDE";
                CalibrateBot(false);
            }
        }
        internal void AdviseHOR(string ticker, string col)
        {
            try
            {
                clientHOR.StartAdvise($"{ticker}.{col}", 1, true, 10);
                clientHOR.Advise += OnAdviseHOR;
                CalibrateBot(true);
            }
            catch (NDde.DdeException)
            {
                ddeVWAP.ForeColor = Color.Red;
                ddeVWAP.Text = "Error DDE";
                CalibrateBot(false);
            }
        }
        internal void OnAdvise(object sender, DdeAdviseEventArgs args)
        {
            botsList[0].Advise(float.Parse(args.Text) / 100);
            this.Invoke(textBoxDelegate, botsList[0].GetTemp()); //Delegate to update bot form current measure
            if (addTableItem)
            {
                this.Invoke(updateTableDelegate, botsList[0].GetAction(), botsList[0].GetPrice()); //Delegate to update operations table with last bot Operations
                AddTableItem(false);
            }
        }
        internal void OnAdviseVWAP(object sender, DdeAdviseEventArgs args)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ",";
            provider.NumberGroupSeparator = ".";
            botsList[0].AdviseVWAP(Convert.ToDouble(args.Text, provider));
            this.Invoke(vwapDelegate, botsList[0].GetVWAP()); //Delegate to update bot form current measure
            botsList[0].VWAPColour();
        }
        internal void OnAdviseHOR(object sender, DdeAdviseEventArgs args)
        {
            DateTime now = Convert.ToDateTime(args.Text);
            botsList[0].AdviseHOR(now);
            this.now = now;
        }
        #endregion

        #region MOVE WINDOW
        private void formNamePN_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movY = e.Y;
            movX = e.X;
            this.Opacity = 0.5f;
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
            this.Opacity = winOpacity;
        }
        #endregion

        #region BUTTONS HANDLER - HIGHLIGHT
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

        #region BUTTONS HANDLER - CLICK
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

            if (calibrateBot1BT.Enabled)
            {
                tickerTB.Focus();

                mainFracGroup.Text = $"{dashB.renkoPeriod} R";
                upperFracGroup.Text = renkoTimeSelector.Value == 0 ? $"{dashB.upperRenkoPeriod} R" : $"{(Convert.ToDouble(dashB.timeSpamPeriod) / 2)} min";
                SetZeroOps();

                botsList[0].SetUpperCurve(strategyInput.Value == 0 ? false : true, renkoTimeSelector.Value == 0 ? false : true);

                if (strategyInput.Value == 0)
                {
                    upperFracGroup.Visible = false;
                    upperBMean.Visible = false;
                    upperRSI.Visible = false;
                    upperRSIMean.Visible = false;
                    upperBoll.Visible = false;
                }
                else
                {
                    upperFracGroup.Visible = true;
                    upperBMean.Visible = true;
                    upperRSI.Visible = true;
                    upperRSIMean.Visible = true;
                    upperBoll.Visible = true;

                    upperFracCloseTB.Visible = renkoTimeSelector.Value == 0 ? true : false;
                    upperFracOpenTB.Visible = renkoTimeSelector.Value == 0 ? true : false;
                    upperRSIMean.Visible = renkoTimeSelector.Value == 0 ? true : false;
                    upperLB1.Visible = renkoTimeSelector.Value == 0 ? true : false;
                    upperLB2.Visible = renkoTimeSelector.Value == 0 ? true : false;

                    if (renkoTimeSelector.Value == 1)
                    {
                        botsList[0].InitialUpperBrick(5000);
                        botsList[0].FinalUpperBrick(5000);
                    }
                }
            }
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

        #region MULTITHREAD OPERATIONS
        private void StartMultiThread()
        {
            textBoxDelegate = new UpdateTextBoxDelegate(UpdateCurrentMeasure);
            vwapDelegate = new UpdateVWAPDelegate(UpdateVWAP);
            botsList[0].Strategy.MainCurve.OnCurrentMeasuresUpdate += UpdateMainRSIandPlot;
            botsList[0].Strategy.UpperCurve.OnCurrentMeasuresUpdate += UpdateUpperRSIandPlot;
            updateTableDelegate = new UpdateTableDelegate(UpdateTableItems);
        }
        private void UpdateCurrentMeasure(string text)
        {
            ddeCurrentMeasureBot1.Text = text;
        }
        private void UpdateVWAP(string text)
        {
            ddeVWAP.ForeColor = Color.Black;
            ddeVWAP.Text = text;
        }
        private void UpdateMainRSIandPlot()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    ddeRSIMeasureBot1.Text = botsList[0].Strategy.MainCurve.CurrentRSI.ToString("0.00");
                    ddePlotMeasureBot1.Text = botsList[0].Strategy.MainCurve.CurrentPlot.ToString("0.00");
                    ddeBBMeasureBot1.Text = botsList[0].Strategy.MainCurve.CurrentBB.ToString("0.00");
                    ddeBBMeanMeasureBot1.Text = botsList[0].Strategy.MainCurve.CurrentBollMean.ToString("0.00");
                    MeasuresColoring("main");
                });
            }
            catch (ObjectDisposedException e)
            {
            }
        }
        private void UpdateUpperRSIandPlot()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    upperRSI.Text = botsList[0].Strategy.UpperCurve.CurrentRSI.ToString("0.00");
                    upperRSIMean.Text = botsList[0].Strategy.UpperCurve.CurrentPlot.ToString("0.00");
                    upperBoll.Text = botsList[0].Strategy.UpperCurve.CurrentBB.ToString("0.00");
                    upperBMean.Text = botsList[0].Strategy.UpperCurve.CurrentBollMean.ToString("0.00");
                    MeasuresColoring("upper");
                });
            }
            catch (ObjectDisposedException e)
            {
            }
        }
        private void MeasuresColoring(string curve)
        {
            if (curve == "main")
            {
                //RSI
                ddeRSIMeasureBot1.ForeColor = (Convert.ToDouble(ddeRSIMeasureBot1.Text) > 50) && (Convert.ToDouble(ddeRSIMeasureBot1.Text) > Convert.ToDouble(ddePlotMeasureBot1.Text)) ? Color.Lime :
                   (Convert.ToDouble(ddeRSIMeasureBot1.Text) < 50) && (Convert.ToDouble(ddeRSIMeasureBot1.Text) < Convert.ToDouble(ddePlotMeasureBot1.Text)) ? Color.Red : Color.Black;
                //Bollinger
                if (Convert.ToDouble(ddeBBMeasureBot1.Text) > Convert.ToDouble(ddeBBMeanMeasureBot1.Text)) { ddeBBMeasureBot1.ForeColor = Color.Aqua; ddeBBMeanMeasureBot1.ForeColor = Color.Black; }
                else { ddeBBMeasureBot1.ForeColor = Color.Black; ddeBBMeanMeasureBot1.ForeColor = Color.Aqua; }
            }
            else 
            {
                //RSI
                upperRSI.ForeColor = (Convert.ToDouble(upperRSI.Text) > 50) ?  Color.Lime : Color.Red;
                //Bollinger
                if (Convert.ToDouble(upperBoll.Text) > Convert.ToDouble(upperBMean.Text)) { upperBoll.ForeColor = Color.Aqua; upperBMean.ForeColor = Color.Black; }
                else { upperBoll.ForeColor = Color.Black; upperBMean.ForeColor = Color.Aqua; }
            }
        }
        private void UpdateTableItems(string item2, double item3)
        {
            ListViewItem newItem = new ListViewItem($"{orderTime.ToString("HH:mm:ss")}");
            newItem.SubItems.Add(item2);
            newItem.SubItems.Add(item3.ToString("0.00"));

            bot1Operations.Items.Insert(0, newItem);
        }
        internal void AddTableItem(bool val)
        {
            this.addTableItem = val;
        }
        #endregion

        #region partial methods
        partial void lastPerCloseTB_TextChanged(object sender, EventArgs e);
        #endregion
    }
}
