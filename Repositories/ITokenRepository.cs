using Microsoft.AspNetCore.Identity;

namespace WalksProjectAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
