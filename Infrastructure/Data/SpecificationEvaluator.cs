using System.Linq;
using Core.Entities;
using Core.Speficications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where  TEntity : BaseEntity 
    {
       public static IQueryable<TEntity>  GetQuery(IQueryable<TEntity> inputQuery, 
       ISpecification<TEntity> spec) 
       {
           var query= inputQuery;

           if(spec.Criteria !=null) 
           {
               query = query.Where(spec.Criteria);
           }
           
           if(spec.Orderby !=null) 
           {
               query = query.OrderBy(spec.Orderby);
           }

           if(spec.OrderbyDescending !=null) {
               query = query.OrderBy(spec.OrderbyDescending);
           }

           if(spec.IsPagingEnabled) 
           {
               query = query.Skip(spec.Skip).Take(spec.Take);
           }

           query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
           return query;
       }
    }
}