using Newtonsoft.Json;
using System.IO;

namespace EmiasWPF10.Model
{
    internal class ConverterJson
    {
        static public string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static public void Serilaz<T>(T data, string fullPath)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(fullPath, json);
        }
        static public T Deserilaz<T>(string fullPath)
        {
            string json = File.ReadAllText(fullPath);
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
    }
}
