using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pombot_UI.RobotLibrary
{
    class Strategy
    {
        internal static int historySize = 20;
        internal static int plot3Size = 3;
        internal static float renkoPeriod = 3; //for 5R graphs
        internal static float temp;
        internal static bool refreshtemp = false;
    }
}
