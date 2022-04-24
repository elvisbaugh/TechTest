using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IPaymentStrategyFactory
    {
        IPaymentStrategy GetStrategy(PaymentScheme paymentStrategy);
    }
}