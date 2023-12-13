using System.Linq.Expressions;

namespace Identity_Application.Interfaces.Repository;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAsync(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "");

    Task<T> GetByIDAsync(int id);

    Task InsertAsync(T entity);

    Task Update(T entityToUpdate);

    void Delete(T entityToDelete);
}