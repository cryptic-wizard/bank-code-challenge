using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankCodeChallenge.Accounts;

namespace BankCodeChallenge
{
    public class Transaction
    {
        public Account From { get; private set; }
        public Account To { get; private set; }
        public TransactionType Type { get; private set; }
        public double Amount { get; private set; }

        public enum TransactionType
        {
            Deposit,
            Withdrawal,
            Transfer
        }

        public static Transaction CreateWithdrawal(double amount, Account from)
        {
            Transaction transaction = new Transaction
            {
                Type = TransactionType.Withdrawal,
                Amount = amount,
                From = from,
            };

            return transaction;
        }

        public static Transaction CreateDeposit(double amount, Account from)
        {
            Transaction transaction = new Transaction
            {
                Type = TransactionType.Deposit,
                Amount = amount,
                From = from,
            };

            return transaction;
        }

        public static Transaction CreateTransfer(double amount, Account from, Account to)
        {
            Transaction transaction = new Transaction
            {
                Type = TransactionType.Deposit,
                Amount = amount,
                From = from,
                To = to,
            };

            return transaction;
        }
    }
}
