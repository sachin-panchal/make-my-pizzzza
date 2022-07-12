using System.Linq.Expressions;

namespace MakeMyPizza.Data.IRepository;
public interface IBaseRepository<TEntity>
{
    List<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

    TEntity GetByID(object id);
    void Insert(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);

}