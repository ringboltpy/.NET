namespace Domain.Models;

public class Category
{
    public int Id { get; set; } // Unique identifier
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();

    // Optional properties
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
