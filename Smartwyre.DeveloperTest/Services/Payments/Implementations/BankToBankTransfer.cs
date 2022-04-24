using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Payments.Implementations
{
    public class BankToBankTransfer : IPaymentStrategy
    {
        public MakePaymentResult GetPaymentResult(PaymentInfoDto paymentInfoDto)
        {
            var result = new MakePaymentResult();

            if (paymentInfoDto.Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
            {
                result.Success = true;
            }
            return result;
        }
    }
}
