using Lab08_DiegoBejarano.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab08_DiegoBejarano.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsWithHigherPriceThan([FromQuery] int value)
    {
        var products = await _unitOfWork.Products.GetProductsWithPriceHigherThan(value);
        return Ok(products);
    }

    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetProductsByOrderId([FromRoute] int id)
    {
        var products = await _unitOfWork.OrderDetails.GetProducstByOrderId(id);
        return Ok(products);
    }

    [HttpGet("order/{id}/sum")]
    public async Task<IActionResult> GetTotalProductsByOrderId([FromRoute] int id)
    {
        var total = await _unitOfWork.OrderDetails.GetTotalProductQuantityByOrderIdAsync(id);
        return Ok(total);
    }

    [HttpGet("expensive")]
    public async Task<IActionResult> GetMostExpensiveProduct()
    {
        var product = await _unitOfWork.Products.GetMostExpensiveProduct();
        return Ok(product);
    }

    [HttpGet("average-price")]
    public async Task<IActionResult> GetAverageProductPrice()
    {
        var averagePrice = await _unitOfWork.Products.GetAverageProductPriceAsync();
        return Ok(new { AveragePrice = averagePrice });
    }
    
    [HttpGet("no-description")]
    public async Task<IActionResult> GetProductsWithoutDescription()
    {
        var productsWithoutDescription = await _unitOfWork.Products.GetProductsWithoutDescriptionAsync();
        return Ok(productsWithoutDescription);
    }
    
    [HttpGet("{productId}/clients")]
    public async Task<IActionResult> GetClientsByProductId([FromRoute] int productId)
    {
        var clients = await _unitOfWork.OrderDetails.GetClientsByProductIdAsync(productId);
    
        if (clients == null || !clients.Any())
        {
            return NotFound(new { message = "No se encontraron clientes para ese producto." });
        }

        return Ok(clients);
    }

}