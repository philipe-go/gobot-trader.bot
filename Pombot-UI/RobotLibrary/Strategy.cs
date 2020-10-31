using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Xml;

namespace Pombot_UI.RobotLibrary
{
    sealed class Strategy
    {
        #region Strategy Attributes
        private Bot myBot;
        private StrategyCurve mainCurve;
        internal StrategyCurve MainCurve { get => mainCurve; }
        private StrategyCurve upperCurve;
        internal StrategyCurve UpperCurve { get => upperCurve; }
        #endregion

        #region Curves Delegate Event
        internal delegate void BuildCurveDelegate();
        internal event BuildCurveDelegate OnBuildCurve;
        #endregion

        #region Bot Attributes
        internal bool botActive;
        #endregion

        #region Strategy Options
        private bool isBought = false;
        private bool isSold = false;
        private bool useVWAP = false;
        private bool firstPass = true;
        private bool zeroWithBollinger;
        #endregion

        #region MainForm Table Update Handler
        internal string action;
        internal double price;
        #endregion

        #region Win32 foreground application Manager
        private IntPtr prof;
        private string outp = "";

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr point);
        #endregion

        #region Encapsulation
        internal void MaxCurve()
        {
            if (upperCurve != null)
            {
                upperCurve.MaxCurve = GetUpperInitialBrick() < GetUpperFinalBrick() ? true : false;
                mainCurve.MaxCurve = GetInitialBrick() < GetFinalBrick() ? true : false;
            }
            else
            {
                mainCurve.MaxCurve = GetInitialBrick() < GetFinalBrick() ? true : false;
            }
        }
        internal double GetInitialBrick()
        {
            return mainCurve.BrickInitial;
        }
        internal double GetFinalBrick()
        {
            return mainCurve.BrickFinal;
        }
        internal double GetUpperInitialBrick()
        {
            return upperCurve != null ? upperCurve.BrickInitial : 1;
        }
        internal double GetUpperFinalBrick()
        {
            return upperCurve != null ? upperCurve.BrickFinal : 1;
        }
        internal void SetProcess()
        {
            prof = FindWindow("ProfitPro", null);
            SetForegroundWindow(prof);
        }
        internal int CalibrationBallance()
        {
            if (upperCurve != null) return (mainCurve.CalibBalance() + upperCurve.CalibBalance()) / 2;
            return mainCurve.CalibBalance();
        }
        internal string CalibrationString()
        {
            if (upperCurve != null)
                return $"Main Curve: {mainCurve.CalibBalance().ToString("00")}% \nSupport Curve: {upperCurve.CalibBalance().ToString("00")}%";
            return $"Main Curve: {mainCurve.CalibBalance().ToString("00")}%";
        }
        internal bool GetCurveBuilt(string i)
        {
            if (i != "main" && upperCurve != null) return upperCurve.CurveBuilt;
            return mainCurve.CurveBuilt;
        }
        internal void SetZeroWithBollinger(bool val)
        {
            this.zeroWithBollinger = val;
        }
        internal void StartedBot()
        {
            firstPass = ((this.now.Hour == 9 && this.now.Minute < 10)) ? true : false;
        }
        #endregion

        #region Constructor and Setters
        internal Strategy(Bot aBot)
        {
            refreshtemp = false;
            botActive = false;
            useVWAP = false;
            firstPass = true;
            zeroWithBollinger = aBot.zeroOpt;

            myBot = aBot;
            mainCurve = new StrategyCurve();
            upperCurve = new StrategyCurve();
        }

        internal void UpdateCurves()
        {
            mainCurve.UpdateParametres(true, myBot.dashB.renkoPeriod, myBot.dashB.historySize, myBot.dashB.plot3Size, myBot.dashB.plotExitSize, myBot.dashB.bollSize, myBot.dashB.bollMeanSize, this);

            if (upperCurve != null)
            {
                if (myBot.mainForm.renkoTimeSelector.Value == 0) upperCurve.UpdateParametres(true, myBot.dashB.upperRenkoPeriod, myBot.dashB.upperHistorySize, myBot.dashB.upperPlot3Size, myBot.dashB.upperPlotExitSize, myBot.dashB.upperBollSize, myBot.dashB.upperBollMeanSize, this);
                else upperCurve.UpdateParametres(false, myBot.dashB.timeSpamPeriod, myBot.dashB.upperHistorySize, myBot.dashB.upperPlot3Size, myBot.dashB.upperPlotExitSize, myBot.dashB.upperBollSize, myBot.dashB.upperBollMeanSize, this);
            }
        }
        internal void SetUpperCurve(bool val, bool isRenko)
        {
            if (!val) upperCurve = null;
            else
            {
                if (upperCurve == null) upperCurve = new StrategyCurve();
                upperCurve.IsRenko = isRenko;
            }
        }
        internal void Reset()
        {
            mainCurve.Reset();
            if (upperCurve != null) upperCurve.Reset();

            botActive = false;
            refreshtemp = true;
        }
        #endregion

