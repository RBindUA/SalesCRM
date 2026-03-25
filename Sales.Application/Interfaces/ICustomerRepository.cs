using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task AddAsync(Customer customer);
    }
}
