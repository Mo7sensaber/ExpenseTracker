using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationDTOS
{
    public class RegisterDto
    {
        public string UserName { get; set; } =default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string phoneNumber { get; set; } = default!;
    }
}
