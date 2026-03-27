using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;
using Sales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sales.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AdventureWorksContext _context;

        public CustomerRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var query = from c in _context.Customers.Include(c => c.Orders)
                        where c.PersonId != null

                        join p in _context.Set<Person>()
                        on c.PersonId equals p.BusinessEntityId

                        join e in _context.Set<Email>()
                        on p.BusinessEntityId equals e.BusinessEntityId into emailJoin
                        from email in emailJoin.DefaultIfEmpty()

                        select new Customer
                        {
                            CustomerId = c.CustomerId,
                            PersonId = c.PersonId,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            EmailAddress = email != null ? email.EmailAddress : "No Email",
                            Orders = c.Orders
                        };

            return await query
                .AsNoTracking()
                .Take(1000) // testing, to change
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        
        }

    }
}
