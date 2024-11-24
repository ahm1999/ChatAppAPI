using ChatApp.Data.Entities;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var Users = await _userService.GetUsers();

            return Ok(Users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user) {
             await _userService.AddUser(user);

            return Ok("User Created");
        }

        [HttpPost("{userName}")]
        public async Task<IActionResult> TryAddUser(string userName) {
            var user  = await _userService.AddOrReturnUserByName(userName);
            return Ok(user);
        }

    }
}
