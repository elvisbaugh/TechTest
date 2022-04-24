using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Payments.Abstractions;
using Smartwyre.DeveloperTest.Services.Payments.Implementations;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private Mock<IPaymentStrategyFactory> _mockPaymentStrategyFactory;
        private Mock<IAccountDataSto> _mockAccountDataSto;

        private PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _mockPaymentStrategyFactory = new Mock<IPaymentStrategyFactory>();
            _mockAccountDataSto = new Mock<IAccountDataSto>();

            _paymentService = new PaymentService(_mockAccountDataSto.Object, _mockPaymentStrategyFactory.Object);
        }

        [Fact]
        public void Should_Return_True_When_PaymentScheme_Is_BankToBankTransfer()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer, Balance = 30000.00m, Status = AccountStatus.Live };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new BankToBankTransfer());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            };            

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.BankToBankTransfer), Times.Once);
            Assert.Equal(15000.00m, account.Balance);
            Assert.True(result.Success);
        }

        [Fact]
        public void Should_Return_True_When_PaymentScheme_Is_ExpeditedPayments()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments, Balance = 30000.00m, Status = AccountStatus.Live };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new ExpeditedPayments());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.ExpeditedPayments
            };

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.ExpeditedPayments), Times.Once);
            Assert.Equal(15000.00m, account.Balance);
            Assert.True(result.Success);
        }

        [Fact]
        public void Should_Return_False_When_PaymentScheme_ExpeditedPayments_Balance_Is_Insufficient()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments, Balance = 500.00m, Status = AccountStatus.Live };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new ExpeditedPayments());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.ExpeditedPayments
            };

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.ExpeditedPayments), Times.Once);
            Assert.Equal(500.00m, account.Balance);
            Assert.False(result.Success);
        }

        [Fact]
        public void Should_Return_True_When_PaymentScheme_Is_AutomatedPaymentSystem()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 35000.00m, Status = AccountStatus.Live };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new AutomatedPaymentSystem());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.AutomatedPaymentSystem), Times.Once);
            Assert.Equal(20000.00m, account.Balance);
            Assert.True(result.Success);
        }

        [Fact]
        public void Should_Return_False_When_PaymentScheme_AutomatedPaymentSystem_Account_Is_NotLive()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 35000.00m, Status = AccountStatus.Disabled };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new AutomatedPaymentSystem());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.AutomatedPaymentSystem), Times.Once);
            Assert.Equal(35000.00m, account.Balance);
            Assert.False(result.Success);
        }

        [Fact]
        public void Should_Return_False_When_PaymentScheme_Does_Not_Match_Account()
        {
            //Arrange
            var account = new Account() { AccountNumber = "55-33-258", AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer, Balance = 35000.00m, Status = AccountStatus.Live };
            _mockPaymentStrategyFactory.Setup(x => x.GetStrategy(It.IsAny<PaymentScheme>())).Returns(new AutomatedPaymentSystem());
            _mockAccountDataSto.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            var PaymentDetails = new MakePaymentRequest()
            {
                Amount = 15000.00m,
                CreditorAccountNumber = "111-222-333",
                DebtorAccountNumber = "55-33-258",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            //Act
            var result = _paymentService.MakePayment(PaymentDetails);

            //Assert
            _mockPaymentStrategyFactory.Verify(x => x.GetStrategy(PaymentScheme.AutomatedPaymentSystem), Times.Once);
            Assert.Equal(35000.00m, account.Balance);
            Assert.False(result.Success);
        }
    }
}
