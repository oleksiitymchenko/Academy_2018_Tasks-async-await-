using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task CreateAsync(T crew);
        Task UpdateAsync(int id, T crew);
        Task DeleteAsync(int id);
    }
}
