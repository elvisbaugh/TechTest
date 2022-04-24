using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data
{
    public class AccountDataSto : IAccountDataSto
    {
        /// <summary>
        /// The methods in this class should be asynchronous in a real world project.
        /// But for this illustration I think this is okay for now.
        /// </summary>
        private List<Account> _accountEntities { get; set; }

        public AccountDataSto()
        {
            //just for mocking purposes
            _accountEntities = new List<Account>()
            {
                new Account(){ AccountNumber = "1111-111-111", AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer, Balance = 30000.00m, Status = AccountStatus.Live},
                new Account(){AccountNumber = "222-222-22", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 30000.00m, Status = AccountStatus.Live},
            };
        }

        public Account GetAccount(string accountNumber)
        {
            return _accountEntities.Find(x => x.AccountNumber.Equals(accountNumber));
        }

        public void UpdateAccount(Account account)
        {
            var data = _accountEntities.Find(x => x.AccountNumber == account.AccountNumber);
            data.Balance = account.Balance;
        }

    }
}
