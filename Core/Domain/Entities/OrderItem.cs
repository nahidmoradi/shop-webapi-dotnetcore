using System;

namespace Shop.Core.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
