using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class SalesOrder
    {
        //Key, Sales.SalesOrderID
        public int SalesOrderId { get; set; }
        //Sales.SalesOrderHeader
        public DateTime orderDate { get; set; }
        //Sales.Status
        public decimal totalDue { get; set; }
        public byte status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
