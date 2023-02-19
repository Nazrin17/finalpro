

using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, new()
        where TContext : IdentityDbContext<AppUser>

{
    private readonly TContext _context;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (includes != null)
        {
            foreach (var item in includes)
            {

                query = query.Include(item);
            }
        }
        return exp == null ? await query.FirstOrDefaultAsync() : await query.Where(exp).FirstOrDefaultAsync();
    }

    public async Task<TEntity> Get(params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (includes != null)
        {
            foreach (var item in includes)
            {

                query = query.Include(item);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllAsnyc(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (includes != null)
        {
            foreach (var item in includes)
            {

                query = query.Include(item);
            }
        }
        return exp == null ? await query.ToListAsync() : await query.Where(exp).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsnyc(params string[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (includes != null)
        {
            foreach (var item in includes)
            {

                query = query.Include(item);
            }
        }
        return await query.ToListAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }
}

