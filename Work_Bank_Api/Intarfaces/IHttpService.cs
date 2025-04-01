using Work_Bank_Api.Utils;

namespace Work_Bank_Api.Intarfaces
{
    public interface IHttpService
    {
        Task<TokenResponse> CreateToken(CreateTokenData data);
        Task<TransferResponse> CreateDeposite(TransferData data);
        Task<TransferResponse> CreateWithdrawal(TransferData data);
    }
}
