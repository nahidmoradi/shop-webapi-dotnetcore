using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain.Entities;
using Shop.Core.Domain.Interfaces;
using Shop.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).ToListAsync();
    }

    public async Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        return await _dbSet.Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug);
    }
}
