using Lab08_DiegoBejarano.Models;
using Lab08_DiegoBejarano.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab08_DiegoBejarano.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetClienteByFirstName([FromQuery] string name)
    {
        var clients = await _unitOfWork.Clientes.NamedBy(name);
        return Ok(clients);
    }
    
    [HttpGet("most-orders")]
    public async Task<IActionResult> GetClientWithMostOrders()
    {
        var clientWithMostOrders = await _unitOfWork.Orders.GetClientWithMostOrdersAsync();
    
        if (clientWithMostOrders == null)
        {
            return NotFound(new { message = "No se encontró el cliente con más pedidos." });
        }

        return Ok(clientWithMostOrders);
    }

}