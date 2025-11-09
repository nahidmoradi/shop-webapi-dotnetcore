using Shop.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
    Task<Product?> GetProductWithCategoryByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
}
