using System.Text.Json;

namespace JsonHelper
{
    /// <summary>
    /// Utility untuk menyimpan dan memuat data JSON.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Simpan data ke file JSON.
        /// </summary>
        public static void SaveToJson<T>(List<T> data, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path tidak boleh kosong.", nameof(filePath));

            try
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // Bisa ganti ke logging framework jika perlu
                Console.Error.WriteLine($"Gagal menyimpan data ke JSON: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Muat data dari file JSON.
        /// </summary>
        public static List<T> LoadFromJson<T>(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path tidak boleh kosong.", nameof(filePath));

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File JSON tidak ditemukan, mengembalikan list kosong.");
                return new List<T>();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Gagal memuat data dari JSON: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
