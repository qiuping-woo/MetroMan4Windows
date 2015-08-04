using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMEngine;

namespace MetroMan
{
    public class Helper
    {
        public static Language GetOSLang()
        {
            Language result = Language.English;
            try
            {
                string lang = System.Globalization.CultureInfo.CurrentUICulture.Name.ToLower();
                if (lang.StartsWith("en")) result = Language.English;
                else if (lang.StartsWith("zh"))
                {
                    if (lang.Contains("cn")) result = Language.Simplified;
                    else result = Language.Traditional;
                }
                else if (lang.StartsWith("ja")) result = Language.Japanese;
                else if (lang.StartsWith("ko")) result = Language.Korean;
                else if (lang.StartsWith("ru")) result = Language.Russian;
                else if (lang.StartsWith("fr")) result = Language.French;
                else if (lang.StartsWith("es")) result = Language.Spanish;
                else if (lang.StartsWith("de")) result = Language.German;
                else if (lang.StartsWith("pt")) result = Language.Portuguese;
                else if (lang.StartsWith("it")) result = Language.Italian;
                else result = Language.English;
            }
            catch (Exception)
            {
                result = Language.English;
            }
            return result;
        }

        public static async Task<string> GetData(string fileName)
        {
            var appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var dataFolder = await appFolder.GetFolderAsync(@"Assets\data");
            var file = await dataFolder.GetFileAsync(fileName);
            return await Windows.Storage.FileIO.ReadTextAsync(file);
        }
    }
}
