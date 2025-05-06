using Lab08_DiegoBejarano.Dto;
using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<ClientDto>> NamedBy(string name)
    {
        var list = await _context.Clients
            .Where(c => c.Name.StartsWith(name))
            .Select(c => new ClientDto
            {
                Name = c.Name,
            })
            .ToListAsync();

        return list;
    }
}