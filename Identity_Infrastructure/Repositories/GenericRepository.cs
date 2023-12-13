using Identity_Application.Interfaces.Repository;
using Identity_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity_Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    internal IdentityDbContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(IdentityDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAsync(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task<T> GetByIDAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task InsertAsync(T entity)
    {
        /*var asd =*/ await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public virtual async void Delete(int id)
    {
        T entityToDelete = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        Delete(entityToDelete);
    }

    public virtual async void Delete(T entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
            _dbSet.Attach(entityToDelete);

        _dbSet.Remove(entityToDelete);

        await SaveChangesAsync();
    }

    public virtual async Task Update(T entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;

        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}