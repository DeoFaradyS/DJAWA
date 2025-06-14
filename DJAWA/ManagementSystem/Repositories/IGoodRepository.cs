using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementSystem.Models;

namespace ManagementSystem.Repositories
{
    public interface IGoodRepository
    {
        Task<List<Good>> GetAllAsync();
        Task<Good> GetByIdAsync(int id);
        Task<List<Good>> SearchByNameAsync(string name);
        Task<int> AddAsync(Good good);
        Task<bool> UpdateAsync(Good good);
        Task<bool> DeleteAsync(int id);
    }
}