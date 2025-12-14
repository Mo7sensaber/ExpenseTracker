using Shared.AuthenticationDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbestraction
{
    public interface IAuthenticationsService
    {
        Task<ReturnUserDto> Login(LoginDto login);
        Task<ReturnUserDto> Register(RegisterDto register);
        Task<bool> CheckEmailAsync(string email);
        Task<ReturnUserDto> GetCurrentUserAsync(string email);  
    }
}
