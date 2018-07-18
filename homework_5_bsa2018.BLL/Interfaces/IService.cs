using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T crew);
        Task Update(int id, T crew);
        Task Delete(int id);
    }
}
