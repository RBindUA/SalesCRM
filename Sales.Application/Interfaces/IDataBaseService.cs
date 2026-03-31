using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface IDataBaseService
    {

        //migration testing
        Task<int> GetPendingMigrationsCountAsync();
        Task MigrateAsync();

    }
}
