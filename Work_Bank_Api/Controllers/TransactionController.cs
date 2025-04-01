using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Work_Bank_Api.Dtos;
using Work_Bank_Api.Intarfaces;
using Work_Bank_Api.Models;
using Work_Bank_Api.Utils;

namespace Work_Bank_Api.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IConfiguration _configuration;
        private readonly IHttpService _httpService;

        public TransactionController(ITransactionRepo tansactionRepo, IConfiguration configuration, IHttpService httpService)
        {
            _transactionRepo = tansactionRepo;
            _configuration = configuration;
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction([FromQuery]QueryObject query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactions = await _transactionRepo.GetTransactionAsync(query);
            var transactionDtos = transactions.Select(t => t.FromModelToDto());

            return Ok(transactionDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTransactionById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transaction = await _transactionRepo.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound("transaction not found");
            }
            return Ok(transaction.FromModelToDto());
        }

        [HttpPost("deposite")]
        public async Task<IActionResult> Deposite([FromBody]CreateTtansactionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transaction = dto.FromDtoToModel();
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
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction.FromModelToDto());
        }

        [HttpPost("withdrawal")]
        public async Task<IActionResult> Withdrawal([FromBody] CreateTtansactionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = dto.FromDtoToModel();
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
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction.FromModelToDto());
        }

        //[HttpPut("{id:int}")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _transactionRepo.DeleteTransactionAsync(id);
            return NoContent();
        }


    }
}
