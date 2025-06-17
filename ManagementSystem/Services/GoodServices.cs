using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem.Models;
using ManagementSystem.Models.Builders;
using ManagementSystem.Repositories;

namespace ManagementSystem.Services
{
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _repository;
        private readonly GoodBuilder _goodBuilder;

        public GoodService(IGoodRepository repository)
        {
            _repository = repository;
            _goodBuilder = new GoodBuilder();
        }

        public async Task<List<Good>> GetAllGoodsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Good> GetGoodByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Good>> SearchGoodsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllGoodsAsync();

            return await _repository.SearchByNameAsync(name);
        }

        public async Task<int> CreateGoodAsync(Good good)
        {
            var validatedGood = _goodBuilder
                .Reset()
                .SetName(good.Name)
                .SetDescription(good.Description)
                .SetQuantity(good.Quantity)
                .SetPrice(good.Price)
                .Build();

            return await _repository.AddAsync(validatedGood);
        }

        public async Task<bool> UpdateGoodAsync(Good good)
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

            return await _repository.UpdateAsync(validatedGood);
        }

        public async Task<bool> DeleteGoodAsync(int id)
        {
            if (id <= 0)
                return false;

            var existingGood = await _repository.GetByIdAsync(id);
            if (existingGood == null)
                return false;

            return await _repository.DeleteAsync(id);
        }

        public bool ValidateGood(Good good, out List<string> errors)
        {
            _goodBuilder
                .Reset()
                .SetName(good.Name)
                .SetDescription(good.Description)
                .SetQuantity(good.Quantity)
                .SetPrice(good.Price);

            errors = _goodBuilder.ValidationErrors;
            return _goodBuilder.IsValid;
        }

        public bool ValidateInput(string name, string description, int quantity, decimal price, out List<string> errors)
        {
            _goodBuilder
                .Reset()
                .SetName(name)
                .SetDescription(description)
                .SetQuantity(quantity)
                .SetPrice(price);

            errors = _goodBuilder.ValidationErrors;
            return _goodBuilder.IsValid;
        }

        public Good CreateGoodFromInput(string name, string description, int quantity, decimal price)
        {
            return _goodBuilder
                .Reset()
                .SetName(name)
                .SetDescription(description)
                .SetQuantity(quantity)
                .SetPrice(price)
                .Build();
        }

        public Good UpdateGoodFromInput(int id, string name, string description, int quantity, decimal price)
        {
            return _goodBuilder
                .Reset()
                .SetId(id)
                .SetName(name)
                .SetDescription(description)
                .SetQuantity(quantity)
                .SetPrice(price)
                .UpdateTimestamp()
                .Build();
        }
    }
}