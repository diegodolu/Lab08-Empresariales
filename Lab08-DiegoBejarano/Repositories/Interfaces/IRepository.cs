namespace Lab08_DiegoBejarano.Repositories;

public interface IRepository<T> where T:class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task Add(T entity);
    Task AutoAdd(T entity);
    Task Update(int id, T entity);
    Task Delete(int id);
}