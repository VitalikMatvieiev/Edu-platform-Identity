using Identity_Domain.Entities.Base;
using System.Linq.Expressions;

namespace Identity_Application.Interfaces.Repository;

public interface IGenericRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAsync(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "");

    Task<int> InsertAsync(T entity);

    Task UpdateAsync(T entityToUpdate);

    Task DeleteAsync(int id);

    Task DeleteAsync(T entityToDelete);
}