using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationDTOS
{
    public class ReturnUserDto
    {
        public string token { get; set; }= default!;
        public string DisplayName { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;

    }
}
