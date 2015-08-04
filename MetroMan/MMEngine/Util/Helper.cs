using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEngine
{
    public class Helper
    {
        public static List<string> SplitByLine(string str)
        {
            return str.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        }

        public static List<string> SplitByComma(string str)
        {
            return str.Split(new string[] { "," }, StringSplitOptions.None).ToList();
        }

        public static List<string> SplitByCommaPlus(string str)
        {
            return str.Split(new string[] { "<,>" }, StringSplitOptions.None).ToList();
        }
    }
}
