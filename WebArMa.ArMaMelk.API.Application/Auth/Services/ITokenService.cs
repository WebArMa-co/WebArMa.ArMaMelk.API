using WebArMa.ArMaMelk.API.Application.Auth.DTOs;

namespace WebArMa.ArMaMelk.API.Application.Auth.Services
{
    public interface ITokenService
    {
        Task<TokenDTO> GenerateAsync(int userId, Guid roleId, string accessCode, CancellationToken cancellationToken);
    }
}
