namespace Dal.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);


        IEnumerable<TEntity> GetAll();
        TEntity? GetById(object id);
        TEntity Add(TEntity entity);
        void Delete(object id);


    }
    public class GenericRepository<TEntity, DbContexRepository> : IGenericRepository< TEntity > where DbContexRepository : DbContext where TEntity : class
    {
        private readonly DbContexRepository _dbContext;
        public GenericRepository(DbContexRepository dbContex)
        {
            _dbContext = dbContex;
        }
 
        //async
      

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task DeleteAsync(object id)
        {
            // null checks ?!?

            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return;
            }
            _dbContext.Set<TEntity>().Remove(entity);
             await _dbContext.SaveChangesAsync();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
        public TEntity? GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);    
        }

        public  TEntity Add(TEntity entity)
        { 
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
             
        }
 
        public void Delete(object id)
        {
           var entity = GetById(id);
            if (entity == null)
            {
                return;
            }
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
           return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
    }
}
