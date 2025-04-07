



namespace Work_Bank_Api.Controllers.v1
{

    [Route("api/v{version:apiVersion}/transaction")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IConfiguration _configuration;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepo tansactionRepo, IConfiguration configuration, IHttpService httpService, IMapper mapper)
        {
            _transactionRepo = tansactionRepo;
            _configuration = configuration;
            _httpService = httpService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <param name="query">the query params</param>
        /// <returns></returns>
        /// <response code="200">sucsess </response>
        /// <response code="400">Bad request </response>
        [HttpGet]
        [ProducesResponseType(typeof(DTOTransaction), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransaction([FromQuery] QueryObject query)
        {

            var transactions = await _transactionRepo.GetTransactionAsync(query);
            //mapper
            var transactionDtos = _mapper.Map<List<DTOTransaction>>(transactions);

            return Ok(transactionDtos);
        }

        /// <summary>
        ///  Get Transaction by id
        /// </summary>
        /// <param name="id">id of transaction</param>
        /// <returns> transaction Dto </returns>
        /// <response code="200">sucsess </response>
        /// <response code="400">Bad request </response>
        /// <response code="404"> Not Found </response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DTOTransaction), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {

            var transaction = await _transactionRepo.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound("transaction not found");
            }
            //return Ok(transaction.FromModelToDto());
            return Ok(_mapper.Map<DTOTransaction>(transaction));
        }


        /// <summary>
        /// post transaction
        /// </summary>
        /// <param name="dto">the transaction params</param>
        /// <returns></returns>
        /// <response code="201">sucsess Created</response>
        /// <response code="400">Bad request </response>
        /// <response code="500"> server error </response>
        [HttpPost("deposite")]
        [ProducesResponseType(typeof(DTOTransaction), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Deposite([FromBody] DTORequestTransaction dto)
        {

            //var transaction = dto.FromDtoToModel();
            var transaction = _mapper.Map<TransactionModel>(dto);
            transaction.Type = TransactionType.Deposit;

            var tokenData = new CreateTokenData
            {
                SecretId = _configuration["HttpClient:SecreteId"]!,
                UserID = transaction.IdNumber,
            };

            //creatye token servise if sucseed or not 
            var tokenResp = await _httpService.CreateToken(tokenData);
            if (tokenResp.Code != 201)
            {
                return Unauthorized();
            }

            var depositeResp = await _httpService.CreateDeposite(new TransferData
            {
                Amount = transaction.Amount,
                BankAccount = transaction.AccountNumber,
            });

            if (depositeResp.Code == 201)
            {
                transaction.Status = TransactionStatus.Completed;
            }
            else
            {
                transaction.Status = TransactionStatus.Failed;
            }

            await _transactionRepo.AddDepositAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, _mapper.Map<DTOTransaction>(transaction));
        }

        /// <summary>
        /// post transaction
        /// </summary>
        /// <param name="dto">the transaction params</param>
        /// <returns></returns>
        /// <response code="201">sucsess Created</response>
        /// <response code="400">Bad request </response>
        /// <response code="500"> server error </response>
        [HttpPost("withdrawal")]
        [ProducesResponseType(typeof(DTOTransaction), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Withdrawal([FromBody] DTORequestTransaction dto)
        {

            var transaction = _mapper.Map<TransactionModel>(dto);
            transaction.Type = TransactionType.Withdrawal;
            var tokenData = new CreateTokenData
            {
                SecretId = _configuration["HttpClient:SecreteId"]!,
                UserID = transaction.IdNumber,
            };

            //creatye token servise if sucseed or not 
            var tokenResp = await _httpService.CreateToken(tokenData);
            if (tokenResp.Code != 201)
            {
                return Unauthorized();
            }

            var withdrwalResp = await _httpService.CreateWithdrawal(new TransferData
            {
                Amount = transaction.Amount,
                BankAccount = transaction.AccountNumber,
            });

            if (withdrwalResp.Code == 201)
            {
                transaction.Status = TransactionStatus.Completed;
            }
            else
            {
                transaction.Status = TransactionStatus.Failed;
            }

            await _transactionRepo.AddWithdrawalAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, _mapper.Map<DTOTransaction>(transaction));
        }

        //[HttpPut("{id:int}")]

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id"> the transaction id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DTOTransaction), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {

            var transaction = await _transactionRepo.DeleteTransactionAsync(id);
            if (transaction == null)
            {
                return NotFound("Transaction not found.");
            }
            return NoContent();
        }


    }
}
