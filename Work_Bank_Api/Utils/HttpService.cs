
using Work_Bank_Api.Intarfaces;
using Work_Bank_Api.Models;

namespace Work_Bank_Api.Utils
{
    public class CreateTokenData
    {
        public string UserID { get; set; } = string.Empty; 
        public string SecretId { get; set; } = string.Empty;
    }
    public class TokenResponse
    {
        public int Code { get; set; }
        public string? Token { get; set; }
    }

    public class TransferData
    {
        public string BankAccount { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
    public class TransferResponse
    {
        public int Code {  get; set; }
        public TransactionStatus Status { get; set; }
    }
    public class HttpService : IHttpService
    {
        private static readonly HttpClient _client = new HttpClient();

        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly ILogger<HttpClient> _logger;
        public HttpService(IConfiguration configuration, ILogger<HttpClient> logger)
        {
            _configuration = configuration;
            _baseUrl = _configuration["HttpClient:BaseUrl"]!;
            _logger = logger;
        }



        public async Task<TransferResponse> CreateDeposite(TransferData data)
        {
           
            try
            {
                string url = _baseUrl + _configuration["HttpClient:CreateDepositeUrl"];
                var resp = await _client.PostAsJsonAsync(url, data);
                
                if (resp.IsSuccessStatusCode)
                {
                    var depositeResp = await resp.Content.ReadFromJsonAsync<TransferResponse>();
                    return depositeResp;
                }
                else
                {
                    return new TransferResponse { Code = (int)resp.StatusCode, Status = TransactionStatus.Failed };
                }
            }
            catch (Exception e)
            // Simulate a response on fail
            {
                _logger.LogError($"deposite get {e} now move to random");
                bool isSucsseed = new Random().Next(0, 2) == 1;
                _logger.LogInformation($"{isSucsseed}");
                var resp = new TransferResponse
                {
                    Code = isSucsseed ? 201 : 500,

                    Status = isSucsseed ? TransactionStatus.Completed : TransactionStatus.Failed

                };
                return resp;
            }
        }


        public async Task<TokenResponse> CreateToken(CreateTokenData data)
        {
            try
            {
                string url = _baseUrl + _configuration["HttpClient:CreateTokenUrl"];
                var response = await _client.PatchAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    var respData = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    return respData;
                }
                else
                {
                    return new TokenResponse
                    {
                         Code = (int)response.StatusCode,
                        Token = null ,
                    };
                }
            } 
            catch(Exception)
            {
                bool isSucsseed = new Random().Next(0, 2) == 1;
                var resp = new TokenResponse
                {
                    Code = isSucsseed ? 201 : 500,
                    Token = isSucsseed ? "I am a Token" : null
                };
                return resp;
            }
        }


        public async Task<TransferResponse> CreateWithdrawal(TransferData data)
        {
            try
            {
                string url = _baseUrl + _configuration["HttpClient:CreateWithdrawalUrl"];
                var resp = await _client.PostAsJsonAsync(url, data);

                if (resp.IsSuccessStatusCode)
                {
                    var withdrawal = await resp.Content.ReadFromJsonAsync<TransferResponse>();
                    return withdrawal;
                }
                else
                {
                    return new TransferResponse { Code = (int)resp.StatusCode, Status = TransactionStatus.Failed };
                }
            }
            catch (Exception )
            // Simulate a response on fail
            {
                bool isSucsseed = new Random().Next(0, 2) == 1;
                var resp = new TransferResponse
                {
                    Code = isSucsseed ? 201 : 500,

                    Status = isSucsseed ? TransactionStatus.Completed : TransactionStatus.Failed

                };
                return resp;
            }
        }
    }
}
