using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Interfaces
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T crew);
        void Update(int id, T crew);
        void Delete(int id);
    }
}
