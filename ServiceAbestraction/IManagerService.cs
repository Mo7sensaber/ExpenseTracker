using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbestraction
{
    public interface IManagerService 
    {
        ICategoryService category { get; }
        IExpenseService expence { get; }
        IReportService report { get; }
        IAuthenticationsService authentication { get; }
    }
}
