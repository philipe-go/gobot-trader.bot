using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Pombot_UI.RobotLibrary
{
    internal abstract class DataBase
    {
        internal static List<int> ConvertToListInt(int strategy, int time, int renko, int renkoU, int timeU, int rsiHist, int rsiHistU, int plot, int plotU, int plotExit, int plotExitU,
                    int bollSize, int bollSizeU, int bollMean, int bollMeanU, int opac, int topMost)
        {
            List<int> tempList = new List<int>();
            tempList.Add(strategy);
            tempList.Add(time);
            tempList.Add(renko);
            tempList.Add(renkoU);
            tempList.Add(timeU);
            tempList.Add(rsiHist);
            tempList.Add(rsiHistU);
            tempList.Add(plot);
            tempList.Add(plotU);
            tempList.Add(plotExit);
            tempList.Add(plotExitU);
            tempList.Add(bollSize);
            tempList.Add(bollSizeU);
            tempList.Add(bollMean);
            tempList.Add(bollMeanU);
            tempList.Add(opac);
            tempList.Add(topMost);

            return tempList;
        }

        internal static List<string> ConvertToListString(string ticker, bool autostart, int replayVal, int zeroVal, bool useVWAP)
        {
            List<string> tempList = new List<string>();
            tempList.Add(ticker);
            tempList.Add(autostart.ToString());
            tempList.Add(replayVal.ToString());
            tempList.Add(zeroVal.ToString());
            tempList.Add(useVWAP.ToString());

            return tempList;
        }

        private static void CheckDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }


        #region Save DashBoard Parameters
        private static string xmlDashFile = @".\SavedParameters\savedDashP.xml";

        internal static void SaveDashParameters(List<int> param)
        {
            CheckDirectory(@".\SavedParameters\");
            XmlSerializer xs = new XmlSerializer(typeof(List<int>));
            XmlWriter xw = XmlWriter.Create(xmlDashFile);
            xs.Serialize(xw, param);
            xw.Close();
        }

        internal static List<int> ReadDashParameters()
        {
            List<int> tempList = new List<int>();
            if (File.Exists(xmlDashFile))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<int>));
                StreamReader sr = new StreamReader(xmlDashFile);
                tempList = (List<int>)xs.Deserialize(sr);
                sr.Close();
            }
            else
            {
                tempList.Add(1); //strategy
                tempList.Add(0); //renkoORtime
                tempList.Add(1); //renko
                tempList.Add(1); //renkoU
                tempList.Add(1); //timeU
                tempList.Add(7); //rsi
                tempList.Add(7); //rsiU
                tempList.Add(1); //plot3
                tempList.Add(1); //plot3U
                tempList.Add(1); //plotExit
                tempList.Add(1); //plotExitU
                tempList.Add(7); //boll
                tempList.Add(7); //bollU
                tempList.Add(1); //bollMean
                tempList.Add(1); //bollMeanU
                tempList.Add(10); //opacity
                tempList.Add(0); //topMost
                SaveDashParameters(tempList);
            }
            return tempList;
        }
        #endregion

        #region Save Bot Parameters
        private static string xmlBotFile = @".\SavedParameters\savedBotP.xml";

        internal static void SaveBotParameters(List<string> param)
        {
            CheckDirectory(@".\SavedParameters\");
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            XmlWriter xw = XmlWriter.Create(xmlBotFile);
            xs.Serialize(xw, param);
            xw.Close();
        }

        internal static List<string> ReadBotParameters()
        {
            List<string> tempList = new List<string>();
            if (File.Exists(xmlBotFile))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<string>));
                StreamReader sr = new StreamReader(xmlBotFile);
                tempList = (List<string>)xs.Deserialize(sr);
                sr.Close();
            }
            else
            {
                tempList.Add(""); //ticker
                tempList.Add("false"); //auto start
                tempList.Add(0.ToString()); //replay
                tempList.Add(0.ToString()); //zero
                tempList.Add("false"); //auto start
                SaveBotParameters(tempList);
            }
            return tempList;
        }
        #endregion

        #region Save Calibration Data
        private static string xmlQueueFile; //= $@".\CalibrationHistory\{tickerName}-q.xml";
        private static string xmlDoubleFile; //= $@".\CalibrationHistory\{tickerName}-d.xml";

        //Queue<double> test = new Queue<double>();
        //List<List<double>> alist = new List<List<double>>();
        //private void Test()
        //{
        //    alist.Add(test.ToList<double>());
        //    foreach (double el in alist[0])
        //    {
        //        test.Enqueue(el);
        //    }
        //}

        internal static void SaveCalibQueues(string ticker, Bot aBot)
        {
            CheckDirectory(@".\CalibrationHistory\");
            xmlQueueFile = $@".\CalibrationHistory\{ticker}-q.xml";

            List<Queue<decimal>> tempQueue = aBot.CalibQueue();
            List<List<double>> templist = new List<List<double>>();

            foreach (Queue<decimal> que in tempQueue)
            {
                List<double> convertedQueue = new List<double>();
                foreach(decimal el in que)
                {
                    convertedQueue.Add(Convert.ToDouble(el));
                }
                templist.Add(convertedQueue);
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<List<double>>));
            XmlWriter xw = XmlWriter.Create(xmlQueueFile);
            xs.Serialize(xw, templist);
            xw.Close();
        }

        internal static void SaveCalibDoubles(string ticker, Bot aBot)
        {
            CheckDirectory(@".\CalibrationHistory\");
            xmlDoubleFile = $@".\CalibrationHistory\{ticker}-d.xml";
            List<double> templist = new List<double>();
            List<decimal> decList = aBot.CalibDoubles();
            foreach (decimal el in decList)
            {
                templist.Add(Convert.ToDouble(el));
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<double>));
            XmlWriter xw = XmlWriter.Create(xmlDoubleFile);
            xs.Serialize(xw, templist);
            xw.Close();
        }

        internal static List<Queue<decimal>> ReadCalibQueues(string ticker)
        {
            CheckDirectory(@".\CalibrationHistory\");
            xmlQueueFile = $@".\CalibrationHistory\{ticker}-q.xml";
            List<List<double>> templist = new List<List<double>>();
            List<Queue<decimal>> tempqueue = new List<Queue<decimal>>();
            if (File.Exists(xmlQueueFile))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<List<double>>));
                StreamReader sr = new StreamReader(xmlQueueFile);
                templist = (List<List<double>>)xs.Deserialize(sr);
                sr.Close();

                foreach (List<double> lst in templist)
                {
                    Queue<decimal> temp = new Queue<decimal>();
                    foreach (double el in lst)
                    {
                        temp.Enqueue(Convert.ToDecimal(el));
                    }
                    tempqueue.Add(temp);
                }

                PomBotAppForm.loadPossible = true;
                return tempqueue;
            }
            else
            {
                PomBotAppForm.loadPossible = false;
                return null;
            }
        }

        internal static List<decimal> ReadCalibDoubles(string ticker)
        {
            CheckDirectory(@".\CalibrationHistory\");
            xmlDoubleFile = $@".\CalibrationHistory\{ticker}-d.xml";
            List<double> tempList = new List<double>();
            List<decimal> decimalList = new List<decimal>();

            if (File.Exists(xmlDoubleFile))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<double>));
                StreamReader sr = new StreamReader(xmlDoubleFile);
                tempList = (List<double>)xs.Deserialize(sr);
                sr.Close();
                foreach(double el in tempList)
                {
                    decimalList.Add(Convert.ToDecimal(el));
                }
                PomBotAppForm.loadPossible = true;
                return decimalList;
            }
            else
            {
                PomBotAppForm.loadPossible = false;
                return null;
            }
        }


        #endregion
    }
}
