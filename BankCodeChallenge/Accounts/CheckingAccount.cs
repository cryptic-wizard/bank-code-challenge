using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankCodeChallenge.Transaction;

namespace BankCodeChallenge.Accounts
{
    public class CheckingAccount : Account
    {
        public override AccountType Type => AccountType.Checking;

        public CheckingAccount(string owner) : base(owner)
        {

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

            return true;
        }
    }
}
