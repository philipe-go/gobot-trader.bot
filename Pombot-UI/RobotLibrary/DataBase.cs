using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Pombot_UI.RobotLibrary
{
    internal abstract class DataBase
    {
        internal static List<int> ConvertToListInt(int renko, int rsiHist, int plot, int inversion, int opac, int topMost)
        {
            List<int> tempList = new List<int>();
            tempList.Add(renko);
            tempList.Add(rsiHist);
            tempList.Add(plot);
            tempList.Add(inversion);
            tempList.Add(opac);
            tempList.Add(topMost);

            return tempList;
        }

        internal static List<string> ConvertToListString(string ticker, int calib, bool autostart)
        {
            List<string> tempList = new List<string>();
            tempList.Add(ticker);
            tempList.Add(calib.ToString());
            tempList.Add(autostart.ToString());

            return tempList;
        }

        #region Save DashBoard Parameters
        static string xmlDashFile = @"..\savedDashP.xml";

        internal static void SaveDashParameters(List<int> param)
        {
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
                tempList.Add(1); //renko
                tempList.Add(7); //rsi
                tempList.Add(1); //plot3
                tempList.Add(0); //inversion
                tempList.Add(10); //opacity
                tempList.Add(0); //topMost
                SaveDashParameters(tempList);
            }
            return tempList;
        }
        #endregion

        #region Save Bot Parameters
        static string xmlBotFile = @"..\savedBotP.xml";

        internal static void SaveBotParameters(List<string> param)
        {
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
                tempList.Add(0.ToString()); //auto calib
                tempList.Add("false"); //auto start
                SaveBotParameters(tempList);
            }
            return tempList;
        }
        #endregion
    }
}
