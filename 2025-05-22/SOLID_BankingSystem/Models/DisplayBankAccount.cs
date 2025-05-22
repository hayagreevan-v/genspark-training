using SOLID_BankingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Models
{
    public static class DisplayBankAccount
    {
        public static void DisplayBankAccountDetails(this IBankAccount account)
        {
           Console.WriteLine(account.ToString());
        }
    }
}
