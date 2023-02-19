using System.Linq.Expressions;

    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsnyc(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<List<TEntity>> GetAllAsnyc(params string[] includes);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<TEntity> Get(params string[] includes);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

