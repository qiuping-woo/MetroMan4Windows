using System;
using System.Collections.Generic;

namespace MMEngine
{
    public class City
    {
        public int GroupID;
        public string CD;
        public double Latitude;
        public double Longitude;
        public string English;
        public string Simplified;
        public string Traditional;
        public string Japanese;
        public string Korean;
        public string Russian;
        public string French;
        public string Spanish;
        public string German;
        public string Portuguese;
        public string Italian;

        public City(string str)
        {
            List<string> list = Helper.SplitByCommaPlus(str);
            GroupID = Convert.ToInt32(list[0]);
            CD = list[1];
            Latitude = Convert.ToDouble(list[2]);
            Longitude = Convert.ToDouble(list[3]);
            English = list[4];
            Simplified = list[5];
            Traditional = list[6];
            Japanese = list[7];
            Korean = list[8];
            Russian = list[9];
            French = list[10];
            Spanish = list[11];
            German = list[12];
            Portuguese = list[13];
            Italian = list[14];
        }

        public string GetMainName(Language lang)
        {
            string result = string.Empty;
            if (lang == Language.English) result = English;
            else if (lang == Language.Simplified) result = Simplified;
            else if (lang == Language.Traditional) result = Traditional;
            else if (lang == Language.Japanese) result = Japanese;
            else if (lang == Language.Korean) result = Korean;
            else if (lang == Language.Russian) result = Russian;
            else if (lang == Language.French) result = French;
            else if (lang == Language.Spanish) result = Spanish;
            else if (lang == Language.German) result = German;
            else if (lang == Language.Portuguese) result = Portuguese;
            else if (lang == Language.Italian) result = Italian;
            return result;
        }

        public string GetSubName(Language lang)
        {
            string result = string.Empty;
            if (lang == Language.English) result = Simplified;
            else if (lang == Language.Simplified) result = English;
            else if (lang == Language.Traditional) result = English;
            else if (lang == Language.Japanese) result = English;
            else if (lang == Language.Korean) result = English;
            else if (lang == Language.Russian) result = English;
            else if (lang == Language.French) result = Simplified;
            else if (lang == Language.Spanish) result = Simplified;
            else if (lang == Language.German) result = Simplified;
            else if (lang == Language.Portuguese) result = Simplified;
            else if (lang == Language.Italian) result = Simplified;
            return result;
        }
    }
}
