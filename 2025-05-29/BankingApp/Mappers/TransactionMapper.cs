using System;
using BankingApp.Models;
using BankingApp.Models.DTOs;

namespace BankingApp.Mappers;

public class TransactionMapper
{
    public static Transaction TransactionFromTransactionDTO(TransactionDTO transactionDTO)
    {
        return new Transaction
        {
            FromAccountNo = transactionDTO.FromAccountNo,
            ToAccountNo = transactionDTO.ToAccountNo,
            Amount = transactionDTO.Amount
        }; 
    }
}
