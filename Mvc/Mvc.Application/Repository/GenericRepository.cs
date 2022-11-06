using Microsoft.EntityFrameworkCore;
using Mvc.Application.Data;

namespace Mvc.Application.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ProductDbContext _context;

    public GenericRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task Create(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        _context.SaveChanges();
    }
}
