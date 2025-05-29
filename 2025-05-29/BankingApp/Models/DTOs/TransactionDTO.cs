using System;

namespace BankingApp.Models.DTOs;

public class TransactionDTO
{
    public int FromAccountNo { get; set; }
    public int ToAccountNo { get; set; }
    public double Amount { get; set; }
}
