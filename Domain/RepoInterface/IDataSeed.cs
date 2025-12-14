using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepoInterface
{
    public interface IDataSeed
    {
        Task IdentityDataSeed();
    }
}
