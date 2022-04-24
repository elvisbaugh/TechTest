using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Services.Payments.Implementations;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddTransient<PaymentService, PaymentService>()
                .BuildServiceProvider()
                .GetService<PaymentService>()
                .MakePayment(MakePayment());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
           services.AddTransient<IPaymentStrategyFactory, PaymentStrategyFactory>();
           services.AddTransient<IPaymentStrategy, BankToBankTransfer>();
           services.AddTransient<IPaymentStrategy, ExpeditedPayments>();
           services.AddTransient<IPaymentStrategy, AutomatedPaymentSystem>();
           services.AddTransient<IAccountDataSto, AccountDataSto>(); 

        }

        private static MakePaymentRequest MakePayment()
        {
            return new MakePaymentRequest()
            {
                Amount = 500.00m,
                CreditorAccountNumber = "555-333-222",
                DebtorAccountNumber = "1111-111-111",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            };
        }
                
    }
}
