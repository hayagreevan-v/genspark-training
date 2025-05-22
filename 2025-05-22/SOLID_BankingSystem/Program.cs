using SOLID_BankingSystem.Interfaces;
using SOLID_BankingSystem.Models;

namespace SOLID_BankingSystem
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            StandardBankAccount sb1 = new StandardBankAccount(1,1000);
            StandardBankAccount sb2 = new StandardBankAccount(2,2000);

            TransferMoney.Transfer(sb1 ,sb2,500);
            sb1.DisplayBankAccountDetails();
            sb2 .DisplayBankAccountDetails();

        }
    }
}
