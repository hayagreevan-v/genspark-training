using SOLID_BankingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Models
{
    class TransferMoney
    {
        public static bool Transfer(ITransferMoney b1, ITransferMoney b2, double amount)
        {
            try
            {
                b1.SendMoney(amount);
                b2.RecieveMoney(amount);
                Console.WriteLine($"Successful Transfer of Rs. {amount} from AccNo : {b1} to AccNo : {b2}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
