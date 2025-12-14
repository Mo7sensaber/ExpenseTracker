using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ReturnExpenceDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } 
        public string Description { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}
