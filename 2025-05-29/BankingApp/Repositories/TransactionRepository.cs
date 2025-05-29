using System;
using System.Threading.Tasks;
using BankingApp.Contexts;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

namespace BankingApp.Repositories;

public class TransactionRepository : Repository<int, Transaction>
{
    public TransactionRepository(BankContext bankContext) : base(bankContext) { }

    public override async Task<Transaction> Get(int id)
    {
        var transaction = await _bankContext.Transactions.FindAsync(id);
        if (transaction == null) throw new Exception("No data found!");
        return transaction;
    }

    public override async Task<ICollection<Transaction>> GetAll()
    {
        var transactions = await _bankContext.Transactions.ToListAsync();
        if (transactions == null || transactions.Count == 0)
            throw new Exception("No data found!");
        return transactions;
    }
}
