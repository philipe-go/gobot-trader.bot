using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDde.Client;

namespace Pombot_UI.RobotLibrary
{
    class Dashboard
    {
        #region Attributes
        /**** DDE interface ****/
        private string app = "profitchart";
        //private string col = "ULT";
        //private string item = $"{ticker}.{col}";
        private string service = "cot";

        /**** Strategy ****/
        internal static int historySize = 20;
        internal static int plot3Size = 3;
        internal static float renkoPeriod = 3; //for 5R graphs

        internal static float temp;
        internal static bool refreshtemp = false;
        #endregion

        #region Singleton
        private static Dashboard instance = null;
        private Dashboard() { }
        public static Dashboard GetInstance()
        {
            if (instance == null) instance = new Dashboard();
            return instance;
        }
        #endregion

        #region DDE interface
        internal bool ConnectDDE()
        {
            DdeClient client = new DdeClient(this.app, this.service);
            try
            {
                client.Connect();
            }
            catch (NDde.DdeException)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
