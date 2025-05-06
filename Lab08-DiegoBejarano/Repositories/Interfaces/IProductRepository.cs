using Lab08_DiegoBejarano.Dto;

namespace Lab08_DiegoBejarano.Repositories;

public interface IProductRepository
{
    Task<List<ProductDto>> GetProductsWithPriceHigherThan(int valor);
}