using System;
using System.Text.RegularExpressions;

namespace ManagementSystem.Utils
{
    public static class SecurityHelper
    {
        // Fungsi untuk membersihkan input dari karakter berbahaya (XSS prevention)
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var sanitized = input.Replace("'", "''")
                                 .Replace("<", "&lt;")
                                 .Replace(">", "&gt;")
                                 .Replace("&", "&amp;");

            sanitized = Regex.Replace(sanitized, @"\s+", " ").Trim();
            return sanitized;
        }

        // Fungsi untuk mendeteksi pola umum SQL Injection
        public static bool ContainsSqlInjectionPatterns(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var sqlPatterns = new[]
            {
                @"(\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE)?|INSERT( +INTO)?|MERGE|SELECT|UPDATE|UNION( +ALL)?)\b)",
                @"(\b(AND|OR)\b.{1,6}?(=|>|<|!|\|\|))",
                @"(\b(CHAR|NCHAR|VARCHAR|NVARCHAR)\s*\(\s*[0-9]+)",
                @"(sp_executesql)"
            };

            foreach (var pattern in sqlPatterns)
            {
                if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                    return true;
            }
            return false;
        }

        // Fungsi untuk membuat ID acak sepanjang 8 karakter
        public static string GenerateSecureId()
        {
            // Substring digunakan agar bisa digunakan di C# 7.3
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}