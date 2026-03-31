using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly AdventureWorksContext _context;
        public DataBaseService(AdventureWorksContext context)
        {
            _context = context;
        }
        public async Task<int> GetPendingMigrationsCountAsync()
        {
            var pending = await _context.Database.GetPendingMigrationsAsync();
            return pending.Count();
        }

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
