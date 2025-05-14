using System.Text.Json;

namespace JsonHelper
{
    /// <summary>
    /// Kelas ini menyediakan metode untuk memuat dan menyimpan data dalam format JSON.
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// Menyimpan data ke dalam file JSON di lokasi yang ditentukan.
        /// </summary>
        /// <typeparam name="T">Tipe data yang akan diserialisasi ke dalam JSON.</typeparam>
        /// <param name="data">Data yang akan disimpan dalam file JSON.</param>
        /// <param name="filePath">Lokasi file tempat data akan disimpan.</param>
        public static void SaveToJson<T>(List<T> data, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString);
        }

        /// <summary>
        /// Memuat data dari file JSON yang disimpan di lokasi yang ditentukan.
        /// </summary>
        /// <typeparam name="T">Tipe data yang akan dideserialisasi dari JSON.</typeparam>
        /// <param name="filePath">Lokasi file tempat data disimpan.</param>
        /// <returns>Daftar data yang dimuat dari file JSON.</returns>
        public static List<T> LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
        }
    }
}
