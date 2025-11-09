using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain.Entities;

namespace Shop.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (context.Categories.Any()) return;

         var categories = new[]
        {
            new Category { Name = "لپ تاپ", Slug = "laptop", Description = "انواع لپ تاپ", IsActive = true },
            new Category { Name = "موبایل", Slug = "mobile", Description = "انواع گوشی موبایل", IsActive = true },
            new Category { Name = "تبلت", Slug = "tablet", Description = "انواع تبلت", IsActive = true },
            new Category { Name = "لوازم جانبی", Slug = "accessories", Description = "لوازم جانبی", IsActive = true }
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();

        var products = new[]
        {
            new Product
            {
                Name = "لپ تاپ ایسوس VivoBook",
                SKU = "LAP-001",
                Description = "لپ تاپ ایسوس با پردازنده Core i5",
                ShortDescription = "مناسب کار و تحصیل",
                Price = 25000000,
                DiscountPrice = 23000000,
                Stock = 10,
                CategoryId = categories[0].Id,
                IsActive = true,
                IsFeatured = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                Name = "گوشی سامسونگ Galaxy A54",
                SKU = "MOB-001",
                Description = "گوشی سامسونگ با صفحه نمایش AMOLED",
                ShortDescription = "دوربین 50 مگاپیکسل",
                Price = 15000000,
                Stock = 25,
                CategoryId = categories[1].Id,
                IsActive = true,
                IsFeatured = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                Name = "تبلت سامسونگ Tab S9",
                SKU = "TAB-001",
                Description = "تبلت پرقدرت سامسونگ",
                ShortDescription = "صفحه 11 اینچ",
                Price = 18000000,
                DiscountPrice = 16500000,
                Stock = 8,
                CategoryId = categories[2].Id,
                IsActive = true,
                CreatedAt = DateTime.Now
            }
        };
        context.Products.AddRange(products);
        context.SaveChanges();
    }
}