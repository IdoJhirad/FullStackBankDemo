namespace Work_Bank_api.Dal.Transaction.Repositories
{
    //TODO ADD TO DI
    public interface ITransactionRepository : IGenericRepository<TransactionModel> 
    {
        Task SaveChangesAsync();
    }
    public class TransactionRepository(AppDbContext dbContex) : GenericRepository<TransactionModel, AppDbContext>(dbContex), ITransactionRepository
    {
        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
