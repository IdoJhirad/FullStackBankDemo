
using Work_Bank_api.Dal.Transaction.Repositories;

namespace Service.Services
{

    
    public interface ITransactionService : IGenericService<TransactionModel, DTOTransaction, DTORequestTransaction>
    {
        TransactionModel TransactionFromDToRequestTransaction(DTORequestTransaction dtoRequest);
        Task<DTOTransaction?> MarkTransactionAsDelete(object id);
    }
    public class TransactionService : GenericService<TransactionModel, DTOTransaction, DTORequestTransaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository) : base(mapper, transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<DTOTransaction?> MarkTransactionAsDelete(object id)
        {
            var transaction = await _repo.GetByIdAsync(id);
            if (transaction == null)
            {
                return null;
            }
            transaction.IsDeleted = true;

            await _transactionRepository.SaveChangesAsync(); 
            
           
            return _mapper.Map<DTOTransaction>(transaction);
        }

        public TransactionModel TransactionFromDToRequestTransaction(DTORequestTransaction dtoRequest)
        {
            return _mapper.Map<TransactionModel>(dtoRequest);
        }
    }
}
