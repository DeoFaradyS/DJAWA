using System.Collections.Generic;
using System.Text.RegularExpressions;
using ManagementSystem.Models;

namespace ManagementSystem.Utils
{
    public static class ValidationHelper
    {
        public static bool ValidateGood(Good good, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(good.Name))
                errors.Add("Nama barang wajib diisi");
            else if (good.Name.Length > 100)
                errors.Add("Nama maksimal 100 karakter");
            else if (!IsValidName(good.Name))
                errors.Add("Nama hanya boleh mengandung huruf, angka, dan spasi");

            if (!string.IsNullOrEmpty(good.Description) && good.Description.Length > 500)
                errors.Add("Deskripsi maksimal 500 karakter");

            if (good.Quantity < 0)
                errors.Add("Jumlah harus positif atau nol");

            if (good.Price <= 0)
                errors.Add("Harga harus lebih dari 0");

            return errors.Count == 0;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return Regex.IsMatch(name, @"^[a-zA-Z0-9\s\-_]+$");
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            return Regex.IsMatch(phoneNumber, @"^(\+62|62|0)[0-9]{8,13}$");
        }

        public static bool IsPositiveNumber(decimal number)
        {
            return number > 0;
        }

        public static bool IsNonNegativeNumber(decimal number)
        {
            return number >= 0;
        }

        public static bool IsValidLength(string input, int minLength, int maxLength)
        {
            if (input == null)
                return minLength <= 0;

            return input.Length >= minLength && input.Length <= maxLength;
        }

        public static bool IsSafeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            var dangerousPatterns = new[]
            {
                @"(\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE)?|INSERT( +INTO)?|MERGE|SELECT|UPDATE|UNION( +ALL)?)\b)",
                @"(\b(AND|OR)\b.{1,6}?(=|>|<|!|\|\|))",
                @"('|('')|(""|(""))",
                @"(<script|</script>|javascript:|vbscript:)",
                @"(eval\s*\(|expression\s*\()"
            };

            foreach (var pattern in dangerousPatterns)
            {
                if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                    return false;
            }

            return true;
        }
    }
}