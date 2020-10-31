using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDde.Client;

namespace Pombot_UI.RobotLibrary
{
    sealed class Bot
    {
        #region Bot Attributes
        /**** DDE Attributes ****/
        private string col;
        private string colVWAP;
        private string colHOR;
        internal string ticker;
        internal bool zeroOpt;
        /**** Strategy Attributes ****/
        private Strategy strategy;
        internal Strategy Strategy { get => strategy; }
        internal Dashboard dashB;
        internal PomBotAppForm mainForm; //to be used via strategy
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
        internal double GetUpperInitial()
        {
            return strategy.GetUpperInitialBrick();
        }
        internal double GetUpperFinal()
        {
            return strategy.GetUpperFinalBrick();
        }
        #endregion

        #region Constructor
        internal Bot()
        {
            this.dashB = Dashboard.GetInstance();
            this.col = "ULT";
            this.colVWAP = "67";
            this.colHOR = "HOR";
            this.ticker = "";
            this.zeroOpt = true;
            this.activated = false;
            this.strategy = new Strategy(this);
        }
        #endregion

        #region DDE
        internal void Connect(bool val) //TO SET THE Connection status as true on the mainFrom
        {
            this.activated = val;
            strategy.StartedBot();
        }
        #endregion

        #region Calibration
        internal void Calibrate()
        {
            if (ticker != "")
            {
                mainForm.Advise(ticker, col);
                mainForm.AdviseVWAP(ticker, colVWAP);
                mainForm.AdviseHOR(ticker, colHOR);
                strategy.UpdateCurves();
                strategy.Temp = strategy.GetFinalBrick();
                strategy.MaxCurve();
            }
        }

        #region Auto Calibration Methods
        internal void InitialBrick(double brick)
        {
            strategy.AutoEntryOpen(brick);
        }
        internal void FinalBrick(double brick)
        {
            strategy.AutoEntryClose(brick);
        }
        internal void InitialUpperBrick(double brick)
        {
            strategy.UpperOpenBrick(brick);
        }
        internal void FinalUpperBrick(double brick)
        {
            strategy.UpperCloseBrick(brick);
        }
        #endregion
        
        internal void ResetCalibration()
        {
            strategy.Reset();
        }
        #endregion

        #region Strategy Hidden Properties
        internal void Advise(float temp)
        {
            strategy.Advise(temp);
        }
        internal void AdviseVWAP(double temp)
        {
            strategy.AdviseVWAP(temp);
        }
        internal void AdviseHOR(DateTime now)
        {
            strategy.AdviseHOR(now);
        }
        internal void SetUpperCurve(bool val, bool isRenko)
        {
            strategy.SetUpperCurve(val, isRenko);
        }
        internal void SetProcess()
        {
            strategy.SetProcess();
        }
        internal void SetZeroInBoll(bool val)
        {
            strategy.SetZeroWithBollinger(val);
        }
        internal string GetTemp()
        {
            return strategy.Temp.ToString("0.00");
        }
        internal void SetVWAP(bool val)
        {
            strategy.SetVWAP(val);
        }
        internal string GetVWAP()
        {
            return strategy.Vwap.ToString("0.00");
        }
        internal void VWAPColour()
        {
            strategy.VWAPColour();
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
        internal bool GetBuiltCurve(string i)
        {
            return strategy.GetCurveBuilt(i);
        }
        internal int GetCalibrationBalance()
        {
            return strategy.CalibrationBallance();
        }
        internal string CalibrationString()
        {
            return strategy.CalibrationString();
        }
        internal List<Queue<decimal>> CalibQueue()
        {
            List<Queue<decimal>> temp = new List<Queue<decimal>>();
            temp = strategy.CalibQueue();
            return temp;
        }
        internal List<decimal> CalibDoubles()
        {
            List<decimal> temp = new List<decimal>();
            temp = strategy.CalibDoubles();
            return temp;
        }
        internal void SetCalib(List<decimal> doubles, List<Queue<decimal>> queues)
        {
            strategy.ReadCalib(doubles, queues);
        }
        #endregion
    }// Class Bot
}//Namespace
