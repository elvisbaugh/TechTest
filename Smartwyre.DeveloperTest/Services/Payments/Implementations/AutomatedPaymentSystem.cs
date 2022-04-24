using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Payments.Implementations
{
    public class AutomatedPaymentSystem : IPaymentStrategy
    {
        private readonly MakePaymentResult _makePaymentResult = new();

        public MakePaymentResult GetPaymentResult(PaymentInfoDto paymentInfoDto)
        {
            if (paymentInfoDto.Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem) && paymentInfoDto.Account.Status == AccountStatus.Live)
            {
                _makePaymentResult.Success = true;
            }
            return _makePaymentResult;
        }
    }
}
