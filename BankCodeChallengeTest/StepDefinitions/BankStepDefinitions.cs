using BankCodeChallenge;
using BankCodeChallenge.Accounts;

namespace BankCodeChallengeTest.StepDefinitions
{
    [Binding]
    public sealed class BankStepDefinitions
    {
        private Bank bank;
        private bool transactionApproved = false;

        #region Given
        [Given("there is a bank named (.*)")]
        public void GivenThereIsABankNamedX(string name)
        {
            bank = new Bank(name);
        }

        [Given("the bank has a checking account with owner (.*)")]
        public void GivenTheBankHasACheckingAccountWithOwnerX(string name)
        {
            bank.Accounts.Add(new CheckingAccount(name));
        }

        [Given("the bank has an investment account with owner (.*) of type (.*)")]
        public void GivenTheBankHasAnInvestmentAccountWithOwnerXOfTypeY(string name, InvestmentAccount.InvestmentAccountType investmentType)
        {
            bank.Accounts.Add(new InvestmentAccount(name, investmentType));
        }

        [Given("user (.*) has a balance of (.*)")]
        public void GivenUserXHasABalanceOfY(string user, double balance)
        {
            Account account = bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                account.Balance = balance;
            }
        }

        #endregion
        #region When

        [When(@"user (.*) makes a deposit of (.*)")]
        public void WhenUserXMakesADepositOfY(string user, double amount)
        {
            Account account = bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                Transaction newTransaction = Transaction.CreateDeposit(amount, account);
                transactionApproved = account.TryPerformTransaction(newTransaction);
            }
        }

        [When(@"user (.*) makes a withdrawal of (.*)")]
        public void WhenUserXMakesAWithdrawalOfY(string user, double amount)
        {
            Account account = bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                Transaction newTransaction = Transaction.CreateWithdrawal(amount, account);
                transactionApproved = account.TryPerformTransaction(newTransaction);
            }
        }

        #endregion
        #region Then

        [Then(@"user (.*) has a balance of (.*)")]
        public void ThenUserXBalanceIsY(string user, double balance)
        {
            Account account = bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                Assert.AreEqual(balance, account.Balance);
            }
        }

        [Then(@"the transaction was (approved|denied)")]
        public void ThenTheTransactionWasApproved(string status)
        {
            bool shouldBeApproved = false;

            if (status == "approved")
            {
                shouldBeApproved = true;
            }
            else if (status == "denied")
            {
                shouldBeApproved = false;
            }

            Assert.AreEqual(shouldBeApproved, transactionApproved);
        }


        #endregion
    }
}