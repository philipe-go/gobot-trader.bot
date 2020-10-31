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
using System.Deployment.Application;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Pombot_UI
{
    partial class PomBotAppForm
    {
        #region DASHBOARD ITEMS PANEL

        #region ************************ dde ************************
        private void reconnectDDE_Click(object sender, EventArgs e) //RECONNECT DDE if Disconnected
        {
            //DDEupdateStatusAsync();
            DDEupdateStatus();
            openProfit.Visible = (connectedDDE == true) ? false : true;
        }
        //private async void DDEupdateStatusAsync() //FEEDBACK ON DDE CONNECTION - Get from Dashboard DDE interface
        //{
        //    if (connectedDDE)
        //    {
        //        ddeConnectLB.ForeColor = Color.Lime;
        //        ddeConnectLB.Text = "CONNECTED";
        //        connectionPic.BackgroundImage = Resources.connectionIcon2;
        //        openProfit.Visible = false;
        //    }
        //    else
        //    {
        //        ddeConnectLB.ForeColor = Color.Red;
        //        ddeConnectLB.Text = "DISCONNECTED";
        //        connectionPic.BackgroundImage = Resources.connectionIcon;
        //        await ConnectDDETask();
        //    }
        //}
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
                ddeConnectLB.ForeColor = Color.Red;
                ddeConnectLB.Text = "DISCONNECTED";
                connectionPic.BackgroundImage = Resources.connectionIcon;
                ConnectDDE();
            }
        }
        #endregion

        #region *************strategy dashboard input****************
        private void UpdateDDEStrategy() //RETRIEVE SAVED INFORMATIONS
        {
            strategyTB.Text = strategyInput.Value == 0 ? "No Comparison" : "Comparison";

            renkoTimeLB.Text = renkoTimeSelector.Value == 0 ? "Renko" : "Time";
            renkoTimeSelector.Visible = strategyInput.Value == 0 ? false : true;
            renkoTimeLB.Visible = strategyInput.Value == 0 ? false : true;

            upperCurveDB.Visible = strategyInput.Value == 1 ? true : false;
            upperCurveDB.Text = renkoTimeSelector.Value == 0 ?  $"{dashB.upperRenkoPeriod} R" : $"{(Convert.ToDouble(dashB.timeSpamPeriod) / 2).ToString("0.0")} min";

            renkoInput.Value = dashB.renkoPeriod;
            renkoTB.Text = $"{dashB.renkoPeriod} R";
            mainCurveDB.Text = $"{dashB.renkoPeriod} R";
            mainFracGroup.Text = $"{dashB.renkoPeriod} R";
            rsiHistoryInput.Value = dashB.historySize;
            rsiHistoryTB.Text = dashB.historySize.ToString();
            plot3Input.Value = dashB.plot3Size;
            plot3TB.Text = dashB.plot3Size.ToString();
            plotExitInput.Value = dashB.plotExitSize;
            plotExitTB.Text = dashB.plotExitSize.ToString();
            bollSizeInput.Value = dashB.bollSize;
            bollsizeTB.Text = dashB.bollSize.ToString();
            bollMeanInput.Value = dashB.bollMeanSize;
            bollMeanTB.Text = dashB.bollMeanSize.ToString();

            upperRenkoInput.Value = dashB.upperRenkoPeriod;
            upperRenkoTB.Text = upperCurveDB.Text;

            upperFracGroup.Text = $"{dashB.upperRenkoPeriod} R";
            upperRsiHistoryInput.Value = dashB.upperHistorySize;
            upperRsiHistoryTB.Text = dashB.upperHistorySize.ToString();
            upperPlot3Input.Value = dashB.upperPlot3Size;
            upperPlot3TB.Text = dashB.upperPlot3Size.ToString();
            upperPlotExitInput.Value = dashB.upperPlotExitSize;
            upperPlotExitTB.Text = dashB.upperPlotExitSize.ToString();
            upperBollSizeInput.Value = dashB.upperBollSize;
            upperBollsizeTB.Text = dashB.upperBollSize.ToString();
            upperBollMeanInput.Value = dashB.upperBollMeanSize;
            upperBollMeanTB.Text = dashB.upperBollMeanSize.ToString();

            timeSpamSelector.Value = dashB.timeSpamPeriod;
        }
        #endregion

        #region ***********strategy parameters control***************
        private void strategyInput_Scroll(object sender, ScrollEventArgs e)
        {
            if (strategyInput.Value == 0)
            {
                strategyTB.Text = "No Comparison";
                upperCurveDB.Visible = false;
                renkoTimeSelector.Visible = false;
                renkoTimeLB.Visible = false;
            }
            else
            {
                strategyTB.Text = "Comparison";
                renkoTimeSelector.Visible = true;
                renkoTimeLB.Visible = true;
                upperCurveDB.Visible = true;
            }
        }
        private void renkoTimeSelector_Scroll(object sender, ScrollEventArgs e)
        {
            if (renkoTimeSelector.Value == 0)
            {
                renkoTimeLB.Text = "Renko";
                upperRenkoInput.Visible = true;
                timeSpamSelector.Visible = false;
                upperCurveDB.Text = $"{dashB.upperRenkoPeriod} R";
                upperRenkoTB.Text = upperCurveDB.Text;
            }
            else
            {
                renkoTimeLB.Text = "Time";
                upperRenkoInput.Visible = false;
                timeSpamSelector.Visible = true;
                upperCurveDB.Text = $"{(Convert.ToDouble(dashB.timeSpamPeriod) / 2).ToString("0.0")} min";
                upperRenkoTB.Text = upperCurveDB.Text;
            }
        }
        private void renkoInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.renkoPeriod = renkoInput.Value;
            renkoTB.Text = $"{dashB.renkoPeriod} R";
            mainCurveDB.Text = $"{dashB.renkoPeriod} R";
            mainFracGroup.Text = $"{dashB.renkoPeriod} R";
        }

        private void upperRenkoInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperRenkoPeriod = upperRenkoInput.Value;
            upperRenkoTB.Text = $"{dashB.upperRenkoPeriod} R";
            upperCurveDB.Text = $"{dashB.upperRenkoPeriod} R";
            upperFracGroup.Text = $"{dashB.upperRenkoPeriod} R";
        }
        private void timeSpamSelector_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.timeSpamPeriod = timeSpamSelector.Value;
            upperRenkoTB.Text = $"{(Convert.ToDouble(dashB.timeSpamPeriod)/2).ToString("0.0")} min";
            upperCurveDB.Text = $"{(Convert.ToDouble(dashB.timeSpamPeriod)/2).ToString("0.0")} min";
        }
        private void rsiHistoryInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.historySize = rsiHistoryInput.Value;
            rsiHistoryTB.Text = dashB.historySize.ToString();
        }
        private void upperRsiHistoryInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperHistorySize = upperRsiHistoryInput.Value;
            upperRsiHistoryTB.Text = dashB.upperHistorySize.ToString();
        }
        private void plot3Input_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.plot3Size = plot3Input.Value;
            plot3TB.Text = dashB.plot3Size.ToString();
        }
        private void upperPlot3Input_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperPlot3Size = upperPlot3Input.Value;
            upperPlot3TB.Text = dashB.upperPlot3Size.ToString();
        }
        private void plotExitInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.plotExitSize = plotExitInput.Value;
            plotExitTB.Text = dashB.plotExitSize.ToString();
        }
        private void upperPlotExitInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperPlotExitSize = upperPlotExitInput.Value;
            upperPlotExitTB.Text = dashB.upperPlotExitSize.ToString();
        }
        private void bollSizeInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.bollSize = bollSizeInput.Value;
            bollsizeTB.Text = dashB.bollSize.ToString();
        }
        private void upperBollSizeInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperBollSize = upperBollSizeInput.Value;
            upperBollsizeTB.Text = dashB.upperBollSize.ToString();
        }
        private void bollMeanInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.bollMeanSize = bollMeanInput.Value;
            bollMeanTB.Text = dashB.bollMeanSize.ToString();
        }
        private void upperBollMeanInput_Scroll(object sender, ScrollEventArgs e)
        {
            dashB.upperBollMeanSize = upperBollMeanInput.Value;
            upperBollMeanTB.Text = dashB.upperBollMeanSize.ToString();
        }
        #endregion

        #region *****************dashboard buttons*******************
        private void saveBT_Click(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Lime;
            ((Button)sender).Text = "Saved";
            timer2.Interval = 1500;
            timer2.Start();

            List<int> temp = new List<int>();
            temp = DataBase.ConvertToListInt(strategyInput.Value, renkoTimeSelector.Value, renkoInput.Value, upperRenkoInput.Value, timeSpamSelector.Value, rsiHistoryInput.Value, upperRsiHistoryInput.Value,
                plot3Input.Value, upperPlot3Input.Value, plotExitInput.Value, upperPlotExitInput.Value, bollSizeInput.Value, upperBollSizeInput.Value, bollMeanInput.Value, upperBollMeanInput.Value,
                opacityScrollBar.Value, topMostSelector.Value);
            DataBase.SaveDashParameters(temp);
        }
        private void stopBotsBT_Click(object sender, EventArgs e)
        {
            this.InitializeMainForm();
            GC.Collect();
        }
        //timer used for countdowns to saved label
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            saveBT.ForeColor = Color.Black;
            saveBT.Text = "Save Parameters";
            save2BT.ForeColor = Color.Black;
            save2BT.Text = "Save Parameters";
            timer2.Stop();
        }
        #endregion

        #region *************keyboard input manager******************
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
            dashB.sellKeyboard.Add(Keys.V);
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

        #endregion //DASHBOARD

        #region BOT1 PANEL

        #region *************ticker input field handler**************
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
                replayFeedback.Text = tickerTB.Text.ToUpper();
                replayScroll.Value = 0;
                botsList[0].ticker = replayFeedback.Text.ToUpper();
                tickerTB.Text = botsList[0].ticker;
                tickerTB.Enabled = false;
            }
            else
            {
                tickerTB.Enabled = true;
            }
        }
        private void replayScroll_Scroll(object sender, ScrollEventArgs e)
        {
            ReplaySelector();
        }
        private void ReplaySelector()
        {
            replayFeedback.Text = "";
            replayFeedback.Text = replayScroll.Value == 1 ? $"[R] {tickerTB.Text}" : $"{tickerTB.Text}";
            botsList[0].ticker = replayFeedback.Text.ToUpper();
        }
        #endregion

        #region *************exit on bollinger selector**************
        private void zeroOpsSelectorInput_Scroll(object sender, ScrollEventArgs e)
        {
            SetZeroOps();
        }
        private void SetZeroOps()
        {
            zeroRSImark.Visible = zeroOpsSelectorInput.Value == 0 ? true : false;
            zeroBollMark.Visible = zeroOpsSelectorInput.Value == 0 ? false : true;
            botsList[0].zeroOpt = zeroOpsSelectorInput.Value == 0 ? false : true;
            botsList[0].SetZeroInBoll(zeroOpsSelectorInput.Value == 0 ? false : true);
        }
        #endregion

        #region ********************vwap selector********************
        private void useVWAPCB_CheckedChanged(object sender, EventArgs e)
        {
            bool val = useVWAPCB.Checked ? true : false;
            botsList[0].SetVWAP(val);
        }
        #endregion

        #region ***main calibration input field open price handler***
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
        #endregion

        #region ***main calibration input field close price handler**
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
        partial void lastPerCloseTB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.lastPerCloseTB.Text))
            {
                this.lastPerCloseTB.Text = "";
                this.lastPerCloseTB.Focus();
            }
        }
        #endregion

        #region **upper calibration input field open price handler***
        internal void upperOpenTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                upperOpenTB_Leave(sender, e);
            }
        }
        internal void upperOpenTB_Leave(object sender, EventArgs e)
        {
            if (upperFracOpenTB.Text != "")
            {
                botsList[0].InitialUpperBrick(System.Convert.ToDouble(upperFracOpenTB.Text));
                upperFracOpenTB.Text = botsList[0].GetUpperInitial().ToString();
                upperFracOpenTB.Enabled = false;
            }
        }
        internal void upperOpenTB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.upperFracOpenTB.Text))
            {
                this.upperFracOpenTB.Text = "";
                this.upperFracOpenTB.Focus();
            }
        }
        #endregion

        #region **upper calibration input field close price handler**
        internal void upperCloseTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                upperCloseTB_Leave(sender, e);
            }
        }
        internal void upperCloseTB_Leave(object sender, EventArgs e)
        {
            if (upperFracOpenTB.Text != "")
            {
                botsList[0].FinalUpperBrick(System.Convert.ToDouble(upperFracCloseTB.Text));
                upperFracCloseTB.Text = botsList[0].GetUpperFinal().ToString();
                upperFracCloseTB.Enabled = false;
            }
        }
        internal void upperCloseTB_TextChanged(object sender, EventArgs e)
        {
            if (!Validator.isDigit(this.upperFracCloseTB.Text))
            {
                this.upperFracCloseTB.Text = "";
                this.upperFracCloseTB.Focus();
            }
        }
        #endregion

        #region ******************bot buttons************************
        private void saveCalibBT_Click(object sender, EventArgs e)
        {
            if (tickerTB.Text != "" && botsList[0].GetCalibrationBalance() >= 100)
            {
                saveCalibBT.ForeColor = Color.Lime;
                saveCalibBT.Text = "Saved";
                timerSaveCalibBot1.Interval = 1500;
                timerSaveCalibBot1.Start();

                DataBase.SaveCalibDoubles(tickerTB.Text, botsList[0]);
                DataBase.SaveCalibQueues(tickerTB.Text, botsList[0]);
                botsList[0].SetUpperCurve(strategyInput.Value == 0 ? false : true, renkoTimeSelector.Value == 0? true : false);
            }
            else
            {
                tickerTB.Focus();
                saveCalibBT.ForeColor = Color.Red;
                saveCalibBT.Text = "Not Possible";
                timerSaveCalibBot1.Interval = 2000;
                timerSaveCalibBot1.Start();
            }
        }
        private void LoadCalibBT_Click(object sender, EventArgs e)
        {
            if (tickerTB.Text != "")
            {
                List<decimal> doubles = DataBase.ReadCalibDoubles(tickerTB.Text);
                List<Queue<decimal>> queues = DataBase.ReadCalibQueues(tickerTB.Text);

                if (loadPossible)
                {
                    loadCalibBT.ForeColor = Color.Lime;
                    loadCalibBT.Text = "Loaded";
                    timerLoadCalibBot1.Interval = 1500;
                    timerLoadCalibBot1.Start();

                    botsList[0].SetCalib(doubles, queues);
                    this.lastperOpenTB.Text = botsList[0].GetInitialBrick().ToString();
                    this.lastPerCloseTB.Text = botsList[0].GetFinalBrick().ToString();
                    this.upperFracOpenTB.Text = botsList[0].GetUpperInitial().ToString();
                    this.upperFracCloseTB.Text = botsList[0].GetUpperFinal().ToString();
                    lastPerCloseTB.Focus();
                    calibrateBot1BT.Enabled = true;
                    replayScroll.Enabled = true;
                    tickerTB.Enabled = false;
                    lastperOpenTB.Enabled = false;
                    lastPerCloseTB.Enabled = false;
                    upperFracOpenTB.Enabled = false;
                    upperFracCloseTB.Enabled = false;
                    calibrationBot1Bar.Value = botsList[0].GetCalibrationBalance() >= calibrationBot1Bar.Maximum ?
            calibrationBot1Bar.Maximum : botsList[0].GetCalibrationBalance();
                }
                else
                {
                    loadCalibBT.ForeColor = Color.Red;
                    loadCalibBT.Text = "Not Possible";
                    timerLoadCalibBot1.Interval = 2000;
                    timerLoadCalibBot1.Start();
                    tickerTB.Clear();
                    tickerTB.Enabled = true;
                    tickerTB.Focus();
                }
            }
            else
            {
                loadCalibBT.ForeColor = Color.Red;
                loadCalibBT.Text = "Not Possible";
                timerLoadCalibBot1.Interval = 2000;
                timerLoadCalibBot1.Start();
            }
        }
        private void resetCalibBT_Click(object sender, EventArgs e)
        {
            resetCalibBT.ForeColor = Color.Black;
            tickerLB.ForeColor = Color.Black;
            ResetBot();
        }
        private void saveParamsBot1BT_Click(object sender, EventArgs e)
        {
            saveParamsBot1BT.ForeColor = Color.Lime;
            saveParamsBot1BT.Text = "Saved";
            timerBot1.Interval = 1500;
            timerBot1.Start();

            List<string> temp = DataBase.ConvertToListString(tickerTB.Text, autoStartCheck.Checked, replayScroll.Value, zeroOpsSelectorInput.Value, useVWAPCB.Checked);
            DataBase.SaveBotParameters(temp);
        }
        #endregion

        #region *********timers to saved button change status********
        private void timerBot1_Tick(object sender, EventArgs e)
        {
            saveParamsBot1BT.ForeColor = Color.Black;
            saveParamsBot1BT.Text = "Save Parameters";
            timerBot1.Stop();
        }
        private void timerSaveCalibBot1_Tick(object sender, EventArgs e)
        {
            saveCalibBT.ForeColor = Color.Black;
            saveCalibBT.Text = "Save Calibration";
            timerSaveCalibBot1.Stop();
        }
        private void timerLoadCalibBot1_Tick(object sender, EventArgs e)
        {
            loadCalibBT.ForeColor = Color.Black;
            loadCalibBT.Text = "Load Calibration";
            timerLoadCalibBot1.Stop();
        }
        #endregion

        #region *****************calibration button******************
        private void calibrateBot1BT_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
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
                else if (strategyInput.Value == 1 && botsList[0].GetUpperFinal() == 0)
                {
                    upperFracCloseTB.Focus();
                }
                else if (strategyInput.Value == 1 && botsList[0].GetUpperInitial() == 0)
                {
                    upperFracOpenTB.Focus();
                }
                else
                {
                    botsList[0].Calibrate();
                }
            }
            else
            {
                CalibrateBot(false);
            }
        }
        private void CalibrateBot(bool val)
        {
            if (val)
            {
                calibBot1.Start();
                bot1BT.Focus();
                ddeCurrentMeasureBot1.Text = "";
                ddeCurrentMeasureBot1.ForeColor = Color.Black;
                ddeVWAP.Text = "";
                ddeVWAP.ForeColor = Color.Black;
                calibrateBot1BT.Enabled = false;
                lastperOpenTB.Enabled = false;
                lastPerCloseTB.Enabled = false;
                upperFracOpenTB.Enabled = false;
                upperFracCloseTB.Enabled = false;
                tickerTB.Enabled = false;
                replayScroll.Enabled = false;
                resetCalibBT.Enabled = false;

                strategyInput.Enabled = false;
                renkoTimeSelector.Enabled = false;

                renkoInput.Enabled = false;
                rsiHistoryInput.Enabled = false;
                plot3Input.Enabled = false;
                plotExitInput.Enabled = false;
                bollSizeInput.Enabled = false;
                bollMeanInput.Enabled = false;

                upperRenkoInput.Enabled = false;
                timeSpamSelector.Enabled = false;
                upperRsiHistoryInput.Enabled = false;
                upperPlot3Input.Enabled = false;
                upperPlotExitInput.Enabled = false;
                upperBollSizeInput.Enabled = false;
                upperBollMeanInput.Enabled = false;
            }
            else
            {
                calibrateBot1BT.ForeColor = Color.Red;
                calibrateBot1BT.Text = "Not Possible";
                calibErrorTimer.Interval = 2000;
                calibErrorTimer.Start();
            }
        }
        private void calibErrorTimer_Tick(object sender, EventArgs e)
        {
            calibrateBot1BT.ForeColor = Color.Black;
            calibrateBot1BT.Text = "Calibrate";
            calibErrorTimer.Stop();
        }
        #endregion

        #region *********timer for calibration load bar**************
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
        private void calibrationBot1Bar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            string fb = botsList[0].CalibrationString();
            tip.AutoPopDelay = 10000;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;

            tip.ShowAlways = true;

            tip.SetToolTip(this.calibrationBot1Bar, $"{fb}");
        }
        #endregion

        #region ***************start bot operations******************
        private void startBot1BT_Click(object sender, EventArgs e)
        {
            if (connectedDDE)
            {
                robot1PB.BackgroundImage = Resources.robotIcon2;
                botsList[0].Connect(true); //connect bot actions (buy, sell, zero)

                //deactivate items
                startBot1BT.Enabled = false;
                calibrateBot1BT.Enabled = false;
                loadCalibBT.Enabled = false;
                replayScroll.Enabled = false;
                renkoInput.Enabled = false;
            }
        }
        #endregion

        #region ****************stop bot operations******************
        private void stopBot1BT_Click(object sender, EventArgs e)
        {
            botsList[0].Connect(false); //disconnect bot actions (buy, sell, zero) but maintain the RSI history building

            robot1PB.BackgroundImage = Resources.robotIcon;

            startBot1BT.Enabled = true;
            resetCalibBT.Enabled = true;
        }
        #endregion

        #region ********************reset bot************************
        private void ResetBot()
        {
            botsList[0].ResetCalibration();

            //lastperOpenTB.Clear();
            //lastPerCloseTB.Clear();
            //upperFracOpenTB.Clear();
            //upperFracCloseTB.Clear();
            //tickerTB.Clear();
            calibrationBot1Bar.Value = 0;

            //Bot Panel Items
            foreach (Control ctr in bot1PN.Controls.OfType<GroupBox>())
            {
                foreach (Control tr in ctr.Controls)
                {
                    if (tr is TextBox) ((TextBox)tr).Clear();
                    if (tr is GroupBox) { foreach (Control tb in tr.Controls) if (tb is TextBox) { ((TextBox)tb).Clear(); tb.Enabled = true; } }
                    tr.Enabled = true;
                }
            }
            //Dashboard Panel Items
            foreach (Control ctr in dashboardPN.Controls.OfType<GroupBox>())
            {
                foreach (Control tr in ctr.Controls)
                    ctr.Enabled = true;
            }

            startBot1BT.Enabled = false;
            calibBot1.Enabled = true;

            tickerTB.Focus();
        }
        #endregion

        #endregion //BOT

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
