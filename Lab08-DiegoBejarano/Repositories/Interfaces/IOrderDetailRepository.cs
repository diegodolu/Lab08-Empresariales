using Lab08_DiegoBejarano.Dto;

namespace Lab08_DiegoBejarano.Repositories;

public interface IOrderDetailRepository
{
    Task<List<ProductDto>> GetProducstByOrderId(int id);
}