using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pombot_UI.RobotLibrary;

namespace Pombot_UI
{
    static class Program
    {
        internal static string appName = "NoName v1.0";
        internal static string userName;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PomBot());
        }
    }
}
