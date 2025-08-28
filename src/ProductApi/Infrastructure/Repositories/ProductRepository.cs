namespace ProductApi.Infrastructure.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Domain;
using ProductApi.Common.Interfaces;
using ProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<int> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.ProductId;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Products.AnyAsync(p => p.Name == name);
    }
}
