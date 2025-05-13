using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<ClientDto>> NamedBy(string name)
    {
        var list = await _context.Clients
            .Where(c => c.Name.StartsWith(name))
            .Select(c => new ClientDto
            {
                Name = c.Name,
            })
            .ToListAsync();

        return list;
    }

    public async Task<List<ClientOrderDto>> GetClientsWithOrders()
    {
        var clientOrders = await _context.Clients.AsNoTracking().Select(client => new ClientOrderDto
        {
            ClientName = client.Name,
            Orders = client.Orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate
            }).ToList()
        }).ToListAsync();

        return clientOrders;
    }

    public async Task<List<ClientProductsDto>> GetClientWithTotalPurchasedProducts()
    {
        var clientsWithProductCount = await _context.Clients
            .AsNoTracking()
            .Select(client => new ClientProductsDto
            {
                ClientName = client.Name,
                TotalProducts = client.Orders
                    .Sum(order => order.Orderdetails
                        .Sum(detail => detail.Quantity))
            })
            .ToListAsync();

        return clientsWithProductCount;
    }

    public async Task<List<SalesClientDto>> GetClientWithTotalSales()
    {
        var clientes = await _context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .GroupBy(order => order.ClientId)
            .Select(group => new SalesClientDto
            {
                ClientName = _context.Clients
                    .FirstOrDefault(c => c.ClientId == group.Key).Name,
                TotalSales = group.Sum(order => order.Orderdetails
                    .Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();

        return clientes;
    }
    
}