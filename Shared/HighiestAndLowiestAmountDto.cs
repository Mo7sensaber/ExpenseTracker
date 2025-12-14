using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class HighiestAndLowiestAmountDto
    {
        public decimal Total { get; set; }
        public string HighiestCategory { get; set; } = default!;
        public string LowiestCategory { get; set; } = default!;
    }
}
