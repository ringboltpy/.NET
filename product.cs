namespace Domain.Models;

public class Product
{
    public int Id { get; set; } // Unique identifier
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    // Navigation property
    public Category? Category { get; set; }

    // Optional properties
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
