using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Domain.Service;
public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    private readonly IBaseRepository<TEntity> _repo;
    private readonly ILogger<BaseService<TEntity>> _logger;
    public BaseService(IBaseRepository<TEntity> repo,
                       ILogger<BaseService<TEntity>> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public List<TEntity> Get()
    {
        _logger.LogInformation("Get all records started in BaseService");
        return _repo.Get();
    }

    public List<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = ""
    )
    {
        return _repo.Get(filter, orderBy, includeProperties);
    }

    public TEntity Get(int id)
    {
        return _repo.GetByID(id);
    }

    public TEntity Insert(TEntity entity)
    {
        _repo.Insert(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _repo.Update(entity);
        return entity;
    }

    public TEntity Delete(TEntity entity)
    {
        _repo.Delete(entity);
        return entity;
    }
}