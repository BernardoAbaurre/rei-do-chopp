using ReiDoChopp.Domain.Users.Entities;

namespace ReiDoChopp.Domain.Users.Services.Interfaces
{
    public interface ITokensService
    {
        Task<string> GenerateToken(User user);
    }
}
