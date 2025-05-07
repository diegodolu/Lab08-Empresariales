using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> GetProductsWithPriceHigherThan(int value)
    {
        var products = await _context.Products
            .Where(p => p.Price > value).Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToListAsync();
        return products;
    }

    public async Task<ProductDto?> GetMostExpensiveProduct()
    {
        return await _context.Products
            .OrderByDescending(p => p.Price)
            .Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetAverageProductPriceAsync()
    {
        var averagePrice = await _context.Products
            .Select(p => p.Price)  
            .AverageAsync();      

        return averagePrice;
    }

    public async Task<List<ProductDto>> GetProductsWithoutDescriptionAsync()
    {
        var productsWithoutDescription = await _context.Products
            .Where(p => string.IsNullOrEmpty(p.Description))  
            .Select(p => new ProductDto  
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToListAsync(); 

        return productsWithoutDescription;
    }
}