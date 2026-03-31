using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.DTO
{
    public class CustomerDto : IDataErrorInfo
    {
        public int CustomerId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public int OrderCount { get; set; }
        public string OrderIds { get; set; } = "No Orders";
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                            error = "Name cannot be empty";
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                            error = "Name cannot be empty";
                        break;

                }
                return error;
            }
        }
        public string Error => string.Empty;
    }
}
