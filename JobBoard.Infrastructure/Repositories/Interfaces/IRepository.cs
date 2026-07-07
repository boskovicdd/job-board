using System.Linq.Expressions;

namespace JobBoard.Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(params string[] includes);
    T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params string[] includes);
}
