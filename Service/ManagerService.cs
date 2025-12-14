using ServiceAbestraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ManagerService(Func<ICategoryService> funcCategory, Func<IExpenseService> funcExpence
        ,Func<IReportService> funcReport,Func<IAuthenticationsService> funcAuth) : IManagerService
    {
        public ICategoryService category => funcCategory.Invoke();

        public IExpenseService expence => funcExpence.Invoke();

        public IReportService report => funcReport.Invoke();

        public IAuthenticationsService authentication => funcAuth.Invoke();
    }
}
