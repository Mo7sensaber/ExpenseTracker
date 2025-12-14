using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Expense: BaseClass<int>
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
        public string Description { get; set; } = default!;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
