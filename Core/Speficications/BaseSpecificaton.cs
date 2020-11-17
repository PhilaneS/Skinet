using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Speficications
{
    public class BaseSpecificaton<T> : ISpecification<T>
    {
        
        public BaseSpecificaton()
        {
            
        }
        public BaseSpecificaton(Expression<Func<T,bool>>  criteria)
        {
            Criteria = criteria;
        }        

        public  Expression<Func<T,bool>>  Criteria {get;}
        public  List<Expression<Func<T,object>>> Includes {get;} =
        new List<Expression<Func<T,object>>>();

        public Expression<Func<T, object>> Orderby {get; private set;}

        public Expression<Func<T, object>> OrderbyDescending {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        protected void AddIncdude (Expression<Func<T,object>>  includeExpression)
       {
           Includes.Add(includeExpression);
       } 
       protected void AddOrderBy(Expression<Func<T,object>>  OrderByExpression)
       {
            Orderby = OrderByExpression;
       }
       protected void AddOrderbyDescending(Expression<Func<T,object>>  OrderByDescExpression)
       {
            OrderbyDescending = OrderByDescExpression;
       }
       protected void ApplyPaging(int skip, int take)
       {
           Skip = skip;
           Take = take;
           IsPagingEnabled = true;
       }
    }      
}
