using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class Email
    {
        public int BusinessEntityId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
    }
}
