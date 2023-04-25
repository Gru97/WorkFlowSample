using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFE.Console.TransferMoneyExample.Domain
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Owner { get; private  set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        private Account()
        {
            
        }
        public Account(string owner, string accountNumber, decimal balance)
        {
            Id = Guid.NewGuid();
            Owner = owner;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void SetBalance(decimal balance)
        {
            Balance = balance;
        }
    }
}