        #region DDE

        #region DDE Checkers & Param
        private bool refreshtemp;
        internal bool Refreshtemp { get => refreshtemp; }
        private double temp;
        internal double Temp { get => temp; set => temp = value; }
        private double vwap;
        internal double Vwap { get => vwap; }
        private DateTime now;
        internal DateTime Now { get => now; }
        #endregion

        internal void Advise(double temp)
        {
            if (refreshtemp) this.temp = temp;
            else CurvesCallBack();
            refreshtemp = true;
        }
        internal void AdviseVWAP(double temp)
        {
            this.vwap = temp;
        }
        internal void AdviseHOR(DateTime now)
        {
            this.now = now;
        }
        #endregion

        #region SaveData
        internal List<Queue<decimal>> CalibQueue()
        {
            List<Queue<decimal>> templist = new List<Queue<decimal>>();
            templist.Add(mainCurve.PeriodsPrices);
            templist.Add(mainCurve.MaxPeriods);
            templist.Add(mainCurve.MinPeriods);
            templist.Add(mainCurve.ThreePerMean);
            templist.Add(mainCurve.ExitPerMean);
            templist.Add(mainCurve.BollMovMean);
            if (upperCurve == null) upperCurve = new StrategyCurve();
            templist.Add(upperCurve.PeriodsPrices);
            templist.Add(upperCurve.MaxPeriods);
            templist.Add(upperCurve.MinPeriods);
            templist.Add(upperCurve.ThreePerMean);
            templist.Add(upperCurve.ExitPerMean);
            templist.Add(upperCurve.BollMovMean);

            return templist;
        }
        internal List<decimal> CalibDoubles()
        {
            List<decimal> templist = new List<decimal>();
            templist.Add(Convert.ToDecimal(mainCurve.BrickInitial));
            templist.Add(Convert.ToDecimal(mainCurve.BrickFinal));
            templist.Add(mainCurve.LowMean);
            templist.Add(mainCurve.HighMean);
            templist.Add(mainCurve.RsiMean);
            templist.Add(mainCurve.Plot3Mean);
            templist.Add(mainCurve.PlotExitMean);
            templist.Add(mainCurve.BbMean);
            templist.Add(mainCurve.BbTop);
            templist.Add(mainCurve.BbLow);
            templist.Add(mainCurve.BbWidth);
            templist.Add(mainCurve.BbMovMean);
            if (upperCurve == null) upperCurve = new StrategyCurve();
            templist.Add(Convert.ToDecimal(upperCurve.BrickInitial));
            templist.Add(Convert.ToDecimal(upperCurve.BrickFinal));
            templist.Add(upperCurve.LowMean);
            templist.Add(upperCurve.HighMean);
            templist.Add(upperCurve.RsiMean);
            templist.Add(upperCurve.Plot3Mean);
            templist.Add(upperCurve.PlotExitMean);
            templist.Add(upperCurve.BbMean);
            templist.Add(upperCurve.BbTop);
            templist.Add(upperCurve.BbLow);
            templist.Add(upperCurve.BbWidth);
            templist.Add(upperCurve.BbMovMean);


            return templist;
        }
        internal void ReadCalib(List<decimal> doubles, List<Queue<decimal>> queues)
        {
            mainCurve.PeriodsPrices = queues[0];
            mainCurve.MaxPeriods = queues[1];
            mainCurve.MinPeriods = queues[2];
            mainCurve.ThreePerMean = queues[3];
            mainCurve.ExitPerMean = queues[4];
            mainCurve.BollMovMean = queues[5];

            mainCurve.BrickInitial = Convert.ToDouble(doubles[0]);
            mainCurve.BrickFinal = Convert.ToDouble(doubles[1]);
            mainCurve.LowMean = doubles[2];
            mainCurve.HighMean = doubles[3];
            mainCurve.RsiMean = doubles[4];
            mainCurve.Plot3Mean = doubles[5];
            mainCurve.PlotExitMean = doubles[6];
            mainCurve.BbMean = doubles[7];
            mainCurve.BbTop = doubles[8];
            mainCurve.BbLow = doubles[9];
            mainCurve.BbWidth = doubles[10];
            mainCurve.BbMovMean = doubles[11];

            mainCurve.MaxCurve = (mainCurve.BrickFinal - mainCurve.BrickInitial > 0) ? true : false;
            mainCurve.HistoryComplete = (mainCurve.MaxPeriods.Count() == myBot.dashB.historySize) ? true : false;

            if (upperCurve != null)
            {
                upperCurve.PeriodsPrices = queues[6];
                upperCurve.MaxPeriods = queues[7];
                upperCurve.MinPeriods = queues[8];
                upperCurve.ThreePerMean = queues[9];
                upperCurve.ExitPerMean = queues[10];
                upperCurve.BollMovMean = queues[11];

                upperCurve.BrickInitial = Convert.ToDouble(doubles[12]);
                upperCurve.BrickFinal = Convert.ToDouble(doubles[13]);
                upperCurve.LowMean = doubles[14];
                upperCurve.HighMean = doubles[15];
                upperCurve.RsiMean = doubles[16];
                upperCurve.Plot3Mean = doubles[17];
                upperCurve.PlotExitMean = doubles[18];
                upperCurve.BbMean = doubles[19];
                upperCurve.BbTop = doubles[20];
                upperCurve.BbLow = doubles[21];
                upperCurve.BbWidth = doubles[22];
                upperCurve.BbMovMean = doubles[23];

                upperCurve.MaxCurve = (upperCurve.BrickFinal - upperCurve.BrickInitial > 0) ? true : false;
                upperCurve.HistoryComplete = (upperCurve.MaxPeriods.Count() == myBot.dashB.upperHistorySize) ? true : false;
            }
            //this.botActive = historyComplete == true ? true : false;
        }
        #endregion

