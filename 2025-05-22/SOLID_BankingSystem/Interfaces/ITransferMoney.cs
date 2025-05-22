using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Interfaces
{
    public interface ITransferMoney
    {
        void SendMoney(double amount);
        void RecieveMoney(double amount);
    }
}
