using Work_Bank_Api.Service.Services.Utils;
namespace Work_Bank_api.Service.Services
{
    public interface IBankService
    {
        Task<DTOResponse<IEnumerable<DTOTransaction>>> GetTransactionsAsync(QueryObject query);
        Task<DTOResponse<DTOTransaction?>> GetTransactionByIdAsync(int id);
        Task<DTOResponse<DTOTransaction?>> AddDepositAsync(DTORequestTransaction model);
        Task<DTOResponse<DTOTransaction?>> AddWithdrawalAsync(DTORequestTransaction model);
        Task<DTOResponse<TransactionModel?>> DeleteTransactionAsync(int id);
    }

    public class BankService : IBankService
    {
        private readonly ILogger _logger;
        private readonly ITransactionService _transactionService;
        private readonly IHttpService _httpService;
        IConfiguration _configuration;
        public BankService(IConfiguration configuration, ILogger<BankService> logger , IHttpService httpService, ITransactionService transactionService)
        {
            _httpService = httpService;
            _logger = logger;   
            _transactionService = transactionService;
            _configuration = configuration;
        }
       
        public async Task<DTOResponse<IEnumerable<DTOTransaction>>> GetTransactionsAsync(QueryObject query)
        {
            var listOfDtos = new List<DTOTransaction>();
            var transactions = await _transactionService.GetAllAsync();
            var filterdData = FilterData(query, transactions);

            return new DTOResponse<IEnumerable<DTOTransaction>>
            {
                Status = "Sucsess",
                Code = 200,
                Data = filterdData
            };

        }

        public async Task<DTOResponse<DTOTransaction?>> GetTransactionByIdAsync(int id)
        {
            var response = new DTOResponse<DTOTransaction?>();
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
            {
                response.Status = "Not Found";
                response.Data = null;
                response.Code = 404;
            }
            else
            {
                response.Status = "Sucsess";
                response.Data = transaction;
                response.Code = 200;
            }
            return response;
        }

        public async Task<DTOResponse<DTOTransaction?>> AddDepositAsync(DTORequestTransaction model)
        {
            var response = new DTOResponse<DTOTransaction?>();
            
            var transactionModel = _transactionService.TransactionFromDToRequestTransaction(model);
            transactionModel.Type = TransactionType.Deposit;

            //Token for HttpService
            var tokenData = new CreateTokenData
            {
                SecretId = _configuration["HttpClient:SecreteId"]!,
                UserID = transactionModel.IdNumber,
            };

            var tokenResp = await _httpService.CreateToken(tokenData);
            if (tokenResp.Code != 201)
            {
                response.Code = 401;
                response.Status = "Unauthorized";
                response.Data = null;
                
                return response;
                //return Unauthorized();
            }

            var depositeResp = await _httpService.CreateDeposite(new TransferData
            {
                Amount = transactionModel.Amount,
                BankAccount = transactionModel.AccountNumber,
            });

            if (depositeResp.Code == 201)
            {
                transactionModel.Status = TransactionStatus.Completed;
            }
            else
            {
                transactionModel.Status = TransactionStatus.Failed;
            }
            var transactionDto = await _transactionService.AddAsync(transactionModel);

            response.Status = "sucsess";
            response.Code = 201;
            response.Data = transactionDto;
            
            return response;
        }

        public async Task<DTOResponse<DTOTransaction?>> AddWithdrawalAsync(DTORequestTransaction model)
        {
            var response = new DTOResponse<DTOTransaction?>();

            var transactionModel = _transactionService.TransactionFromDToRequestTransaction(model);
            transactionModel.Type = TransactionType.Withdrawal;

            //Token for HttpService
            var tokenData = new CreateTokenData
            {
                SecretId = _configuration["HttpClient:SecreteId"]!,
                UserID = transactionModel.IdNumber,
            };

            var tokenResp = await _httpService.CreateToken(tokenData);
            if (tokenResp.Code != 201)
            {
                response.Code = 401;
                response.Status = "Unauthorized";
                response.Data = null;

                return response;
                //return Unauthorized();
            }

            var withdrwaleResp = await _httpService.CreateWithdrawal(new TransferData
            {
                Amount = transactionModel.Amount,
                BankAccount = transactionModel.AccountNumber,
            });

            if (withdrwaleResp.Code == 201)
            {
                transactionModel.Status = TransactionStatus.Completed;
            }
            else
            {
                transactionModel.Status = TransactionStatus.Failed;
            }

            var transactionDto = await _transactionService.AddAsync(transactionModel);

            response.Status = "sucsess";
            response.Code = 201;
            response.Data = transactionDto;

            return response;
        }

        public async Task<DTOResponse<TransactionModel?>> DeleteTransactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        private List<DTOTransaction> FilterData(QueryObject query, IEnumerable<DTOTransaction> transactionsIEnumrable)
        {
            var transactions = transactionsIEnumrable.AsQueryable();

            transactions = transactions.WhereIf(query.IsDeleted.HasValue, t => t.IsDeleted == query.IsDeleted!.Value);

            transactions = transactions.WhereIf(query.IsEdited.HasValue, t => t.IsEdited == query.IsEdited!.Value);
            //dates
            transactions = transactions.WhereIf(query.FromDate.HasValue, e => e.Date >= query.FromDate!.Value)
                .WhereIf(query.ToDate.HasValue, e => e.Date <= query.ToDate!.Value.AddDays(1));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                Console.WriteLine($"u are here{query.IsDecsending}");
                if (query.SortBy.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    transactions = query.IsDecsending ? transactions.OrderByDescending(e => e.Amount) : transactions.OrderBy(e => e.Amount);
                }
                else if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    transactions = query.IsDecsending ? transactions.OrderByDescending(e => e.Date) : transactions.OrderBy(e => e.Date);
                }
            }

            transactions = transactions.WhereIf(query.Type.HasValue, t => t.Type == query.Type);

            var skipNum = (query.PageNumber - 1) * query.PageSize;

            return  transactions.Skip(skipNum).Take(query.PageSize).ToList();

        }
    }
}
