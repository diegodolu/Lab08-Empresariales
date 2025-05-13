using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;

namespace Lab08_DiegoBejarano.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAfterDateAsync(DateTime date);
    Task<ClientDto> GetClientWithMostOrdersAsync();
    Task<List<OrderDetailsDto>> GetOrdersWithDetails();
}