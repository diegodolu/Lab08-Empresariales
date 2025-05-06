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
}