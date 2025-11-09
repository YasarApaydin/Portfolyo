using Portfolyo.Entities.Models;

namespace Portfolyo.Entities.Common
{
    public interface IJwtProvider
    {


        Task<string> CreateTokenAsync(AppUser appUser);
    }
}
