
using Work_Bank_Api.Models;

namespace Work_Bank_Api.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public string HebrewName { get; set; } = string.Empty;
      
        public string EnglishName { get; set; } = string.Empty;
 
        public string IdNumber { get; set; } = string.Empty;

        public string AccountNumber { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public decimal Amount { get; set; } 

        public DateTime Date { get; set; }

        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }

        public bool IsEdited { get; set; }

        public bool IsDeleted { get; set; }
    }
}
