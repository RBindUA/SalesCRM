using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sales.Application.DTO;
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailsAsync(CustomerOrderDetailDto dto)
        {
            var entity = await _context.SalesOrderDetails
                .FirstOrDefaultAsync(x => x.SalesOrderId == dto.SalesOrderId
                                       && x.SalesOrderDetailId == dto.SalesOrderDetailId);

            if (entity != null)
            {
                entity.OrderQty = dto.Quantity;
            }
        }

        public CustomerRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var query = from c in _context.Customers
                        join p in _context.Set<Person>() on c.PersonId equals p.BusinessEntityId
                        join e in _context.Set<Email>() on p.BusinessEntityId equals e.BusinessEntityId into emailJoin
                        from email in emailJoin.DefaultIfEmpty()

                        select new CustomerDto
                        {
                            CustomerId = c.CustomerId,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            EmailAddress = email != null ? email.EmailAddress : "No Email",

                            OrderCount = c.Orders.Count(),

                            OrderIds = c.Orders.Any()
                                       ? string.Join(", ", c.Orders.Select(o => o.SalesOrderId))
                                       : "No Orders"
                        };

            return await query
                .AsNoTracking()
                .Take(1000)
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .AsNoTracking()
         .Where(c => c.CustomerId == id)
         .Select(c => new CustomerDto
         {
             CustomerId = c.CustomerId,
             FirstName = c.FirstName,
             LastName = c.LastName,
             EmailAddress = c.EmailAddress,
             OrderCount = c.Orders.Count()
         })
         .FirstOrDefaultAsync();
        }


        public async Task AddAsync(CreateCustomerDto dto)
        {
            var newCustomer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress
            };

            await _context.Customers.AddAsync(newCustomer);
        }

        public async Task<IEnumerable<CustomerOrderDetailDto>> GetCustomerOrderDetailsAsync(int customerId)
        {
            var query = from od in _context.SalesOrderDetails
                        join o in _context.SalesOrders
                        on od.SalesOrderId equals o.SalesOrderId
                        join p in _context.Product
                        on od.ProductId equals p.ProductId
                        where o.CustomerId == customerId
                        select new CustomerOrderDetailDto
                        {
                            // unique Id
                            SalesOrderDetailId = od.SalesOrderDetailId,

                            SalesOrderId = od.SalesOrderId,
                            ProductId = p.ProductId,
                            ProductName = p.Name,
                            Quantity = od.OrderQty,
                            LineTotal = od.LineTotal,
                            OrderDate = o.orderDate
                        };
            return await query.ToListAsync();
        }

        public async Task DeleteOrderAsync(int OrderDetailId)
        {
            var entity = await _context.SalesOrderDetails.FirstOrDefaultAsync
                (ed => ed.SalesOrderDetailId == OrderDetailId);

            if (entity != null)
            {
                _context.SalesOrderDetails.Remove(entity);
            }

        }
    }
}
