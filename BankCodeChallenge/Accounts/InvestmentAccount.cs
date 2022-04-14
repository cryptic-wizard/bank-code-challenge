using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankCodeChallenge.Transaction;

namespace BankCodeChallenge.Accounts
{
    public class InvestmentAccount : Account
    {
        public override AccountType Type { get => AccountType.Investment; }
        public InvestmentAccountType InvestmentType { get; private set; }

        public const double IndividualWithdrawalLimit = 500;

        public InvestmentAccount(string owner, InvestmentAccountType investmentType) : base(owner)
        {
            InvestmentType = investmentType;
        }

        public enum InvestmentAccountType
        {
            Individual,
            Corporate
        }

        public override bool IsValidTransaction(Transaction transaction)
        {
            if (transaction.Type == TransactionType.Withdrawal && transaction.Amount > Balance)
            {
                return false;
            }
            else if (transaction.Type == TransactionType.Transfer && this == transaction.From &&
                     transaction.Amount > Balance)
            {
                return false;
            }
            // Does individual investment account exceed withdrawal limit
            else if (InvestmentType == InvestmentAccountType.Individual &&
                     transaction.Amount > IndividualWithdrawalLimit)
            {
                return false;
            }

            return true;
        }
    }
}
