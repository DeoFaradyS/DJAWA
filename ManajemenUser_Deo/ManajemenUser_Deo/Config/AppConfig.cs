namespace ManajemenUser_Deo.Config
{
    /// <summary>
    /// Kelas ini menyimpan konfigurasi aplikasi yang bersifat global.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Menyimpan path lokasi file data pengguna.
        /// </summary>
        public static string UserDataPath { get; set; } = "test_users.json";
    }
}
