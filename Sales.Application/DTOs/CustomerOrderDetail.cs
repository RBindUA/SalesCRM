using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTOs
{
    public class CustomerOrderDetail
    {
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public short Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
