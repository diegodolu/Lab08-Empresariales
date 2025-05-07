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
    
    
}