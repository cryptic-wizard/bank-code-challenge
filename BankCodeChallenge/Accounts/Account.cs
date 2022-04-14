using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankCodeChallenge.Transaction;

namespace BankCodeChallenge.Accounts
{
    public abstract class Account
    {
        public double Balance { get; set; }
        public string Owner { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public abstract AccountType Type { get; }

        public enum AccountType
        {
            Checking,
            Investment
        }

        protected Account(string owner)
        {
            Owner = owner;
            Balance = 0;
            Transactions = new List<Transaction>();
        }

        /// <returns> false if transaction denied </returns>
        public abstract bool IsValidTransaction(Transaction transaction);

        /// <returns> false if transaction denied </returns>
        public bool TryPerformTransaction(Transaction transaction)
        {
            if (!IsValidTransaction(transaction))
            {
                return false;
            }

            switch (transaction.Type)
            {
                case TransactionType.Deposit:
                    Balance += transaction.Amount;
                    return true;
                case TransactionType.Withdrawal:
                    Balance -= transaction.Amount;
                    return true;
                case TransactionType.Transfer:
                    if (this == transaction.From)
                    {
                        Balance -= transaction.Amount;
                    }
                    else if (this == transaction.To)
                    {
                        Balance += transaction.Amount;
                    }
                    return true;
                default: throw new ArgumentOutOfRangeException(nameof(transaction.Type));
            }
        }
    }
}
