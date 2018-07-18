using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task Create(TEntity item);

        Task Update(int id, TEntity item);

        void Delete(int id);
    }
}
