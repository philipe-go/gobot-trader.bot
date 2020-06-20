using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Pombot_UI.RobotLibrary
{
    class Strategy
    {
        private Dashboard dashB;

        internal Bot myBot;

        #region Curve Variables
        private bool maxCurve;
        private bool historyComplete;
        internal bool botActive;
        internal bool manualCalibration;
        internal Queue<double> periods = new Queue<double>();
        
        private double brickInitial;
        private double brickFinal;
        internal Queue<double> maxPeriods = new Queue<double>();
        internal Queue<double> minPeriods = new Queue<double>();
        
        private double manCalibTemp;
        private Stack<double> manCalib = new Stack<double>(); //stack instance to handle backwards period insertion
        #endregion

        #region Strategy Variables
        private double lowMean = 0;
        private double highMean = 0;
        internal double rsiMean = 0;
        internal double plot3Mean = 0;
        private bool firstPass = true;
        internal Queue<double> threePerMean = new Queue<double>();
        #endregion

        #region Strategy Options
        private bool isBought = false;
        private bool isSold = false;
        internal bool useInversion = false;
        #endregion

        internal bool refreshtemp;
        internal double temp;
        private double renkoPeriod;

        internal string action;
        internal double price;

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
        #endregion

        #region Constructor
        internal Strategy()
        {
            dashB = Dashboard.GetInstance();
            historyComplete = false;
            refreshtemp = false;
            botActive = false;
        }
        #endregion

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
        internal void Reset()
        {
            manCalib.Clear();
            brickInitial = 0;
            brickFinal = 0;
        }
        internal void Advise(float temp)
        {
            if (refreshtemp) this.temp = temp;
            renkoPeriod = ((dashB.renkoPeriod / 2) - 0.5);
            RSICurve();
            refreshtemp = true;
        }

        internal void RSICurve()
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
                        if (minPeriods.Count() > dashB.historySize) minPeriods.Dequeue();
                        if (maxPeriods.Count() > dashB.historySize) maxPeriods.Dequeue();

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
                            if (minPeriods.Count() > dashB.historySize) minPeriods.Dequeue();
                            if (maxPeriods.Count() > dashB.historySize) maxPeriods.Dequeue();

                            //Console.WriteLine("===> Reverse Point from LOW to HIGH);
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
                            if (minPeriods.Count() > dashB.historySize) minPeriods.Dequeue();
                            if (maxPeriods.Count() > dashB.historySize) maxPeriods.Dequeue();

                            //Console.WriteLine("<=== Reverse Point from HIGH  to LOW);
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
                        if (minPeriods.Count() > dashB.historySize) minPeriods.Dequeue();
                        if (maxPeriods.Count() > dashB.historySize) maxPeriods.Dequeue();

                        if (historyComplete) StrategyProcess();
                    }
                }
            }//Automatic Calibration

            else
            {
                while(manCalib.Count != 0)
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
            historyComplete = (maxPeriods.Count() == dashB.historySize) ? true : false;
        
        } //RSI Curve Method

        private void StrategyProcess()
        {
            if (firstPass)
            {
                highMean = maxPeriods.Sum() / dashB.historySize; //can use maxPeriods.Average() from LinQ
                lowMean = minPeriods.Sum() / dashB.historySize; //can use maxPeriods.Average() from LinQ
            }
            else
            {
                myBot.mainForm.UpdateRSI(true);
                highMean = (highMean * (dashB.historySize - 1) / dashB.historySize) + (periods.Last() - periods.First() > 0 ? periods.Last() - periods.First() : 0) / dashB.historySize; //mean of MaxPeriods
                lowMean = (lowMean * (dashB.historySize - 1) / dashB.historySize) + (periods.Last() - periods.First() < 0 ? Math.Abs(periods.Last() - periods.First()) : 0) / dashB.historySize; //mean of MinPeriods 
            }

            Math.Round(highMean, 2);
            Math.Round(lowMean, 2);

            rsiMean = (lowMean != 0) ? 100 - (100 / (1 + (highMean / lowMean))) : (highMean != 0) ? 100 : 50; //RSI index for the historySize (N periods)

            Math.Round(rsiMean, 2);

            threePerMean.Enqueue(rsiMean);
            if (threePerMean.Count() > dashB.plot3Size)
            {
                threePerMean.Dequeue();
                plot3Mean = threePerMean.Average();
                CallStrategyAction();
                botActive = true;

                //ListViewItem newItem = new ListViewItem($"{DateTime.Now.ToString("dd.MMM.yy")}");
                //newItem.SubItems.Add("Plot 3");
                //newItem.SubItems.Add(plot3Mean.ToString("0.00"));
                //listView.Items.Add(newItem);
                //Console.WriteLine($"{DateTime.Now} - RSI20: {rsiMean.ToString("0.00")} --- Plot3: {plot3Mean.ToString("0.00")}");
            }

            firstPass = false;
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
                        //Console.WriteLine($"{DateTime.Now} ===> BUY <====\n");
                    }
                    isBought = true;
                    isSold = false;
                }
                else if (useInversion) //zero position
                {
                    if (isBought || isSold)
                    {
                        action = "Zero";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);
                        //Console.WriteLine($" {DateTime.Now} ===> Zero <====\n");
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
                        //Console.WriteLine($"{DateTime.Now} ===> SELL <====\n");
                    }
                    isBought = false;
                    isSold = true;
                }
                else if (useInversion) //zero position
                {
                    if (isBought || isSold)
                    {
                        action = "Zero";
                        price = brickFinal;
                        myBot.mainForm.AddTableItem(true);
                        //Console.WriteLine($" {DateTime.Now} ===> Zero <====\n");
                    }
                    isBought = false;
                    isSold = false;
                }
            }
        }//StrategyAction Method
    }//Class Strategy

}//Namespace
