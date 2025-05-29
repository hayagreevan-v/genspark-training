using System;
using BankingApp.Interfaces;
using BankingApp.Models.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Mappers;

namespace BankingApp.Services;

public class TransactionService : IService<int, Transaction, TransactionDTO>
{
    private readonly IRepository<int,Transaction> _transactionRepository;
    private readonly IRepository<int,User> _userRepository;
    public TransactionService(IRepository<int,Transaction> transactionRepository, IRepository<int,User> userRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }
    public async Task<Transaction> Add(TransactionDTO item)
    {
        using var dbTransaction = await _transactionRepository.StartTransaction();
        Transaction newTransaction = TransactionMapper.TransactionFromTransactionDTO(item);
        newTransaction = await _transactionRepository.Add(newTransaction);
        try
        {
            User FromUser = await _userRepository.Get(newTransaction.FromAccountNo);
            if (FromUser.Balance < newTransaction.Amount)
            {
                throw new Exception("Insufficient Balance for Sender");
            }
            FromUser.Balance -= newTransaction.Amount;
            await _userRepository.Update(newTransaction.FromAccountNo, FromUser);

            User ToUser = await _userRepository.Get(newTransaction.ToAccountNo);
            ToUser.Balance += newTransaction.Amount;
            await _userRepository.Update(newTransaction.ToAccountNo, ToUser);

        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
        await dbTransaction.CommitAsync();
        return newTransaction;

    }

    public async Task<Transaction> View(int id)
    {
        Transaction? t = await _transactionRepository.Get(id);
        if (t == null) throw new Exception("No transaction found");
        return t;
    }

    public async Task<ICollection<Transaction>> ViewAll()
    {
        return await _transactionRepository.GetAll();
    }
}
