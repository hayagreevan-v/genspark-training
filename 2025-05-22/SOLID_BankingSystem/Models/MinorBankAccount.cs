using SOLID_BankingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Models
{
    public class MinorBankAccount : IBankAccount
    {
        int AccountNo { get => AccountNo; set => AccountNo = value; }
        double Balance { get => Balance; set => Balance = value; }
        int IBankAccount.AccountNo { get => AccountNo; set => AccountNo = value; }
        double IBankAccount.Balance { get => Balance; set => Balance = value; }

        public MinorBankAccount(int AccountNo, double Balance) {
            this.AccountNo = AccountNo;
            this.Balance = Balance;
        }
        public void Desposit(double amount)
        {
            Balance += amount;
        }

        public void withDraw(double amount)
        {
            if (Balance < amount)
            {
                throw new Exception("Tnsufficient Balance");
            }
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"Minor Bank Account :\nAccount No. : {AccountNo}\t Balance : {Balance}";
        }

        void IBankAccount.withDraw(double amount)
        {
            throw new NotImplementedException();
        }

        void IBankAccount.Desposit(double amount)
        {
            throw new NotImplementedException();
        }
    }
}
