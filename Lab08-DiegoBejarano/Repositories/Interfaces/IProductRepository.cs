using Lab08_DiegoBejarano.Dto;

namespace Lab08_DiegoBejarano.Repositories;

public interface IProductRepository
{
    Task<List<ProductDto>> GetProductsWithPriceHigherThan(int valor);
    Task<ProductDto?> GetMostExpensiveProduct();
    Task<decimal> GetAverageProductPriceAsync();
    Task<List<ProductDto>> GetProductsWithoutDescriptionAsync();
}