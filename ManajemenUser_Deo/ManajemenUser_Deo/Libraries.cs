using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManajemenUser_Deo
{
    internal class Libraries
    {
        public static void SaveToJson<T>(List<T> data, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        public static List<T> LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
