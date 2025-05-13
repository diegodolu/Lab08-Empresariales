using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;

namespace Lab08_DiegoBejarano.Repositories;

public interface IClienteRepository
{
    Task<List<ClientDto>> NamedBy(string name);
    Task<List<ClientOrderDto>> GetClientsWithOrders();
    Task<List<ClientProductsDto>> GetClientWithTotalPurchasedProducts();
    Task<List<SalesClientDto>> GetClientWithTotalSales();
}