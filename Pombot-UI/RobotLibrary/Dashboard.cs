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
        internal int historySize;
        internal int plot3Size;
        internal int renkoPeriod;
        internal bool inversionStrategy;

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
