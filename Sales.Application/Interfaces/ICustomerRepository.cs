using Sales.Application.DTO;
using Sales.Application.DTOs;
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
        //Create
        Task<CustomerDto?> GetByIdAsync(int id);
        Task AddAsync(CreateCustomerDto customer);
        Task<IEnumerable<CustomerOrderDetailDto>> GetCustomerOrderDetailsAsync(int customerId);

        //Read
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();


        //Update
        Task UpdateOrderDetailsAsync(CustomerOrderDetailDto dto);
        Task SaveChangesAsync();
    }
}
