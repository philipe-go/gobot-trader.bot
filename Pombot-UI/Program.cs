using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pombot_UI.RobotLibrary;

namespace Pombot_UI
{
    static class Program
    {
        internal static string appName = "GObot";
        internal static Version appVersion;
        internal static string userName;
        
        [STAThread]
        static void Main()
        {
            appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PomBot());
        }
    }
}
