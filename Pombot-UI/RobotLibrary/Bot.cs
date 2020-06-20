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
    class Bot
    {
        #region Bot Attributes
        /**** DDE Attributes ****/
        internal string ticker;
        private string col = "ULT";
        /**** Strategy Attributes ****/
        private Dashboard dashb;
        internal Strategy strategy;
        internal bool manualCalib;
        internal bool activated;
        internal Label currentMeasure;
        internal PomBotAppForm mainForm;
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
            dashb = Dashboard.GetInstance();
            this.col = "ULT";
            this.ticker = "";
            strategy = new Strategy();
            manualCalib = false;
            activated = false;
            strategy.myBot = this;
        }
        #endregion

        #region DDE
        internal void Connect(bool val)
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
    }
}
