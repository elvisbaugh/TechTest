using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Services.Payments.Implementations;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentStrategyFactory: IPaymentStrategyFactory
    {
        readonly Dictionary<PaymentScheme, IPaymentStrategy> strategyContext = new();

        public PaymentStrategyFactory()
        {
            strategyContext.Add(PaymentScheme.BankToBankTransfer, new BankToBankTransfer());
            strategyContext.Add(PaymentScheme.ExpeditedPayments, new ExpeditedPayments());
            strategyContext.Add(PaymentScheme.AutomatedPaymentSystem, new AutomatedPaymentSystem());
        }

        public IPaymentStrategy GetStrategy(PaymentScheme paymentStrategy)
        {
            return strategyContext[paymentStrategy];
        }

    }
}
