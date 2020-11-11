using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Speficications
{
    public class ProductsWithTypeAndBrandsSpecification : BaseSpecificaton<Product>
    {
        public ProductsWithTypeAndBrandsSpecification()
        {
            AddIncdude(x=> x.ProductType);
            AddIncdude(x=> x.ProductBrand);
        }

        public ProductsWithTypeAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
            AddIncdude(x=> x.ProductType);
            AddIncdude(x=> x.ProductBrand);
        }
    }
}