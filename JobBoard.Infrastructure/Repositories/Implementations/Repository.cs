using System.Linq.Expressions;
using JobBoard.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly JobBoardContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(JobBoardContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll(params string[] includes) => ApplyIncludes(includes).ToList();

    public T? GetById(int id) => DbSet.Find(id);

    public void Add(T entity) => DbSet.Add(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params string[] includes) =>
        ApplyIncludes(includes).Where(predicate).ToList();

    private IQueryable<T> ApplyIncludes(IEnumerable<string> includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
