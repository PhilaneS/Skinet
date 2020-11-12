using Core.Entities;

namespace Core.Speficications
{
    public class ProductWithFilterForCountSpecification : BaseSpecificaton<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams prodParams)
        : base(x => 
        (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search)) &&
        (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) && 
        (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId))
        {
            
        }
    }
}