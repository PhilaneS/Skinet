using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Speficications
{
    public class ProductsWithTypeAndBrandsSpecification : BaseSpecificaton<Product>
    {
        public ProductsWithTypeAndBrandsSpecification(ProductSpecParams prodParams)
        : base(x => 
        (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search)) &&
        (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) &&
         (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId))
        {
            AddIncdude(x=> x.ProductType);
            AddIncdude(x=> x.ProductBrand);
            AddOrderBy(s => s.Name);

            ApplyPaging(prodParams.PageSize * (prodParams.PageIndex - 1),prodParams.PageSize);  

            if(!string.IsNullOrEmpty(prodParams.sort))
            {
                switch(prodParams.sort)
                {
                    case "priceAsc": 
                    AddOrderBy(p => p.Price);
                    break;
                    case "priceDesc":
                    AddOrderbyDescending(p=> p.Price);
                    break;
                    default:
                    AddOrderBy(n => n.Name);
                    break;
                }
            }
        }

        public ProductsWithTypeAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
            AddIncdude(x=> x.ProductType);
            AddIncdude(x=> x.ProductBrand);
        }
    }
}