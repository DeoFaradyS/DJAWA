using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementSystem.Models.Builders
{
    public class SqlQueryBuilder
    {
        private StringBuilder _query;
        private List<string> _columns;
        private List<string> _values;
        private List<string> _conditions;
        private List<string> _updates;
        private string _tableName;
        private string _orderBy;
        private int? _limit;

        public SqlQueryBuilder()
        {
            Reset();
        }

        public SqlQueryBuilder Reset()
        {
            _query = new StringBuilder();
            _columns = new List<string>();
            _values = new List<string>();
            _conditions = new List<string>();
            _updates = new List<string>();
            _tableName = string.Empty;
            _orderBy = string.Empty;
            _limit = null;
            return this;
        }

        public SqlQueryBuilder Select(params string[] columns)
        {
            _columns.AddRange(columns);
            return this;
        }

        public SqlQueryBuilder SelectAll()
        {
            _columns.Clear();
            _columns.Add("*");
            return this;
        }

        public SqlQueryBuilder From(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public SqlQueryBuilder Where(string condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public SqlQueryBuilder WhereEquals(string column, string parameterName)
        {
            _conditions.Add($"{column} = @{parameterName}");
            return this;
        }

        public SqlQueryBuilder WhereLike(string column, string parameterName)
        {
            _conditions.Add($"{column} LIKE @{parameterName}");
            return this;
        }

        public SqlQueryBuilder OrderBy(string column, bool ascending = true)
        {
            _orderBy = $"ORDER BY {column} {(ascending ? "ASC" : "DESC")}";
            return this;
        }

        public SqlQueryBuilder Limit(int count)
        {
            _limit = count;
            return this;
        }

        public SqlQueryBuilder InsertInto(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public SqlQueryBuilder Values(string column, string parameterName)
        {
            _columns.Add(column);
            _values.Add($"@{parameterName}");
            return this;
        }

        public SqlQueryBuilder Update(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public SqlQueryBuilder Set(string column, string parameterName)
        {
            _updates.Add($"{column} = @{parameterName}");
            return this;
        }

        public SqlQueryBuilder DeleteFrom(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public string BuildSelect()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new InvalidOperationException("Table name is required for SELECT query");

            _query.Clear();
            _query.Append("SELECT ");

            if (_columns.Count == 0)
                _query.Append("*");
            else
                _query.Append(string.Join(", ", _columns));

            _query.Append($" FROM {_tableName}");

            if (_conditions.Count > 0)
                _query.Append($" WHERE {string.Join(" AND ", _conditions)}");

            if (!string.IsNullOrEmpty(_orderBy))
                _query.Append($" {_orderBy}");

            if (_limit.HasValue)
                _query.Append($" LIMIT {_limit.Value}");

            return _query.ToString();
        }

        public string BuildInsert()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new InvalidOperationException("Table name is required for INSERT query");

            if (_columns.Count == 0 || _values.Count == 0)
                throw new InvalidOperationException("Columns and values are required for INSERT query");

            if (_columns.Count != _values.Count)
                throw new InvalidOperationException("Number of columns must match number of values");

            _query.Clear();
            _query.Append($"INSERT INTO {_tableName} ");
            _query.Append($"({string.Join(", ", _columns)}) ");
            _query.Append($"VALUES ({string.Join(", ", _values)})");

            return _query.ToString();
        }

        public string BuildInsertWithReturnId()
        {
            var insertQuery = BuildInsert();
            return $"{insertQuery}; SELECT last_insert_rowid();";
        }

        public string BuildUpdate()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new InvalidOperationException("Table name is required for UPDATE query");

            if (_updates.Count == 0)
                throw new InvalidOperationException("SET clauses are required for UPDATE query");

            _query.Clear();
            _query.Append($"UPDATE {_tableName} ");
            _query.Append($"SET {string.Join(", ", _updates)}");

            if (_conditions.Count > 0)
                _query.Append($" WHERE {string.Join(" AND ", _conditions)}");

            return _query.ToString();
        }

        public string BuildDelete()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new InvalidOperationException("Table name is required for DELETE query");

            _query.Clear();
            _query.Append($"DELETE FROM {_tableName}");

            if (_conditions.Count > 0)
                _query.Append($" WHERE {string.Join(" AND ", _conditions)}");

            return _query.ToString();
        }

        public static string QuickSelect(string tableName, string whereColumn = null, string parameterName = null)
        {
            var builder = new SqlQueryBuilder()
                .SelectAll()
                .From(tableName);

            if (!string.IsNullOrEmpty(whereColumn) && !string.IsNullOrEmpty(parameterName))
                builder.WhereEquals(whereColumn, parameterName);

            return builder.BuildSelect();
        }

        public static string QuickInsert(string tableName, params string[] columns)
        {
            var builder = new SqlQueryBuilder().InsertInto(tableName);

            foreach (var column in columns)
            {
                builder.Values(column, column.ToLower());
            }

            return builder.BuildInsertWithReturnId();
        }

        public static string QuickUpdate(string tableName, string idColumn, params string[] updateColumns)
        {
            var builder = new SqlQueryBuilder()
                .Update(tableName)
                .WhereEquals(idColumn, "id");

            foreach (var column in updateColumns)
            {
                builder.Set(column, column.ToLower());
            }

            return builder.BuildUpdate();
        }

        public static string QuickDelete(string tableName, string idColumn = "Id")
        {
            return new SqlQueryBuilder()
                .DeleteFrom(tableName)
                .WhereEquals(idColumn, "id")
                .BuildDelete();
        }
    }
}