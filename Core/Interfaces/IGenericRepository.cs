using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Speficications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity    {
         Task<T> GetByAsyId(int id);
         Task<IReadOnlyList<T>> ListAllAsynch();
         Task<T> GetEntityWithSpec(ISpecification<T> spec);
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
         Task<int> CountAsynch(ISpecification<T> spec);
         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);        
    }
}