namespace Lab08_DiegoBejarano.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete();
    IClienteRepository Clientes { get; }
    IProductRepository Products { get; }
    IOrderDetailRepository OrderDetails { get; }
    IOrderRepository Orders { get; }

}