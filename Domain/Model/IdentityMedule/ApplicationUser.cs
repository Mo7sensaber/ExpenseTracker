using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.IdentityMedule
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = default!;
    }
}
