namespace Work_Bank_api.Dal.Transaction.Repositories
{
    //TODO ADD TO DI
    public interface ITransactionRepository : IGenericRepository<TransactionModel> 
    {

    }
    public class TransactionRepository(AppDbContext dbContex) : GenericRepository<TransactionModel, AppDbContext>(dbContex), ITransactionRepository
    {
    }
}
