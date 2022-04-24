using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Model;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataSto _accountDataSto;
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;

        public PaymentService(IAccountDataSto accountDataSto, IPaymentStrategyFactory paymentStrategyFactory)
        {
            _accountDataSto = accountDataSto;
            _paymentStrategyFactory = paymentStrategyFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            var paymentInfo = new PaymentInfoDto()
            {
                Account = _accountDataSto.GetAccount(request.DebtorAccountNumber),
                Amount = request.Amount
            };
            PrintDetails(paymentInfo, "Before");
            if (paymentInfo.Account != null)
            {
                IPaymentStrategy paymentMethod = _paymentStrategyFactory.GetStrategy(request.PaymentScheme);

                result = paymentMethod.GetPaymentResult(paymentInfo);
            }

            return result.Success ? UpdateBackAccount(result, paymentInfo) : result;
        }

        //Could be improved to be an asynchronous method to update and saves changes async
        private MakePaymentResult UpdateBackAccount(MakePaymentResult result, PaymentInfoDto paymentInfo)
        {
             paymentInfo.Account.Balance -= paymentInfo.Amount;
            _accountDataSto.UpdateAccount(paymentInfo.Account);

            PrintDetails(paymentInfo, "After");

            return result;
        }

        private static void PrintDetails(PaymentInfoDto paymentInfo, string title)
        {
            Console.WriteLine($"_______{title}______\n ");
            Console.WriteLine($"Account Balance: {paymentInfo.Account.Balance}\n" +
                $"Account Number: {paymentInfo.Account.AccountNumber}\n" +
                $"Alowed Payment Scheme: {paymentInfo.Account.AllowedPaymentSchemes}");
            Console.WriteLine(" ");
        }
    }
}
