using System;
using System.Collections.Generic;
using ManagementSystem.Models;
using ManagementSystem.Utils;

namespace ManagementSystem.Models.Builders
{
    public class GoodBuilder
    {
        private Good _good;
        private List<string> _validationErrors;

        public GoodBuilder()
        {
            Reset();
        }

        public GoodBuilder Reset()
        {
            _good = new Good();
            _validationErrors = new List<string>();
            return this;
        }

        public GoodBuilder SetId(int id)
        {
            _good.Id = id;
            return this;
        }

        public GoodBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _validationErrors.Add("Nama barang wajib diisi");
            }
            else if (name.Length > 100)
            {
                _validationErrors.Add("Nama maksimal 100 karakter");
            }
            else if (!ValidationHelper.IsValidName(name))
            {
                _validationErrors.Add("Nama hanya boleh mengandung huruf, angka, dan spasi");
            }
            else
            {
                _good.Name = SecurityHelper.SanitizeInput(name.Trim());
            }
            return this;
        }

        public GoodBuilder SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                _validationErrors.Add("Deskripsi maksimal 500 karakter");
            }
            else
            {
                _good.Description = SecurityHelper.SanitizeInput(description?.Trim() ?? string.Empty);
            }
            return this;
        }

        public GoodBuilder SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                _validationErrors.Add("Jumlah harus positif atau nol");
            }
            else
            {
                _good.Quantity = quantity;
            }
            return this;
        }

        public GoodBuilder SetPrice(decimal price)
        {
            if (price <= 0)
            {
                _validationErrors.Add("Harga harus lebih dari 0");
            }
            else
            {
                _good.Price = price;
            }
            return this;
        }

        public GoodBuilder SetCreatedAt(DateTime createdAt)
        {
            _good.CreatedAt = createdAt;
            return this;
        }

        public GoodBuilder SetUpdatedAt(DateTime updatedAt)
        {
            _good.UpdatedAt = updatedAt;
            return this;
        }

        public GoodBuilder UpdateTimestamp()
        {
            _good.UpdateTimestamp();
            return this;
        }

        public bool IsValid => _validationErrors.Count == 0;

        public List<string> ValidationErrors => new List<string>(_validationErrors);

        public Good Build()
        {
            if (!IsValid)
            {
                throw new ValidationException($"Validasi gagal: {string.Join(", ", _validationErrors)}");
            }

            var result = new Good
            {
                Id = _good.Id,
                Name = _good.Name,
                Description = _good.Description,
                Quantity = _good.Quantity,
                Price = _good.Price,
                CreatedAt = _good.CreatedAt,
                UpdatedAt = _good.UpdatedAt
            };

            Reset();
            return result;
        }

        public Good BuildUnsafe()
        {
            var result = new Good
            {
                Id = _good.Id,
                Name = _good.Name ?? string.Empty,
                Description = _good.Description ?? string.Empty,
                Quantity = _good.Quantity,
                Price = _good.Price,
                CreatedAt = _good.CreatedAt,
                UpdatedAt = _good.UpdatedAt
            };

            Reset();
            return result;
        }

        public bool Validate()
        {
            return IsValid;
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}