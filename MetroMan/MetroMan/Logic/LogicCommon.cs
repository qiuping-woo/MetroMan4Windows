using MMEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MetroMan
{
    public class LogicCommon
    {
        private const string KEY_CITY = "CITY_201508";

        public static List<City> CityList = new List<City>();
        public static List<SettingCity> SettingCityList = new List<SettingCity>();

        public static string AppCity
        {
            get
            {
                return ApplicationData.Current.LocalSettings.Values.ContainsKey(KEY_CITY) ? ApplicationData.Current.LocalSettings.Values[KEY_CITY].ToString() : string.Empty;
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values[KEY_CITY] = value;
            }
        }

        public static async Task InitApp()
        {
            //Get OS Lang
            Language lang = Helper.GetOSLang();
            //Load mmcity.csv
            var file = await Helper.GetData("mmcity.csv");
            var lineList = MMEngine.Helper.SplitByLine(file);
            foreach (var line in lineList)
            {
                CityList.Add(new City(line));
            }
            //Load Setting
            if (string.IsNullOrEmpty(AppCity)) AppCity = "sh";
            //Init SettingCity
            foreach (City city in CityList)
            {
                SettingCity settingCity = new SettingCity();
                settingCity.CD = city.CD;
                settingCity.MainName = city.GetMainName(lang);
                settingCity.SubName = city.GetSubName(lang);
                SettingCityList.Add(settingCity);
            }
        }
    }
}
