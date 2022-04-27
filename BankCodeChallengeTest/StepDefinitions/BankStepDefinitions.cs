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

        #region Given Steps

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

        #endregion
        #region Then Steps

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