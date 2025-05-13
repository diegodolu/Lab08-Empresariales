using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersAfterDateAsync(DateTime date)
    {
        return await _context.Orders
            .Where(o => o.OrderDate > date)
            .ToListAsync();
    }
    
    public async Task<ClientDto> GetClientWithMostOrdersAsync()
    {
        var clientWithMostOrders = await _context.Orders
            .GroupBy(o => o.ClientId)  
            .OrderByDescending(g => g.Count())  
            .Select(g => new 
            {
                ClientId = g.Key,
                OrderCount = g.Count()
            })
            .FirstOrDefaultAsync();  

        if (clientWithMostOrders == null)
        {
            return null; 
        }

       
        var client = await _context.Clients
            .Where(c => c.ClientId == clientWithMostOrders.ClientId)
            .FirstOrDefaultAsync();

        if (client == null)
        {
            return null; 
        }

        return new ClientDto
        {
            Name = client.Name,
        };
    }

    public async Task<List<OrderDetailsDto>> GetOrdersWithDetails()
    {
        var ordersWithDetails = await _context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .Select(order => new OrderDetailsDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Products = order.Orderdetails
                    .Select(od => new ProductDto2
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        Price = od.Product.Price
                    }).ToList()
            }).ToListAsync();

        return ordersWithDetails;
    }
    
    
    
}