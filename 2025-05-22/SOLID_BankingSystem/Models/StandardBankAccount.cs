using SOLID_BankingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Models
{
    public class StandardBankAccount : IBankAccount,ITransferMoney
    {
        int AccountNo { get; set; }

        double Balance { get; set; }
        int IBankAccount.AccountNo { get => AccountNo; set => AccountNo = value; }
        double IBankAccount.Balance { get => Balance; set => Balance = value; }

        public StandardBankAccount(int AccountNo, double Balance) { 
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

        public override string ToString() {
            return $"Standard Bank Account :\nAccount No. : {AccountNo}\t Balance : {Balance}";
        }

        public void SendMoney(double amount)
        {
            if (Balance < amount)
            {
                throw new Exception("Tnsufficient Balance");
            }
            Balance -= amount;
        }

        public void RecieveMoney(double amount)
        {
            Balance += amount;
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
