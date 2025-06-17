using System;
using System.Data.SQLite;
using System.IO;

namespace ManagementSystem.Models
{
    public class DatabaseContext
    {
        private readonly string _connectionString;
        private static DatabaseContext _instance;
        private static readonly object _lock = new object();

        private DatabaseContext()
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "goods.db");
            _connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new DatabaseContext();
                    }
                }
                return _instance;
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        private void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Goods (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        Quantity INTEGER NOT NULL DEFAULT 0,
                        Price DECIMAL(10,2) NOT NULL,
                        CreatedAt DATETIME NOT NULL,
                        UpdatedAt DATETIME NOT NULL
                    )";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}