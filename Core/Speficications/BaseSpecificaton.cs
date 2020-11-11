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

       protected void AddIncdude (Expression<Func<T,object>>  includeExpression)
       {
           Includes.Add(includeExpression);
       } 
    }      
}