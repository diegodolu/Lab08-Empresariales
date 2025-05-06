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
}