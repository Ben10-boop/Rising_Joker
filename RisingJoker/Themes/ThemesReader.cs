using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RisingJoker.Themes
{
    public class ThemesReader
    {
        public static List<T> ReadThemes<T>(string file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
    }
}
