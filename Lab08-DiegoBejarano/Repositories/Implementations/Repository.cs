using Lab08_DiegoBejarano.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08_DiegoBejarano.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

    public async Task<T?> GetById(int id) => await _context.Set<T>().FindAsync(id);

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task AutoAdd(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(int id, T entity)
    {
        var entityFound = await _context.Set<T>().FindAsync(id);
    
        if (entityFound == null)
        {
            throw new Exception("Entidad no encontrada.");
        }

        _context.Entry(entityFound).CurrentValues.SetValues(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}