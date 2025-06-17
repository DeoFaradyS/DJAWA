using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem.Models;

namespace ManagementSystem.Services
{
    public interface IGoodService
    {
        Task<List<Good>> GetAllGoodsAsync();
        Task<Good> GetGoodByIdAsync(int id);
        Task<List<Good>> SearchGoodsByNameAsync(string name);
        Task<int> CreateGoodAsync(Good good);
        Task<bool> UpdateGoodAsync(Good good);
        Task<bool> DeleteGoodAsync(int id);
        bool ValidateGood(Good good, out List<string> errors);
        bool ValidateInput(string name, string description, int quantity, decimal price, out List<string> errors);
        Good CreateGoodFromInput(string name, string description, int quantity, decimal price);
        Good UpdateGoodFromInput(int id, string name, string description, int quantity, decimal price);
    }
}