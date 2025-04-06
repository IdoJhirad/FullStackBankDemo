using Asp.Versioning;

namespace Work_Bank_Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/transaction")]
    [ApiVersion("2.0")]
    public class TransactionV2Controller : ControllerBase
    {

        [HttpGet]
        public IActionResult GetV2()
        {
            return Ok(new { message = "This is version 2.0" });
        }
    }
}
