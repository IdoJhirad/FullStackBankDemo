using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Work_Bank_Api.Dtos
{
    public class CreateTtansactionDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = " Name Should Contanin no more than 20 Character.")]
        [RegularExpression("^(?=.*[\\u05D0-\\u05EA])[\\u05D0-\\u05EA\\s'-]{1,20}$", ErrorMessage = "HebrewName should contain at least one Hebrew letter and only contain allowed characters.")]
        public string HebrewName { get; set; } = string.Empty;
       
        [Required]
        [MaxLength(15, ErrorMessage = " Name Should Contanin no more than 20 Character.")]
        [RegularExpression("^(?=.*[[A-Za-z])[A-Za-z\\s'-]{1,15}$", ErrorMessage = "EnglishName should contain only english latters")]
        public string EnglishName { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "IdNumber must be exactly 9 digits.")]
        public string IdNumber { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{1,9}$" , ErrorMessage = "Account number must be 1 - 10 digits.")]
        public string AccountNumber { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Value Must Bigger Than 1")]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; } = 0m;

    }
    
}
