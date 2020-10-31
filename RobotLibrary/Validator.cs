using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pombot_UI.RobotLibrary
{
    public static class Validator
    {
        public static bool isDigit(string text)
        {
            Regex pattern = new Regex(@"^[0-9]{0,10}([.][0-9]{0,10})?$");
            return pattern.IsMatch(text);
        }
    }
}
