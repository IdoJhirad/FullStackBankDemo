using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Work_Bank_Api.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        
        [MaxLength(20, ErrorMessage = " Name Should Contanin no more than 20 Character.")]
        [RegularExpression("^[\\u05D0-\\u05EA\\s'-]{1,20}$")]
        public string HebrewName { get; set; } = string.Empty;

        [MaxLength(15, ErrorMessage = " Name Should Contanin no more than 20 Character.")]
        [RegularExpression("^[A-Za-z\\s'-]{1,15}$")]
        public string EnglishName { get; set; } = string.Empty;

        [RegularExpression(@"^\d{9}$", ErrorMessage = "IdNumber must be exactly 9 digits.")]
        public string IdNumber { get; set; } = string.Empty;


        [RegularExpression(@"^\d{1-9}$", ErrorMessage = "Account number must be exactly 9 digits.")]
        public string AccountNumber { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; } = 0m;

        public DateTime Date { get; set; } = DateTime.Now;

        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }

        public bool IsEdited { get; set; } = false;

        public bool IsDeleted { get; set; }  = false;
    }
    public enum TransactionType
    {
        Deposit = 0,
        Withdrawal = 1,
       
    }
    public enum TransactionStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2
    }
}
