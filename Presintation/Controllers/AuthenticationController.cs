using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbestraction;
using Shared.AuthenticationDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController:ControllerBase
    {
        private readonly IManagerService manager;

        public AuthenticationController(IManagerService manager)
        {
            this.manager = manager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ReturnUserDto>> RegisterUser(RegisterDto register)
        {
            var regist = await manager.authentication.Register(register);
            return Ok(regist);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ReturnUserDto>> LoginUser(LoginDto login)
        {
            var loginUser=await manager.authentication.Login(login);
            return Ok(loginUser);
        }
        [HttpGet("GetUser")]
        [Authorize]
        public async Task<ActionResult<ReturnUserDto>> GetUser()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await manager.authentication.GetCurrentUserAsync(email!);
            return Ok(user);
        }
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var exists = await manager.authentication.CheckEmailAsync(email);
            return Ok(exists);
        }


    }
}
