using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTOs
{
    public class CustomerOrderDetailDto: IDataErrorInfo
    {
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public short Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime OrderDate { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(ProductName):
                        if (string.IsNullOrWhiteSpace(ProductName))
                            error = "Product name cannot be empty.";
                        break;

                    case nameof(Quantity):
                        if (Quantity <= 0)
                            error = "Quantity must be a positive number.";
                        break;

                    case nameof(LineTotal):
                        if (LineTotal < 0)
                            error = "Total price cannot be negative.";
                        break;
                }
                return error;
            }
        }
        public string Error => string.Empty;
    }
}
