using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        internal string app = "profitchart";
        internal string service = "cot";

        /**** Strategy ****/
        internal int historySize = 20;
        internal int plot3Size = 3;
        internal int renkoPeriod = 5; //for 5R graphs
        internal bool inversionStrategy = false;

        /**** Keyboard Bindings ****/
        internal List<Keys> buyKeyboard = new List<Keys>();
        internal List<Keys> sellKeyboard = new List<Keys>();
        internal List<Keys> zeroKeyboard = new List<Keys>();
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
    }
}
