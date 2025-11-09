using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain.Entities;
using Shop.Core.Domain.Interfaces;
using Shop.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
    {
        return await _dbSet.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductWithCategoryByIdAsync(int id)
    {
        return await _dbSet.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet.Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }
}
