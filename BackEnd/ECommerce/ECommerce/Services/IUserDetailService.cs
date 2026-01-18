using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IUserDetailService
    {
        List<UserDetailDto> GetAll(string userName);
        UserDetailDto? Get(string userName);
        //UserDetailDto? Get(int id);
        bool Create(UserDetailDto userDetailDto);
    }
}
