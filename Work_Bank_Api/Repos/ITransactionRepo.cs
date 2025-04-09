namespace Work_Bank_Api.Webapi.Repos
{
    public interface ITransactionRepo
    {
        Task<List<TransactionModel>> GetTransactionAsync(QueryObject query);
        Task<TransactionModel?> GetTransactionByIdAsync(int id);
        Task<TransactionModel> AddDepositAsync(TransactionModel model);
        Task<TransactionModel> AddWithdrawalAsync(TransactionModel model);
        Task<TransactionModel?> DeleteTransactionAsync(int id);

        //Task<TransactionModel?> EditTransaction(int id);


    }
}
