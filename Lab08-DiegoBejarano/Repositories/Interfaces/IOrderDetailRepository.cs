using Lab08_DiegoBejarano.Dto;

namespace Lab08_DiegoBejarano.Repositories;

public interface IOrderDetailRepository
{
    Task<List<ProductDto>> GetProducstByOrderId(int id);
    Task<int> GetTotalProductQuantityByOrderIdAsync(int id);
    Task<List<OrderProductDto>> GetAllOrderDetailsAsync();
    Task<List<string>> GetProductsSoldToClientAsync(int clientId);
    Task<List<string>> GetClientsByProductIdAsync(int id);
}