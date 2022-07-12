using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using MakeMyPizza.Data.IRepository;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    internal readonly IPizzaOrderManagementDbContext context;
    internal virtual DbSet<TEntity> dbSet { get; set; }
    private readonly ILogger<BaseRepository<TEntity>> _logger;

    public BaseRepository(IPizzaOrderManagementDbContext context,
                          ILogger<BaseRepository<TEntity>> logger)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
        _logger = logger;
    }

    public virtual List<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        _logger.LogInformation("Get all records started in BaseRepository");
        IQueryable<TEntity> query = dbSet;

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
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual TEntity GetByID(object id)
    {
        _logger.LogInformation("Get record by id started in BaseRepository");
        return dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        _logger.LogInformation("Add new record started in BaseRepository");
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        _logger.LogInformation("Delete record by id started in BaseRepository");
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        _logger.LogInformation("Delete record started in BaseRepository");
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _logger.LogInformation("Update record started in BaseRepository");
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}