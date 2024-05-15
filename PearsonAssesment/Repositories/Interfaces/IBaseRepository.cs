using System.Linq.Expressions;

namespace PearsonAssesment.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        T GetById(string id);
        T Add(T entity);
        void Update(T entity);

    }
}
