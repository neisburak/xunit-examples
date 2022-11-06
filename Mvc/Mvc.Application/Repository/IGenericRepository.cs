namespace Mvc.Application.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(int id);
    Task Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
