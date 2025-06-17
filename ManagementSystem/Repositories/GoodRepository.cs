using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using ManagementSystem.Models;
using ManagementSystem.Models.Builders;
using ManagementSystem.Utils;

namespace ManagementSystem.Repositories
{
    public class GoodRepository : IGoodRepository
    {
        private readonly DatabaseContext _context;
        private readonly GoodBuilder _goodBuilder;

        public GoodRepository()
        {
            _context = DatabaseContext.Instance;
            _goodBuilder = new GoodBuilder();
        }

        public async Task<List<Good>> GetAllAsync()
        {
            var goods = new List<Good>();
            var query = SqlQueryBuilder.QuickSelect("Goods");

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = (SQLiteDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var good = _goodBuilder
                            .Reset()
                            .SetId(GetSafeInt(reader, "Id"))
                            .SetName(GetSafeString(reader, "Name"))
                            .SetDescription(GetSafeString(reader, "Description"))
                            .SetQuantity(GetSafeInt(reader, "Quantity"))
                            .SetPrice(GetSafeDecimal(reader, "Price"))
                            .SetCreatedAt(GetSafeDateTime(reader, "CreatedAt"))
                            .SetUpdatedAt(GetSafeDateTime(reader, "UpdatedAt"))
                            .BuildUnsafe();

                        goods.Add(good);
                    }
                }
            }

            return goods;
        }

        public async Task<Good> GetByIdAsync(int id)
        {
            var query = SqlQueryBuilder.QuickSelect("Goods", "Id", "id");

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = (SQLiteDataReader)await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return _goodBuilder
                                .Reset()
                                .SetId(GetSafeInt(reader, "Id"))
                                .SetName(GetSafeString(reader, "Name"))
                                .SetDescription(GetSafeString(reader, "Description"))
                                .SetQuantity(GetSafeInt(reader, "Quantity"))
                                .SetPrice(GetSafeDecimal(reader, "Price"))
                                .SetCreatedAt(GetSafeDateTime(reader, "CreatedAt"))
                                .SetUpdatedAt(GetSafeDateTime(reader, "UpdatedAt"))
                                .BuildUnsafe();
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<Good>> SearchByNameAsync(string name)
        {
            var goods = new List<Good>();
            var sanitizedName = SecurityHelper.SanitizeInput(name);

            var query = new SqlQueryBuilder()
                .SelectAll()
                .From("Goods")
                .WhereLike("Name", "name")
                .OrderBy("Name")
                .BuildSelect();

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", $"%{sanitizedName}%");

                    using (var reader = (SQLiteDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var good = _goodBuilder
                                .Reset()
                                .SetId(GetSafeInt(reader, "Id"))
                                .SetName(GetSafeString(reader, "Name"))
                                .SetDescription(GetSafeString(reader, "Description"))
                                .SetQuantity(GetSafeInt(reader, "Quantity"))
                                .SetPrice(GetSafeDecimal(reader, "Price"))
                                .SetCreatedAt(GetSafeDateTime(reader, "CreatedAt"))
                                .SetUpdatedAt(GetSafeDateTime(reader, "UpdatedAt"))
                                .BuildUnsafe();

                            goods.Add(good);
                        }
                    }
                }
            }

            return goods;
        }

        public async Task<int> AddAsync(Good good)
        {
            var validatedGood = _goodBuilder
                .Reset()
                .SetName(good.Name)
                .SetDescription(good.Description)
                .SetQuantity(good.Quantity)
                .SetPrice(good.Price)
                .Build();

            var query = SqlQueryBuilder.QuickInsert("Goods",
                "Name", "Description", "Quantity", "Price", "CreatedAt", "UpdatedAt");

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SQLiteCommand(query, connection, transaction))
                        {
                            AddParameters(command, validatedGood);
                            var result = await command.ExecuteScalarAsync();
                            var newId = Convert.ToInt32(result);

                            transaction.Commit();
                            return newId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> UpdateAsync(Good good)
        {
            var validatedGood = _goodBuilder
                .Reset()
                .SetId(good.Id)
                .SetName(good.Name)
                .SetDescription(good.Description)
                .SetQuantity(good.Quantity)
                .SetPrice(good.Price)
                .UpdateTimestamp()
                .Build();

            var query = SqlQueryBuilder.QuickUpdate("Goods", "Id",
                "Name", "Description", "Quantity", "Price", "UpdatedAt");

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SQLiteCommand(query, connection, transaction))
                        {
                            AddParameters(command, validatedGood);
                            command.Parameters.AddWithValue("@id", validatedGood.Id);

                            var affected = await command.ExecuteNonQueryAsync();
                            transaction.Commit();

                            return affected > 0;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = SqlQueryBuilder.QuickDelete("Goods");

            using (var connection = _context.GetConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            var affected = await command.ExecuteNonQueryAsync();

                            transaction.Commit();
                            return affected > 0;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private int GetSafeInt(SQLiteDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return 0;

                var value = reader[columnName];
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        private string GetSafeString(SQLiteDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return string.Empty;

                return reader[columnName]?.ToString() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private decimal GetSafeDecimal(SQLiteDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return 0m;

                var value = reader[columnName];
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0m;
            }
        }

        private DateTime GetSafeDateTime(SQLiteDataReader reader, string columnName)
        {
            try
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return DateTime.Now;

                var value = reader[columnName];
                if (DateTime.TryParse(value.ToString(), out DateTime result))
                    return result;

                return DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        private void AddParameters(SQLiteCommand command, Good good)
        {
            command.Parameters.Clear();

            command.Parameters.AddWithValue("@name", good.Name ?? string.Empty);
            command.Parameters.AddWithValue("@description", good.Description ?? string.Empty);
            command.Parameters.AddWithValue("@quantity", good.Quantity);
            command.Parameters.AddWithValue("@price", good.Price);
            command.Parameters.AddWithValue("@createdat", good.CreatedAt);
            command.Parameters.AddWithValue("@updatedat", good.UpdatedAt);
        }
    }
}