        #region Calibration
        internal void AutoEntryOpen(double brickOpen)
        {
            mainCurve.BrickInitial = brickOpen;
        }
        internal void AutoEntryClose(double brickClose)
        {
            mainCurve.BrickFinal = brickClose;
        }
        internal void UpperOpenBrick(double openBrick)
        {
            upperCurve.BrickInitial = openBrick;
        }
        internal void UpperCloseBrick(double closeBrick)
        {
            upperCurve.BrickFinal = closeBrick;
        }
        internal Task CurvesCallBack()
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    OnBuildCurve?.Invoke();
                    if (mainCurve.CurveBuilt && (upperCurve != null ? upperCurve.CurveBuilt : true)) StrategyProcess();
                    Thread.Sleep(10);
                }
            });
        } //Curves Build CallBack to be called as a new Task
        #endregion

        #region Strategy Actions
        private void StrategyProcess() //Plot 3 Curve Build and post-RSI
        {
            if (myBot.activated) CallStrategyAction();
            botActive = true;
        }
        private void KeyBoardOutput(List<Keys> listK)
        {
            prof = FindWindow(null, "Profit");
            SetForegroundWindow(prof);
            outp = "";

            SendKeys.SendWait("{ESC}");
            switch (listK[0])
            {
                case (Keys.Shift):
                    {
                        if (listK.Count() > 1)
                        {
                            for (int i = 1; i < listK.Count(); i++)
                            {
                                outp += listK[i].ToString();
                            }
                            SendKeys.SendWait("+" + $"{outp}");
                        }
                        else
                        {
                            SendKeys.SendWait("+");
                        }
                    }
                    break;
                case (Keys.Control):
                    {
                        if (listK.Count() > 1)
                        {
                            for (int i = 1; i < listK.Count(); i++)
                            {
                                outp += listK[i].ToString();
                            }
                            SendKeys.SendWait("^" + $"{outp}");
                        }
                        else
                        {
                            SendKeys.SendWait("^");
                        }
                    }
                    break;
                case (Keys.Alt):
                    {
                        if (listK.Count() > 1)
                        {
                            for (int i = 1; i < listK.Count(); i++)
                            {
                                outp += listK[i].ToString();
                            }
                            SendKeys.SendWait("%" + $"{outp}");
                        }
                        else
                        {
                            SendKeys.SendWait("%");
                        }
                    }
                    break;
                default:
                    {
                        if (listK.Count() > 1)
                        {
                            for (int i = 0; i < listK.Count(); i++)
                            {
                                outp += listK[i].ToString();
                            }
                            SendKeys.SendWait($"{outp}");
                        }
                        else
                        {
                            SendKeys.SendWait($"{listK[0].ToString()}");
                        }
                    }
                    break;
            }
            SendKeys.SendWait("{ESC}");
            outp = "";
        }
        private void CallStrategyAction()
        {
            if (BollingerCheck())
            {
                if ((!isBought) && (!isSold) && (mainCurve.RsiMean >= 50) && (mainCurve.RsiMean > mainCurve.Plot3Mean) && CheckVWAP("buy") && CheckUpperRSI("buy")) //buy
                {
                    KeyBoardOutput(myBot.dashB.buyKeyboard);
                    LogAction("Buy");
                    isBought = true;
                    isSold = false;
                    myBot.mainForm.ddeCurrentMeasureBot1.ForeColor = Color.Lime;
                    PlayeSound();
                }
                else if ((!isSold) && (!isBought) && (mainCurve.RsiMean < 50) && (mainCurve.RsiMean < mainCurve.Plot3Mean) && CheckVWAP("sell") && CheckUpperRSI("sell")) //sell 
                {
                    KeyBoardOutput(myBot.dashB.sellKeyboard);
                    LogAction("Sell");
                    isBought = false;
                    isSold = true;
                    myBot.mainForm.ddeCurrentMeasureBot1.ForeColor = Color.Red;
                    PlayeSound();
                }
            }
            else if (!BollingerMainCheck())
            {
                if (zeroWithBollinger) //zero on bollinger
                {
                    if (isSold != isBought) //zero
                    {
                        KeyBoardOutput(myBot.dashB.zeroKeyboard);
                        LogAction("Zero");
                        isBought = false;
                        isSold = false;
                        myBot.mainForm.ddeCurrentMeasureBot1.ForeColor = Color.Black;
                        PlayeSound();
                    }
                }
                else //zero on RSI + bollinger
                {
                    if ((isBought) && (mainCurve.RsiMean < mainCurve.PlotExitMean)) //zero when isBought
                    {
                        KeyBoardOutput(myBot.dashB.zeroKeyboard);
                        LogAction("Zero");
                        isBought = false;
                        isSold = false;
                        myBot.mainForm.ddeCurrentMeasureBot1.ForeColor = Color.Black;
                        PlayeSound();
                    }
                    else if ((isSold) && (mainCurve.RsiMean > mainCurve.PlotExitMean)) //zero when isSold
                    {
                        KeyBoardOutput(myBot.dashB.zeroKeyboard);
                        LogAction("Zero");
                        isBought = false;
                        isSold = false;
                        myBot.mainForm.ddeCurrentMeasureBot1.ForeColor = Color.Black;
                        PlayeSound();
                    }
                }
            }
        }//StrategyAction Method
        private void LogAction(string action)
        {
            this.action = action;
            price = temp;
            myBot.mainForm.AddTableItem(true);
            myBot.mainForm.orderTime = now;
        }
        private Task PlayeSound()
        {
            return Task.Factory.StartNew(() =>
            {
                System.IO.Stream sound = Properties.Resources.moneyCashier;
                SoundPlayer player = new SoundPlayer(sound);
                player.Play();
            });
        }
        #endregion

        #region Bollinger Strategy
        private bool BollingerCheck()
        {
            return BollingerMainCheck() && (upperCurve != null ? BollingerUpperCheck() : true);
        }
        private bool BollingerMainCheck()
        {
            return mainCurve.BbWidth > mainCurve.BbMovMean;
        }
        private bool BollingerUpperCheck()
        {
            //if (firstPass) //Means bot was started before the time set inside the method "StartedBot()"
            //{
            //    if (upperCurve.BbWidth < upperCurve.BbMovMean) firstPass = false;
            //    return upperCurve.CurrentBB <= Convert.ToDecimal(0.47) && upperCurve.CurrentBB > upperCurve.CurrentBollMean;
            //}
            //else if (upperCurve.CurrentBB > Convert.ToDecimal(1.0)) return false;
            return upperCurve.CurrentBB > upperCurve.CurrentBollMean;
        }
        #endregion

        #region VWAP
        internal void SetVWAP(bool val)
        {
            useVWAP = val;
        }
        private bool CheckVWAP(string state)
        {
            if (!useVWAP) return true;
            return state == "buy" ? temp > vwap : temp < vwap;
        }
        internal void VWAPColour()
        {
            if (useVWAP) myBot.mainForm.ddeVWAP.ForeColor = GetFinalBrick() > vwap ? Color.Lime : Color.Red;
            else myBot.mainForm.ddeVWAP.ForeColor = Color.Transparent;
        }
        #endregion

        #region Check Upper RSI
        private bool CheckUpperRSI(string state)
        {
            if (upperCurve == null) return true;
            return state == "buy" ? upperCurve.CurrentRSI > 50 : upperCurve.CurrentRSI < 50;
        }
        #endregion
    }//Class Strategy
}//Namespace
