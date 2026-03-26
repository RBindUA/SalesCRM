using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }

        public ICollection<SalesOrder> Orders { get; set; } = new List<SalesOrder>();
        public string OrdersList => Orders != null && Orders.Any()
         ? string.Join(", ", Orders.Select(o => o.SalesOrderId))
         : "No Orders";
    }
}