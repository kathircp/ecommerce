using ECommerce.DTOs;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailService _userDetailService;

        public UserDetailController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(string userName)
        {
            var userDetails = _userDetailService.GetAll(userName);            
            return Ok(userDetails);
        }

        [HttpGet]
        public IActionResult Get(string userName)
        {
            var ud = _userDetailService.Get(userName);
            if (ud == null) return NotFound();           
            return Ok(ud);
        }      

        [HttpPost]
        public IActionResult Create([FromBody] UserDetailDto userDetail)
        {
            return Ok(_userDetailService.Create(userDetail));            
        }
    }
}
