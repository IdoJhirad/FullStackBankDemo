using Microsoft.EntityFrameworkCore;
using Work_Bank_Api.Db;
using Work_Bank_Api.Intarfaces;
using Work_Bank_Api.Models;
using Work_Bank_Api.Utils;

namespace Work_Bank_Api.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly AppDbContext _appDbContext;


        public TransactionRepo(AppDbContext dbContext) 
        {
            _appDbContext = dbContext;
        }
        public async Task<List<TransactionModel>> GetTransactionAsync(QueryObject query)
        {
            var transaction = _appDbContext.Transactions.AsQueryable();

            //filter the transactions
            transaction = transaction.WhereIf(query.IsDeleted.HasValue, t => t.IsDeleted == query.IsDeleted!.Value);

            transaction = transaction.WhereIf(query.IsEdited.HasValue, t => t.IsEdited == query.IsEdited!.Value);
            //dates
            transaction = transaction.WhereIf(query.FromDate.HasValue, e => e.Date >= query.FromDate!.Value)
                .WhereIf(query.ToDate.HasValue, e => e.Date <= query.ToDate!.Value.AddDays(1));

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                Console.WriteLine($"u are here{query.IsDecsending}");
                if (query.SortBy.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    transaction = query.IsDecsending ? transaction.OrderByDescending(e => e.Amount) : transaction.OrderBy(e => e.Amount);
                }
                else if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    transaction = query.IsDecsending ? transaction.OrderByDescending(e => e.Date) : transaction.OrderBy(e => e.Date);
                }
            }

            transaction = transaction.WhereIf(query.Type.HasValue, t => t.Type == query.Type);
          
            var skipNum = ( query.PageNumber -1 ) * query.PageSize;

            return await transaction.Skip(skipNum).Take(query.PageSize).ToListAsync();

        }

        public async Task<TransactionModel?> GetTransactionByIdAsync(int id)
        {
            return await _appDbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<TransactionModel> AddDepositAsync(TransactionModel model)
        {
            await _appDbContext.Transactions.AddAsync(model);
            await _appDbContext.SaveChangesAsync();

            return model;
        }

        public async Task<TransactionModel> AddWithdrawalAsync(TransactionModel model)
        {
            await _appDbContext.Transactions.AddAsync(model);
            await _appDbContext.SaveChangesAsync();

            return model;
        }

        public async Task<TransactionModel?> DeleteTransactionAsync(int id)
        {
            var transaction = await _appDbContext.Transactions.FirstOrDefaultAsync(t => t.Id ==id);
            if (transaction == null)
            {
                return null;
            }
            transaction.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
            return transaction;
        }
    }
}
