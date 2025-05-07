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

    public async Task<int> GetTotalProductQuantityByOrderIdAsync(int id)
    {
        return await _context.Orderdetails
            .Where(od => od.OrderId == id)
            .SumAsync(od => od.Quantity);
    }

    public async Task<List<OrderProductDto>> GetAllOrderDetailsAsync()
    {
        var orderDetails = await _context.Orderdetails
            .Join(_context.Products, 
                od => od.ProductId, 
                p => p.ProductId, 
                (od, p) => new { od.OrderId, p.Name, od.Quantity })
            .Select(x => new OrderProductDto
            {
                Name = x.Name,
                Quantity = x.Quantity
            })
            .ToListAsync();

        return orderDetails;
    }

    public async Task<List<string>> GetProductsSoldToClientAsync(int clientId)
    {
        var products = await _context.Orders
            .Where(o => o.ClientId == clientId)  
            .Join(_context.Orderdetails, 
                o => o.OrderId, 
                od => od.OrderId, 
                (o, od) => new { o.ClientId, od.ProductId }) 
            .Join(_context.Products, 
                od => od.ProductId, 
                p => p.ProductId, 
                (od, p) => p.Name)  
            .ToListAsync();

        return products;
    }

    public async Task<List<string>> GetClientsByProductIdAsync(int id)
    {
        var clients = await _context.Orderdetails
            .Where(od => od.ProductId == id)
            .Join(_context.Orders,
                od => od.OrderId,
                o => o.OrderId,
                (od, o) => o)
            .Join(_context.Clients,
                o => o.ClientId,
                c => c.ClientId,
                (o, c) => c.Name)
            .Distinct()
            .ToListAsync();

        return clients;
    }
}