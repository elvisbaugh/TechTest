using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services.Payments.Abstractions
{
    public interface IPaymentStrategy
    {
        MakePaymentResult GetPaymentResult(PaymentInfoDto paymentInfoDto);
    }
}
