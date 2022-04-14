using BankCodeChallenge.Accounts;

namespace BankCodeChallenge
{
    public class Bank
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; } = new List<Account>();

        public Bank(string name)
        {
            Name = name;
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}