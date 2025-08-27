using System;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Domain;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
