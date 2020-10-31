using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI.RobotLibrary
{
    sealed class StrategyCurve
    {
        #region Strategy Properties
        private Strategy strategy;
        private int renko;
        private int historySize;
        private int plot3Size;
        private int plotExitSize;
        private int bollSize;
        private int bollMeanSize;
        #endregion

        #region Curve Properties
        private bool isRenko;
        private bool maxCurve;
        private bool firstPass;
        private bool historyComplete;
        private bool curveBuilt;

        private int tempTime;

        private double renkoPeriod;
        private double brickInitial;
        private double brickFinal;

        private Queue<decimal> maxPeriods = new Queue<decimal>();
        private Queue<decimal> minPeriods = new Queue<decimal>();
        private Queue<decimal> periodsPrices = new Queue<decimal>();

        private decimal lowMean = 0;
        private decimal highMean = 0;
        private decimal rsiMean = 0;
        private decimal plot3Mean = 0;
        private decimal plotExitMean = 0;
        private Queue<decimal> threePerMean = new Queue<decimal>();
        private Queue<decimal> exitPerMean = new Queue<decimal>();

        private decimal bbMean = 0;
        private decimal bbTop = 0;
        private decimal bbLow = 0;
        private decimal stdDev = 0;
        private decimal bbWidth = 0;
        private decimal bbMovMean = 0;
        private Queue<decimal> bollMovMean = new Queue<decimal>();
        #endregion

        #region Events and Delegates
        internal delegate void CurrentMeasuresDelegate();
        internal CurrentMeasuresDelegate UpdateCurrentMeasure;
        internal event CurrentMeasuresDelegate OnCurrentMeasuresUpdate;
        #endregion

        #region Constructor & Update
        internal StrategyCurve()
        {
            this.maxCurve = true;
            this.historyComplete = false;
            this.firstPass = true;
            this.curveBuilt = false;
            this.isRenko = true;

            this.renko = 1;
            this.historySize = 1;
            this.plot3Size = 1;
            this.plotExitSize = 1;
            this.bollSize = 1;
            this.bollMeanSize = 1;
            this.maxPeriods = new Queue<decimal>();
            this.minPeriods = new Queue<decimal>();
            this.periodsPrices = new Queue<decimal>();
            this.threePerMean = new Queue<decimal>();
            this.exitPerMean = new Queue<decimal>();
            this.bollMovMean = new Queue<decimal>();

            this.tempTime = 0;
        }

        internal void UpdateParametres(bool type, int renko, int rsi, int rsimean, int rsiexit, int boll, int bollmean, Strategy strat)
        {
            this.isRenko = type;
            this.renko = renko;
            this.historySize = rsi;
            this.plot3Size = rsimean;
            this.plotExitSize = rsiexit;
            this.bollSize = boll;
            this.bollMeanSize = bollmean;
            this.strategy = strat;

            if (isRenko) { renkoPeriod = (renko / 2) - 0.5; strategy.OnBuildCurve += BuildCurveRenko; }
            else { renkoPeriod = Convert.ToDouble(renko) * 30; tempTime = Convert.ToInt32(renkoPeriod); strategy.OnBuildCurve += BuildCurveHOR; }

            UpdateCurrentMeasure = new CurrentMeasuresDelegate(SetCurrentMeasures);
        }
        #endregion

        #region Encapsulation
        internal bool CurveBuilt { get => curveBuilt; }
        internal double BrickInitial { get => brickInitial; set => brickInitial = value; }
        internal double BrickFinal { get => brickFinal; set => brickFinal = value; }
        internal decimal BbMovMean { get => bbMovMean; set => bbMovMean = value; }
        internal decimal BbWidth { get => bbWidth; set => bbWidth = value; }
        internal decimal RsiMean { get => rsiMean; set => rsiMean = value; }
        internal decimal Plot3Mean { get => plot3Mean; set => plot3Mean = value; }
        internal decimal PlotExitMean { get => plotExitMean; set => plotExitMean = value; }
        internal bool MaxCurve { get => maxCurve; set => maxCurve = value; }
        internal Queue<decimal> MaxPeriods { get => maxPeriods; set => maxPeriods = value; }
        internal Queue<decimal> MinPeriods { get => minPeriods; set => minPeriods = value; }
        internal Queue<decimal> PeriodsPrices { get => periodsPrices; set => periodsPrices = value; }
        internal decimal LowMean { get => lowMean; set => lowMean = value; }
        internal decimal HighMean { get => highMean; set => highMean = value; }
        internal Queue<decimal> ThreePerMean { get => threePerMean; set => threePerMean = value; }
        internal Queue<decimal> ExitPerMean { get => exitPerMean; set => exitPerMean = value; }
        internal decimal BbMean { get => bbMean; set => bbMean = value; }
        internal decimal BbTop { get => bbTop; set => bbTop = value; }
        internal decimal BbLow { get => bbLow; set => bbLow = value; }
        internal decimal StdDev { get => stdDev; set => stdDev = value; }
        internal Queue<decimal> BollMovMean { get => bollMovMean; set => bollMovMean = value; }
        internal bool HistoryComplete { get => historyComplete; set => historyComplete = value; }
        internal bool IsRenko { get => isRenko; set => isRenko = value; }
        internal Strategy Strategy { get => strategy; set => strategy = value; }
        #endregion

        #region Curve Management Methods
        internal void Reset()
        {
            renko = 1;
            historySize = 1;
            plot3Size = 1;
            plotExitSize = 1;
            bollSize = 1;
            bollMeanSize = 1;

            brickInitial = 0;
            brickFinal = 0;
            lowMean = 0;
            highMean = 0;
            rsiMean = 0;
            plot3Mean = 0;
            plotExitMean = 0;
            bbMean = 0;
            bbTop = 0;
            stdDev = 0;
            bbWidth = 0;
            bbMovMean = 0;

            historyComplete = false;
            maxCurve = false;

            maxPeriods.Clear();
            minPeriods.Clear();
            periodsPrices.Clear();
            threePerMean.Clear();
            bollMovMean.Clear();
        }
        #endregion

        //TODO -> Split the Curves in two child classes to handle similar methods, signatures and behaviour
        #region Build Curve Methods 
        internal void BuildCurveRenko()
        {
            if (strategy.Temp - brickFinal > renkoPeriod) //complete period for ascending curve
            {
                if (maxCurve) //ascending curve
                {
                    brickInitial += renkoPeriod;
                    brickFinal = brickInitial + renkoPeriod;

                    periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                    maxPeriods.Enqueue(Convert.ToDecimal(renkoPeriod));
                    minPeriods.Enqueue(0);

                    if (historyComplete) BuildProcess();
                    Console.WriteLine($"Now: {strategy.Now} - Abertura: {brickInitial} / Fechamento: {BrickFinal}");
                }
                else if (!maxCurve) //reversion of descending curve point
                {
                    if (strategy.Temp - brickInitial > renkoPeriod)
                    {
                        maxCurve = true;
                        brickFinal = brickInitial + renkoPeriod;

                        periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                        maxPeriods.Enqueue(Convert.ToDecimal(2 * renkoPeriod));
                        minPeriods.Enqueue(0);

                        if (historyComplete) BuildProcess();
                        Console.WriteLine($"Now: {strategy.Now} - Abertura: {brickInitial} / Fechamento: {BrickFinal}");
                    }
                }
            }
            if (strategy.Temp - brickFinal < -renkoPeriod) //complete period for descending curve
            {
                if (maxCurve) //reversion point of ascending curve
                {
                    if (strategy.Temp - brickInitial < -renkoPeriod)
                    {
                        maxCurve = false;
                        brickFinal = brickInitial - renkoPeriod;

                        periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                        minPeriods.Enqueue(Convert.ToDecimal(2 * renkoPeriod));
                        maxPeriods.Enqueue(0);

                        if (historyComplete) BuildProcess();
                        Console.WriteLine($"Now: {strategy.Now} - Abertura: {brickInitial} / Fechamento: {BrickFinal}");
                    }
                }
                else if (!maxCurve) //descending curve
                {
                    brickInitial -= renkoPeriod;
                    brickFinal = brickInitial - renkoPeriod;

                    periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                    minPeriods.Enqueue(Convert.ToDecimal(renkoPeriod));
                    maxPeriods.Enqueue(0);

                    if (historyComplete) BuildProcess();
                    Console.WriteLine($"Now: {strategy.Now} - Abertura: {brickInitial} / Fechamento: {BrickFinal}");
                }
            }

            CheckPeriodsCount();
        }
        internal void BuildCurveHOR()
        {
            brickFinal = strategy.Temp;

            if (tempTime >= Convert.ToInt32(renkoPeriod))
            {
                periodsPrices.Enqueue(Convert.ToDecimal(strategy.Temp));

                if (periodsPrices.Count() > 1)
                {
                    if ((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2) >= 0))
                    {
                        maxPeriods.Enqueue(Convert.ToDecimal((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2))));
                        minPeriods.Enqueue(0);
                    }
                    else
                    {
                        maxPeriods.Enqueue(0);
                        minPeriods.Enqueue(Math.Abs(Convert.ToDecimal((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2)))));
                    }
                }


                if (historyComplete) BuildProcess();
                tempTime = 0;
            }

            tempTime = (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60) - tempTime < 0 ? (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60) + 60 : (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60);

            CheckPeriodsCount();
        }
        private void CheckPeriodsCount()
        {
            if (periodsPrices.Count() > bollSize) periodsPrices.Dequeue();
            if (minPeriods.Count() > historySize) minPeriods.Dequeue();
            if (maxPeriods.Count() > historySize) maxPeriods.Dequeue();
            historyComplete = bollSize >= historySize ? periodsPrices.Count() >= bollSize ? true : false : maxPeriods.Count() >= historySize ? true : false;

            if (curveBuilt)
            {
                UpdateCurrentMeasure();
                OnCurrentMeasuresUpdate?.Invoke();
            }
        }
        private void BuildProcess() //Plot 3 Curve Build and post-RSI
        {
            //RSI Calculations
            if (firstPass)
            {
                highMean = maxPeriods.Average();  //maxPeriods.Sum() / historySize; 
                lowMean = minPeriods.Average();  //minPeriods.Sum() / historySize;
            }
            else
            {
                highMean = (highMean * (maxPeriods.Count() - 1) / maxPeriods.Count()) + (periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2) > 0 ? periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2) : 0) / maxPeriods.Count(); //mean of MaxPeriods
                lowMean = (lowMean * (minPeriods.Count() - 1) / minPeriods.Count()) + (periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2) < 0 ? Math.Abs(periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2)) : 0) / minPeriods.Count(); //mean of MinPeriods 
            }

            highMean = Math.Round(highMean, 2);
            lowMean = Math.Round(lowMean, 2);

            rsiMean = (lowMean != 0) ? 100 - (100 / (1 + (highMean / lowMean))) : (highMean != 0) ? 100 : 50; //RSI index for the historySize (N periods)

            //MMedian for RSI
            threePerMean.Enqueue(rsiMean);
            if (threePerMean.Count() > plot3Size)
            {
                threePerMean.Dequeue();
                plot3Mean = threePerMean.Average();
            }
            //MMedian for RSI Exit
            exitPerMean.Enqueue(rsiMean);
            if (exitPerMean.Count() > plotExitSize)
            {
                exitPerMean.Dequeue();
                plotExitMean = exitPerMean.Average();
            }

            //Bollinger Bands Calculations
            stdDev = periodsPrices.Sum(s => (s - periodsPrices.Average()) * (s - periodsPrices.Average()));
            stdDev = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(stdDev / periodsPrices.Count()))); //sample would be periodsPrices.Count() - 1
            bbTop = periodsPrices.Average() + (2 * stdDev);
            bbLow = periodsPrices.Average() - (2 * stdDev);
            bbWidth = (bbTop - bbLow) * 100 / periodsPrices.Average();

            //MMedian for BB
            bollMovMean.Enqueue(bbWidth);
            if (bollMovMean.Count() > bollMeanSize)
            {
                bollMovMean.Dequeue();
                bbMovMean = bollMovMean.Average();
            }
            firstPass = false;
        }
        #endregion

        #region Calibration
        internal int CalibBalance()
        {
            int temp = 0;
            temp += bollSize > historySize ? periodsPrices.Count() * 100 / bollSize : maxPeriods.Count() * 100 / historySize;
            temp += plot3Size > plotExitSize && plot3Size > bollMeanSize ? threePerMean.Count() * 100 / plot3Size :
                                        plotExitSize > bollMeanSize ? exitPerMean.Count() * 100 / plotExitSize : bollMovMean.Count() * 100 / bollMeanSize;
            curveBuilt = temp / 2 >= 100 ? true : false;
            return temp / 2;
        }
        #endregion

        #region Current Measurements
        private decimal currentRSI;
        internal decimal CurrentRSI { get => currentRSI; }
        private decimal currentPlot;
        internal decimal CurrentPlot { get => currentPlot; }
        private decimal currentBB;
        internal decimal CurrentBB { get => currentBB; }
        private decimal currentBollMean;
        internal decimal CurrentBollMean { get => currentBollMean; }

        private void SetCurrentMeasures()
        {
            //RSI
            decimal temp = Convert.ToDecimal(strategy.Temp);
            decimal tempHigh, tempLow;
            tempHigh = (highMean * (historySize - 1) / historySize) + (temp - periodsPrices.Last() > 0 ? temp - periodsPrices.Last() : 0) / historySize; //mean of MaxPeriods
            tempLow = (lowMean * (historySize - 1) / historySize) + (temp - periodsPrices.Last() < 0 ? Math.Abs(temp - periodsPrices.Last()) : 0) / historySize; //mean of MinPeriods 
            tempHigh = Math.Round(tempHigh, 2);
            tempLow = Math.Round(tempLow, 2);

            Math.Round(tempHigh, 2);
            Math.Round(tempLow, 2);

            currentRSI = (tempLow != 0) ? 100 - (100 / (1 + (tempHigh / tempLow))) : 50;

            this.currentRSI = Math.Round(currentRSI, 2);

            //RSI Mean
            this.currentPlot = Math.Round((plot3Mean + currentRSI) / 2, 2);

            SetCurrentBoll();
        }
        private void SetCurrentBoll()
        {
            //Bollinger
            decimal temp = Convert.ToDecimal(strategy.Temp);
            Queue<decimal> tempList = new Queue<decimal>(periodsPrices);
            tempList.Enqueue(temp);
            decimal curstdDev = tempList.Sum(s => (s - tempList.Average()) * (s - tempList.Average()));
            curstdDev = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(curstdDev / tempList.Count())));
            decimal curbbTop = tempList.Average() + (2 * curstdDev);
            decimal curbbLow = tempList.Average() - (2 * curstdDev);
            currentBB = ((curbbTop - curbbLow) / tempList.Average()) * 100;
            tempList = null;

            this.currentBB = Math.Round(currentBB, 2);

            //Bollinger Mean
            this.currentBollMean = Math.Round((bbMovMean + currentBB) / 2, 2);
        }
        #endregion
    }//Class Strategy Curve
}//Namespace
