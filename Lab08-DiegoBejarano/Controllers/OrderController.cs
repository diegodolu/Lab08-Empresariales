using Lab08_DiegoBejarano.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab08_DiegoBejarano.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("after")]
    public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime date)
    {
        var orders = await _unitOfWork.Orders.GetOrdersAfterDateAsync(date);
        return Ok(orders);
    }
    
    [HttpGet("details")]
    public async Task<IActionResult> GetOrderDetails()
    {
        var orderDetails = await _unitOfWork.OrderDetails.GetAllOrderDetailsAsync();
    
        if (orderDetails == null || orderDetails.Count == 0)
        {
            return NotFound(new { message = "No se encontraron detalles de órdenes." });
        }

        return Ok(orderDetails);
    }
    
    [HttpGet("client/{clientId}/products")]
    public async Task<IActionResult> GetProductsSoldToClient([FromRoute] int clientId)
    {
        // Llamamos al servicio para obtener los productos vendidos al cliente
        var products = await _unitOfWork.OrderDetails.GetProductsSoldToClientAsync(clientId);
        
        if (products == null || !products.Any())
        {
            return NotFound(new { message = "No se encontraron productos vendidos a este cliente." });
        }

        // Retornamos la lista de productos
        return Ok(products);
    }
}