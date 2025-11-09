using Shop.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    Task<Category?> GetCategoryWithProductsAsync(int id);
    Task<Category?> GetBySlugAsync(string slug);
}
