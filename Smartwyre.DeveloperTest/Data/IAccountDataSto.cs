using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public interface IAccountDataSto
    {
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
    }
}