using ECommerce.Models;

namespace ECommerce.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}