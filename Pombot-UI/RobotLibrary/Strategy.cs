using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Pombot_UI.RobotLibrary
{
    internal sealed class Strategy
    {
        //private Dashboard dashB;
        private Bot myBot;

        private double currentRSI;

        #region Curve Attributes
        private bool maxCurve;
        private bool historyComplete;
        private Queue<double> periods = new Queue<double>();
        internal bool manualCalibration;
        internal bool botActive;

        private double brickInitial;
        private double brickFinal;
        private Queue<double> maxPeriods = new Queue<double>();
        private Queue<double> minPeriods = new Queue<double>();

        private double manCalibTemp;
        private Stack<double> manCalib = new Stack<double>(); //stack instance to handle backwards period insertion
        #endregion

        #region Strategy Attributes
        private double lowMean = 0;
        private double highMean = 0;
        private double rsiMean = 0;
        private double plot3Mean = 0;
        private bool firstPass = true;
        private Queue<double> threePerMean = new Queue<double>();
        #endregion

        #region Strategy Options
        private bool isBought = false;
        private bool isSold = false;
        #endregion

        #region Strategy Bool Checkers
        internal bool refreshtemp;
        internal double temp;
        private double renkoPeriod;
        #endregion

        #region MainForm Table Update Handler
        internal string action;
        internal double price;
        #endregion

        #region Win32 foreground application Manager
        private IntPtr prof;
        string outp = "";

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr point);
        #endregion

        #region Encapsulation
        internal void MaxCurve(bool val)
        {
            this.maxCurve = val;
        }
        internal int GetHistorySize()
        {
            return manCalib.Count();
        }
        internal double GetInitialBrick()
        {
            return brickInitial;
        }
        internal double GetFinalBrick()
        {
            return brickFinal;
        }
        internal void SetBot(Bot obj)
        {
            this.myBot = obj;
        }
        internal void SetProcess()
        {
            prof = FindWindow("ProfitPro", null);
            SetForegroundWindow(prof);
            //this.profit = Process.GetProcessesByName("profitchart.exe").FirstOrDefault();
        }
        internal int CalibrationBallance()
        {
            return maxPeriods.Count() + threePerMean.Count();
        }
        internal double RsiMean {  get => this.rsiMean; }
        internal double Plot3Mean { get => this.plot3Mean; }
        #endregion

        #region Constructor
        internal Strategy()
        {
            historyComplete = false;
            refreshtemp = false;
            botActive = false;
        }
        #endregion

        internal void Reset()
        {
            manCalib.Clear();
            brickInitial = 0;
            brickFinal = 0;
        }

        #region DDE
        internal void Advise(float temp) //TO DO ---> this method is being accessed through the mainform via BOT object
        {
            if (refreshtemp) this.temp = temp;
            renkoPeriod = (Convert.ToDouble(myBot.dashB.renkoPeriod) / 2) - 0.5;
            RSICurve();
            refreshtemp = true;
        }
        #endregion

        #region Calibration
        internal void ManualEntry(double brick)
        {
            manCalib.Push(brick);
        }
        internal void AutoEntryOpen(double brickOpen)
        {
            brickInitial = brickOpen;
        }
        internal void AutoEntryClose(double brickClose)
        {
            brickFinal = brickClose;
        }
        internal void RSICurve() //RSI Curve Build
        {
            if (!manualCalibration)
            {
                if (temp - brickFinal > renkoPeriod) //complete period for ascending curve
                {
                    if (maxCurve) //ascending curve
                    {
                        brickInitial += renkoPeriod;
                        brickFinal = brickInitial + renkoPeriod;

                        maxPeriods.Enqueue(renkoPeriod);
                        minPeriods.Enqueue(0);
                        periods.Enqueue(brickFinal);
                        if (periods.Count() > 2) periods.Dequeue();
                        if (minPeriods.Count() > myBot.dashB.historySize) minPeriods.Dequeue();
                        if (maxPeriods.Count() > myBot.dashB.historySize) maxPeriods.Dequeue();

                        if (historyComplete) StrategyProcess();
                    }
                    else if (!maxCurve) //reversion of descending curve point
                    {
                        if (temp - brickInitial > renkoPeriod)
                        {
                            maxCurve = true;
                            brickFinal = brickInitial + renkoPeriod;

                            maxPeriods.Enqueue(2 * renkoPeriod);
                            minPeriods.Enqueue(0);
                            periods.Enqueue(brickFinal);
                            if (periods.Count() > 2) periods.Dequeue();
                            if (minPeriods.Count() > myBot.dashB.historySize) minPeriods.Dequeue();
                            if (maxPeriods.Count() > myBot.dashB.historySize) maxPeriods.Dequeue();

                            if (historyComplete) StrategyProcess();
                        }
                    }
                }
                if (temp - brickFinal < -renkoPeriod) //complete period for descending curve
                {
                    if (maxCurve) //reversion point of ascending curve
                    {
                        if (temp - brickInitial < -renkoPeriod)
                        {
                            maxCurve = false;
                            brickFinal = brickInitial - renkoPeriod;

                            minPeriods.Enqueue(2 * renkoPeriod);
                            maxPeriods.Enqueue(0);
                            periods.Enqueue(brickFinal);
                            if (periods.Count() > 2) periods.Dequeue();
                            if (minPeriods.Count() > myBot.dashB.historySize) minPeriods.Dequeue();
                            if (maxPeriods.Count() > myBot.dashB.historySize) maxPeriods.Dequeue();

                            if (historyComplete) StrategyProcess();
                        }
                    }
                    else if (!maxCurve) //descending curve
                    {
                        brickInitial -= renkoPeriod;
                        brickFinal = brickInitial - renkoPeriod;

                        minPeriods.Enqueue(renkoPeriod);
                        maxPeriods.Enqueue(0);
                        periods.Enqueue(brickFinal);
                        if (periods.Count() > 2) periods.Dequeue();
                        if (minPeriods.Count() > myBot.dashB.historySize) minPeriods.Dequeue();
                        if (maxPeriods.Count() > myBot.dashB.historySize) maxPeriods.Dequeue();

                        if (historyComplete) StrategyProcess();
                    }
                }
            }//Automatic Calibration

            else
            {
                while (manCalib.Count != 0)
                {
                    manCalibTemp = manCalib.Peek();
                    manCalib.Pop();
                    if (manCalibTemp - manCalib.Peek() >= 0)
                    {
                        maxPeriods.Enqueue(manCalibTemp - manCalib.Peek());
                        minPeriods.Enqueue(0);
                        maxCurve = true;
                    }
                    else
                    {
                        minPeriods.Enqueue(Math.Abs(manCalibTemp - manCalib.Peek()));
                        maxPeriods.Enqueue(0);
                        maxCurve = false;
                    }
                    if (manCalib.Count == 1)
                    {
                        periods.Enqueue(manCalib.Peek());
                    }
                }
                if (historyComplete) StrategyProcess();
                manualCalibration = false;
            }//Manual Calibration

            //StrategyProcess Call
            historyComplete = (maxPeriods.Count() == myBot.dashB.historySize) ? true : false;

        } //RSI Curve Method

        private void StrategyProcess() //Plot 3 Curve Build and post-RSI
        {
            if (firstPass)
            {
                highMean = maxPeriods.Sum() / myBot.dashB.historySize; //can use maxPeriods.Average() from LinQ
                lowMean = minPeriods.Sum() / myBot.dashB.historySize; //can use maxPeriods.Average() from LinQ
            }
            else
            {
                highMean = (highMean * (myBot.dashB.historySize - 1) / myBot.dashB.historySize) + (periods.Last() - periods.First() > 0 ? periods.Last() - periods.First() : 0) / myBot.dashB.historySize; //mean of MaxPeriods
                lowMean = (lowMean * (myBot.dashB.historySize - 1) / myBot.dashB.historySize) + (periods.Last() - periods.First() < 0 ? Math.Abs(periods.Last() - periods.First()) : 0) / myBot.dashB.historySize; //mean of MinPeriods 
            }

            Math.Round(highMean, 2);
            Math.Round(lowMean, 2);

            rsiMean = (lowMean != 0) ? 100 - (100 / (1 + (highMean / lowMean))) : (highMean != 0) ? 100 : 50; //RSI index for the historySize (N periods)

            Math.Round(rsiMean, 2);

            threePerMean.Enqueue(rsiMean);
            if (threePerMean.Count() > myBot.dashB.plot3Size)
            {
                threePerMean.Dequeue();
                plot3Mean = threePerMean.Average();
                if (myBot.activated) CallStrategyAction();
                myBot.mainForm.UpdateRSI(true);
                botActive = true;
            }

            firstPass = false;
        }
        #endregion

        #region Strategy
        private void KeyBoardOutput(List<Keys> listK)
        {
            prof = FindWindow("ProfitPro", null);
            SetForegroundWindow(prof);
            outp = "";
            SendKeys.Send("{ESC}");
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
            outp = "";
            SendKeys.Send("{ESC}");
        }

        private void CallStrategyAction()
        {
            if (rsiMean > 50) //Condition 1 and 2 for Buy
            {
                if (rsiMean > plot3Mean) //buy
                {
                    if (!isBought)
                    {
                        action = "Buy";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);

                        KeyBoardOutput(myBot.dashB.buyKeyboard);
                    }
                    isBought = true;
                    isSold = false;
                }
                else //zero position
                {
                    if (isBought)
                    {
                        action = "Zero";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);

                        KeyBoardOutput(myBot.dashB.zeroKeyboard);
                    }
                    isBought = false;
                    isSold = false;
                }
            }

            else if (rsiMean < 50) //Condition 1 and 2 for Sell
            {
                if (rsiMean < plot3Mean) //sell
                {
                    if (!isSold)
                    {
                        action = "Sell";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);

                        KeyBoardOutput(myBot.dashB.sellKeyboard);
                    }
                    isBought = false;
                    isSold = true;
                }
                else //zero position
                {
                    if (isSold)
                    {
                        action = "Zero";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);

                        KeyBoardOutput(myBot.dashB.zeroKeyboard);
                    }
                    isBought = false;
                    isSold = false;
                }
            }

            // if (myBot.dashB.inversionStrategy)
        
        }//StrategyAction Method
        
        internal double CurrentRSI()
        {
            double tempHigh, tempLow;
            tempHigh = (highMean * (myBot.dashB.historySize - 1) / myBot.dashB.historySize) + (temp - periods.Last() > 0 ? temp - periods.Last() : 0) / myBot.dashB.historySize; //mean of MaxPeriods
            tempLow = (lowMean * (myBot.dashB.historySize - 1) / myBot.dashB.historySize) + (temp - periods.Last() < 0 ? Math.Abs(temp - periods.Last()) : 0) / myBot.dashB.historySize; //mean of MinPeriods 
            Math.Round(tempHigh, 2);
            Math.Round(tempLow, 2);

            currentRSI = (tempLow != 0) ? 100 - (100 / (1 + (tempHigh / tempLow))) : 50;

            Math.Round(currentRSI, 2);
            return currentRSI;
        }
        internal double CurrentPlot()
        {
            return ((threePerMean.Sum() - threePerMean.First()) + currentRSI) / 3;
        }
        #endregion

    }//Class Strategy

}//Namespace
