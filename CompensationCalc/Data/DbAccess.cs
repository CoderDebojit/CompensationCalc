using Newtonsoft.Json;
using System.Net;

namespace CompensationCalc.Data
{
    public class DbAccess<T1>
    {
        public List<T1> ReadData(string path)
        {
            var _jsonData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T1>>(_jsonData); ;
        }

        public void WriteData(string path, List<T1> _jsonData)
        {
            //string json = JsonConvert.SerializeObject(_jsonData.ToArray());

            File.WriteAllText(path, JsonConvert.SerializeObject(_jsonData.ToArray(), Formatting.Indented));
        }
    }
}
