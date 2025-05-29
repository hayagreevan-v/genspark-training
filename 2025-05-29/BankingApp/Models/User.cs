namespace BankingApp.Models
{
    public class User
    {
        public int AccountNo { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Balance { get; set; }

        public ICollection<Transaction>? SentTransactions { get; set; }
        public ICollection<Transaction>? RecievedTransactions { get; set; }
    }
}