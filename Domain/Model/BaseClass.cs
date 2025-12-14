using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class BaseClass<TKey>
    {
        public TKey Id { get; set; }
    }
}
