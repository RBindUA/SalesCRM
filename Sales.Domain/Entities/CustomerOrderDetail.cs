using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class CustomerOrderDetail
    {
        public int SalesOrderId {get;set;} 
        public int ProductId {get;set;}
        public ushort Quantity {get;set;}
        public decimal Price {get;set;}
        public DateTime OrderDate {get;set;}

        public string ProductName {get;set;} = string.Empty;
    }
}
