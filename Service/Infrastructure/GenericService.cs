namespace Service.Infrastructure
{
    public interface IGenericService <TEntity, DTOEntity, CreateDTOEntity> where TEntity :class where DTOEntity :class where CreateDTOEntity :class
    {
        Task<IEnumerable<DTOEntity>> GetAllAsync();
        Task<DTOEntity?> GetByIdAsync(object id);
        Task<DTOEntity> AddAsync(TEntity model);
        Task<IEnumerable<DTOEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(object id);

    }
    public class GenericService <TEntity, DTOEntity, CreateDTOEntity>(IMapper mapper, IGenericRepository<TEntity> genericRepository) : IGenericService<TEntity, DTOEntity, CreateDTOEntity> where TEntity : class where DTOEntity : class where CreateDTOEntity : class
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly IGenericRepository<TEntity> _repo = genericRepository;

        public async Task<DTOEntity> AddAsync(TEntity model)
        {  
            await _repo.AddAsync(model);
            return _mapper.Map<DTOEntity>(model);

        }

        public async Task DeleteAsync(object id)
        {
             await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<DTOEntity>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DTOEntity>>(entities);
        }

        public async Task<DTOEntity?> GetByIdAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<DTOEntity>(entity);
        }

        public async Task<IEnumerable<DTOEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repo.GetWhere(predicate);
            return _mapper.Map<IEnumerable<DTOEntity>>(entities);
        }
    }
}
