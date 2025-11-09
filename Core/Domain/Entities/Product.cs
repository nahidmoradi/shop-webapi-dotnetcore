using System;

namespace Shop.Core.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string SKU { get; set; } = string.Empty;    
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
}
