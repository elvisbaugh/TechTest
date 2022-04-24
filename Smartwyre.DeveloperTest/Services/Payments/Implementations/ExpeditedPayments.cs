using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Payments.Implementations
{
    public class ExpeditedPayments : IPaymentStrategy
    {
        private readonly MakePaymentResult _makePaymentResult = new();

        public MakePaymentResult GetPaymentResult(PaymentInfoDto paymentInfoDto)
        {
            if (paymentInfoDto.Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments) && paymentInfoDto.Account.Balance > paymentInfoDto.Amount)
            {
                _makePaymentResult.Success = true;
            }

            return _makePaymentResult;
        }
    }
}
