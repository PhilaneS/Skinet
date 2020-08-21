
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                            .Include(p=>p.ProductBrand)
                            .Include(p=> p.ProductType)            
                            .FirstOrDefaultAsync(predicate=> predicate.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsysnc()
        {
            return await _context.Products
                            .Include(p=>p.ProductBrand)
                            .Include(p=> p.ProductType)
                            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsysnc()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsysnc()
        {
            return await _context.ProductTypes.ToListAsync() ;
        }
    }
}