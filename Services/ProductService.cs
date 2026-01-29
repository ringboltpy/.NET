using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    // CRUD OPERATIONS

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        if (!_context.Products.Any(p => p.Id == product.Id))
            return false;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    // CATEGORY-BASED OPERATIONS

    public async Task<List<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPriceByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .SumAsync(p => p.Price);
    }

    public async Task<Dictionary<string, decimal>> GetTotalPricePerCategoryAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .GroupBy(p => p.Category!.Name)
            .Select(g => new
            {
                CategoryName = g.Key,
                TotalPrice = g.Sum(p => p.Price)
            })
            .ToDictionaryAsync(x => x.CategoryName, x => x.TotalPrice);
    }
}
