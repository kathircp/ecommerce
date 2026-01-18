using AutoMapper;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IUserRepository _userRepository;
        public UserDetailService(IMapper mapper, IUserDetailRepository userDetailRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userDetailRepository = userDetailRepository;
            _userRepository = userRepository;
        }

        public bool Create(UserDetailDto userDetailDto)
        {
            var user = _userRepository.GetByUsername(userDetailDto.UserName);
            if (user == null) return false;
            UserDetail userDetail = _mapper.Map<UserDetailDto, Models.UserDetail>(userDetailDto);
            userDetail.UserId = user.Id;
            userDetail.IsPrimary = true;
            bool isValid = _userDetailRepository.Create(userDetail);   
            if (isValid)
            {                
                _userDetailRepository.UpdatePrimary(userDetail.Id, user.Id);
            }
            return isValid;
        }

        public UserDetailDto? Get(string userName)
        {
            var user = _userRepository.GetByUsername(userName);
            if (user == null) return null;
            var userDetail = _userDetailRepository.GetByUserId(user.Id);
            if (userDetail == null) return null;
            var dtoResponse = _mapper.Map<UserDetailDto>(userDetail);
            dtoResponse.UserName = userName;
            return dtoResponse;
           
        }

        public List<UserDetailDto> GetAll(string userName)
        {
            var user = _userRepository.GetByUsername(userName);
            if (user == null) return null;
            var userDetails = _userDetailRepository.GetAll(user.Id);
            var dtoResponse = _mapper.Map<List<UserDetailDto>>(userDetails);
            return dtoResponse;
        }
    }
}
