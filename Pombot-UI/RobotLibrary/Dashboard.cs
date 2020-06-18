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
        private string service = "cot";
        internal DdeClient client;
        //private string item = $"{ticker}.{col}";

        /**** Strategy ****/
        internal int historySize = 20;
        internal int plot3Size = 3;
        internal int renkoPeriod = 5; //for 5R graphs
        internal bool inversionStrategy = false;

        /**** Keyboard Bindings ****/
        internal string buyKeyboard = "";
        internal string sellKeyboard = "";
        internal string zeroKeyboard = "";
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
            this.client = new DdeClient(this.app, this.service);
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
