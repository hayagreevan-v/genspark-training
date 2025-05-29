namespace BankingApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int FromAccountNo { get; set; }
        public int ToAccountNo { get; set; }
        public double Amount { get; set; }

        public User? FromUser { get; set; }
        public User? ToUser { get; set; }
    }
}