using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly ApplicationDbContext _context;

    public OrderDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> GetProducstByOrderId(int id)
    {
        var products = await _context.Orderdetails
            .Where(od => od.OrderId == id)
            .Select(od => new ProductDto
            {
                Name = od.Product.Name,
                Description = od.Product.Description,
                Price = od.Product.Price,
            })
            .ToListAsync();
        return products;
    }
}