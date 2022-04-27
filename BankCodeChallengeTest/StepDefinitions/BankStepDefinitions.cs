using BankCodeChallenge;
using BankCodeChallenge.Accounts;

namespace BankCodeChallengeTest.StepDefinitions
{
    [Binding]
    public sealed class BankStepDefinitions
    {
        private readonly BankFixture fixture;

        public BankStepDefinitions(BankFixture fixture)
        {
            this.fixture = fixture;
        }

        #region Given
        [Given("there is a bank named (.*)")]
        public void GivenThereIsABankNamedX(string name)
        {
            fixture.bank = new Bank(name);
        }

        [Given("the bank has a checking account with owner (.*)")]
        public void GivenTheBankHasACheckingAccountWithOwnerX(string name)
        {
            fixture.bank.Accounts.Add(new CheckingAccount(name));
        }

        [Given("the bank has an (individual|corporate) investment account with owner (.*)")]
        public void GivenTheBankHasAnInvestmentAccountWithOwnerXOfTypeY(InvestmentAccount.InvestmentAccountType type, string name)
        {
            fixture.bank.Accounts.Add(new InvestmentAccount(name, type));
        }

        [Given("user (.*) has a balance of (.*)")]
        public void GivenUserXHasABalanceOfY(string user, double balance)
        {
            Account? account = fixture.bank.Accounts.Find(x => x.Owner == user);

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
            Account? account = fixture.bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                Transaction newTransaction = Transaction.CreateDeposit(amount, account);
                fixture.transactionApproved = account.TryPerformTransaction(newTransaction);
            }
        }

        [When(@"user (.*) makes a withdrawal of (.*)")]
        public void WhenUserXMakesAWithdrawalOfY(string user, double amount)
        {
            Account? account = fixture.bank.Accounts.Find(x => x.Owner == user);

            if (account == null)
            {
                Assert.Fail("User " + user + " was not found");
            }
            else
            {
                Transaction newTransaction = Transaction.CreateWithdrawal(amount, account);
                fixture.transactionApproved = account.TryPerformTransaction(newTransaction);
            }
        }

        [When(@"user (.*) makes a transfer of (.*) to user (.*)")]
        public void WhenUserXMakesATransferOfYToUserZ(string from, double amount, string to)
        {
            Account? fromAccount = fixture.bank.Accounts.Find(x => x.Owner == from);
            Account? toAccount = fixture.bank.Accounts.Find(x => x.Owner == to);

            if (fromAccount == null)
            {
                Assert.Fail("User " + from + " was not found");
            }
            else if (toAccount == null)
            {
                Assert.Fail("User " + to + " was not found");
            }
            else
            {
                Transaction newTransaction = Transaction.CreateTransfer(amount, fromAccount, toAccount);
                fixture.transactionApproved = fromAccount.IsValidTransaction(newTransaction);

                if (fixture.transactionApproved)
                {
                    fromAccount.TryPerformTransaction(newTransaction);
                    toAccount.TryPerformTransaction(newTransaction);
                }
            }
        }

        #endregion
        #region Then

        [Then(@"user (.*) has a balance of (.*)")]
        public void ThenUserXBalanceIsY(string user, double balance)
        {
            Account? account = fixture.bank.Accounts.Find(x => x.Owner == user);

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

            Assert.AreEqual(shouldBeApproved, fixture.transactionApproved);
        }


        #endregion
    }
}