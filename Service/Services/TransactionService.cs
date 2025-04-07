
using Work_Bank_api.Dal.Transaction.Repositories;

namespace Service.Services
{

    //TODO ADD TO DI
    public interface ITransactionService : IGenericService<TransactionModel, DTOTransaction, DTORequestTransaction>
    {
        TransactionModel TransactionFromDToRequestTransaction(DTORequestTransaction dtoRequest);
    }
    public class TransactionService : GenericService<TransactionModel, DTOTransaction, DTORequestTransaction>, ITransactionService
    {
        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository) : base(mapper, transactionRepository)
        {
        }
        public TransactionModel TransactionFromDToRequestTransaction(DTORequestTransaction dtoRequest)
        {
            return _mapper.Map<TransactionModel>(dtoRequest);
        }
    }
}
