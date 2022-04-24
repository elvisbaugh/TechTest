using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Model
{
    public class PaymentInfoDto
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
