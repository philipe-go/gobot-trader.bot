using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDde.Client;

namespace Pombot_UI.RobotLibrary
{
    internal sealed class Bot
    {
        #region Bot Attributes
        /**** DDE Attributes ****/
        private string col;
        internal string ticker;
        /**** Strategy Attributes ****/
        private Strategy strategy;
        internal Dashboard dashB;
        internal PomBotAppForm mainForm; //to be used via strategy
        internal bool manualCalib;
        internal bool activated;
        #endregion

        #region Encapsulation
        internal double GetInitialBrick()
        {
            return strategy.GetInitialBrick();
        }
        internal double GetFinalBrick()
        {
            return strategy.GetFinalBrick();
        }
        internal int GetHistorySize()
        {
            if (manualCalib) return strategy.GetHistorySize();
            return 0;
        }
        #endregion

        #region Constructor
        internal Bot()
        {
            this.dashB = Dashboard.GetInstance();
            this.col = "ULT";
            this.ticker = "";
            this.manualCalib = false;
            this.activated = false;

            this.strategy = new Strategy();
            strategy.SetBot(this);
        }
        #endregion

        #region DDE
        internal void Connect(bool val) //TO SET THE Connection status as true on the mainFrom
        {
            this.activated = val;
        }
        #endregion

        #region Calibration
        internal void Calibrate()
        {
            if (ticker != "")
            {
                strategy.temp = strategy.GetFinalBrick();
                mainForm.Advise(ticker, col);

                if (!manualCalib)
                {
                    bool maxcurveVal = strategy.GetInitialBrick() < strategy.GetFinalBrick() ? true : false;
                    strategy.MaxCurve(maxcurveVal);
                }
                strategy.manualCalibration = manualCalib;
            }
        }

        #region Manual Calibration Methods
        internal void ManualCalibration(double brick)
        {
            if (manualCalib) strategy.ManualEntry(brick);
        }
        #endregion

        #region Auto Calibration Methods
        internal void InitialBrick(double brick)
        {
            if (!manualCalib) strategy.AutoEntryOpen(brick);
        }
        internal void FinalBrick(double brick)
        {
            if (!manualCalib) strategy.AutoEntryClose(brick);
        }
        #endregion

        internal void ResetCalibration()
        {
            strategy.Reset();
        }

        #endregion

        #region Strategy Hidden Properties
        internal void SetProcess()
        {
            strategy.SetProcess();
        }
        internal void Advise(float temp)
        {
            strategy.Advise(temp);
        }
        internal string GetTemp()
        {
            return strategy.temp.ToString("0.00");
        }
        internal string GetRSIMean() 
        {
            //return strategy.RsiMean.ToString("0.00"); //--->COMMENTED TO SHOW THE CURRENT RSI AND PLOT instead of the one when the period closes
            return strategy.CurrentRSI().ToString("0.00");
        }
        internal string GetPlotMean()
        {
            //return strategy.Plot3Mean.ToString("0.00"); //--->COMMENTED TO SHOW THE CURRENT RSI AND PLOT instead of the one when the period closes
            return strategy.CurrentPlot().ToString("0.00");
        }
        internal string GetAction()
        {
            return strategy.action;
        }
        internal double GetPrice()
        {
            return strategy.price;
        }
        internal bool GetBotActive()
        {
            return strategy.botActive;
        }
        internal int GetCalibrationBalance()
        {
            return strategy.CalibrationBallance();
        }
        #endregion
    }
    }
