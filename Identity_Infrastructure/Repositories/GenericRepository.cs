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
        try
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
        catch (Exception ex)
        {
            throw new Exception("Error occured during fetch from the database", ex);
        }
    }

    public virtual async Task<int> InsertAsync(T entity)
    {
        try
        {
            var newEntity = await _dbSet.AddAsync(entity);
            await SaveChangesAsync();

            return newEntity.Entity.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during insert in the database", ex);
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        try
        {
            T entityToDelete = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            await DeleteAsync(entityToDelete);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during delete from the database", ex);
        }
    }

    public virtual async Task DeleteAsync(T entityToDelete)
    {
        try
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during delete from the database", ex);
        }
    }

    public virtual async Task UpdateAsync(T entityToUpdate)
    {
        try
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during update in the database", ex);
        }
    }

    private async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
    }
        catch (Exception ex)
        {
            throw new Exception("Error occured during saving the database", ex);
        }
    }
}