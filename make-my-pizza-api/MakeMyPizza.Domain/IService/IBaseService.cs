using System.Linq.Expressions;

namespace MakeMyPizza.Domain.IService;
public interface IBaseService<TEntity>
{
    List<TEntity> Get();
    TEntity Get(int id);
    List<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = ""
    );
    TEntity Insert(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
}