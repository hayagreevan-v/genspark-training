using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Models
{
    public class CreateBankAccount
    {
        public static StandardBankAccount createStandard(int accNo, double balance)
        {
            return new StandardBankAccount(accNo, balance);
        }
        public static MinorBankAccount createMinor(int accNo, double balance)
        {
            return new MinorBankAccount(accNo, balance);
        }
    }
}
