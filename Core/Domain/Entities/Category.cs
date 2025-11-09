using System.Collections.Generic;

namespace Shop.Core.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public string Slug { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
