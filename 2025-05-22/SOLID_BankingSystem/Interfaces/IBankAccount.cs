using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_BankingSystem.Interfaces
{
    public interface IBankAccount
    {
        public int AccountNo { get; set; }
        public double Balance { get; set; }
        void withDraw(double amount);
        void Desposit(double amount);
    }
}
