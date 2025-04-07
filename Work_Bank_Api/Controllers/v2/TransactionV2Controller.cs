


namespace Work_Bank_Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/transaction")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TransactionV2Controller : ControllerBase
    {

        private readonly IBankService _bankService;
  

        public TransactionV2Controller(IBankService bankService, IConfiguration configuration, IHttpService httpService, IMapper mapper)
        {
            _bankService = bankService;
       
        }

        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <param name="query">the query params</param>
        /// <returns></returns>
        /// <response code="200">sucsess </response>
        /// <response code="400">Bad request </response>
        [HttpGet]
        [ProducesResponseType(typeof(DTOResponse<DTOTransaction>), 200)]
        [ProducesResponseType(typeof(DTOResponse<>), 400)]
        public async Task<IActionResult> GetTransaction([FromQuery] QueryObject query)
        {
            var response = await _bankService.GetTransactionsAsync(query);
            return StatusCode(response.Code, response);
        }

        /// <summary>
        ///  Get Transaction by id
        /// </summary>
        /// <param name="id">id of transaction</param>
        /// <returns> transaction Dto </returns>
        /// <response code="200">sucsess </response>
        /// <response code="404"> Not Found </response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DTOResponse<DTOTransaction>), 200)]
        [ProducesResponseType(typeof(DTOResponse<>),404)]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {

            var response = await _bankService.GetTransactionByIdAsync(id);
            return StatusCode(response.Code, response);

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
        [ProducesResponseType(typeof(DTOResponse<DTOTransaction>), 201)]
        [ProducesResponseType(typeof(DTOResponse<>), 400)]
        [ProducesResponseType(typeof(DTOResponse<>) ,401)]
        [ProducesResponseType(typeof(DTOResponse<>), 500)]
        public async Task<IActionResult> Deposite([FromBody] DTORequestTransaction dto)
        {
            var response = await _bankService.AddDepositAsync(dto);
            return StatusCode(response.Code, response);

            // return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, _mapper.Map<TransactionDto>(transaction));

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
        [ProducesResponseType(typeof(DTOResponse<DTOTransaction>), 201)]
        [ProducesResponseType(typeof(DTOResponse<>), 400)]
        [ProducesResponseType(typeof(DTOResponse<>), 401)]
        [ProducesResponseType(typeof(DTOResponse<>), 500)]
        public async Task<IActionResult> Withdrawal([FromBody] DTORequestTransaction dto)
        {
            var response = await _bankService.AddWithdrawalAsync(dto);
            return StatusCode(response.Code, response);

        }

        //[HttpPut("{id:int}")]

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id"> the transaction id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {

            //var transaction = await _transactionRepo.DeleteTransactionAsync(id);
            //if (transaction == null)
            //{
            //    return NotFound("Transaction not found.");
            //}
            return NoContent();
        }
    }
}
