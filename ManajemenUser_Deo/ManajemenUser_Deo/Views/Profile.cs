using ManajemenUser_Deo.Controllers;
using ManajemenUser_Deo.UserManagement;

namespace ManajemenUser_Deo.Views
{
    class Profile
    {
        private readonly UserController _userController = new UserController();
        private readonly AuthController _authController = new AuthController();

        public void ShowProfile(User user)
        {
            bool isRunning = true;

            while (isRunning) 
            {
                Console.Clear();
                _userController.PrintUserDetails(user);

                string choice = ShowProfileOptions();

                switch (choice)
                {
                    case "1":
                        EditProfile(user);
                        break;
                    case "2":
                        _authController.Logout();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nOpsi tidak valid. Tekan tombol apa saja untuk lanjut...");
                        Thread.Sleep(2000);
                        break;
                }
            }            
        }

        private string ShowProfileOptions()
        {
            Console.WriteLine("\nPilih opsi:");
            Console.WriteLine("1. Edit Profil");
            Console.WriteLine("2. Logout");
            Console.Write("Pilih opsi (1/2): ");
            return Console.ReadLine();
        }

        private void EditProfile(User user)
        {
            Console.Clear();
            Console.WriteLine("=== Edit Profile ===");

            Console.Write($"Masukkan nama baru (kosongkan untuk tetap, current: {user.Name}): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) user.Name = newName;

            Console.Write($"Masukkan email baru (kosongkan untuk tetap, current: {user.Email}): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail)) user.Email = newEmail;

            Console.Write($"Masukkan password baru (kosongkan untuk tetap, current: {user.Password}): ");
            string newPassword = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPassword)) user.Password = newPassword;

            
            bool updateSuccess = _userController.UpdateProfile(user.Id, user.Name, user.Email, user.Password);

            // Tampilkan hasil update
            Console.WriteLine(updateSuccess
                ? "\nProfil telah berhasil diperbarui."
                : "\nTerjadi kesalahan saat memperbarui profil.");

            Thread.Sleep(2000);
            return;
        }

    }
}
