using Lab08_DiegoBejarano.Models;

namespace Lab08_DiegoBejarano.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<string, object> _repositories;
    
    public IClienteRepository Clientes { get; }
    public IProductRepository Products { get; }

    public UnitOfWork(ApplicationDbContext context, IClienteRepository clienteRepository, IProductRepository productRepository)
    {
        _context = context;
        _repositories = new Dictionary<string, object>();
        Clientes = clienteRepository;
        Products = productRepository;
    }
    
    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }
    
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repoInstance = new Repository<TEntity>(_context);
            _repositories[type] = repoInstance;
        }
    
        return (IRepository<TEntity>)_repositories[type];
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    
    
}