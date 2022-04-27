using BankCodeChallenge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCodeChallengeTest
{
    public class BankFixture
    {
        public Bank bank = new Bank("test");
        public bool transactionApproved = false;

        public BankFixture()
        {

        }
    }
}
