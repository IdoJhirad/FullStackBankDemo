

namespace Work_Bank_Api.Dtos
{
    public class CreateTtansactionDto
    {
        /// <summary>
        /// hebrew name 
        /// </summary>
        /// <example>עידו ג'ירד</example>
        public string HebrewName { get; set; } = string.Empty;

        /// <summary>
        /// EnglishName
        /// </summary>
        /// <example>Ido jhirad</example>  
        public string EnglishName { get; set; } = string.Empty;

        /// <summary>
        /// id number
        /// </summary>
        /// <example>318722031</example>
        public string IdNumber { get; set; } = string.Empty;

        /// <summary>
        /// Acount number
        /// </summary>
        /// <example> 1234 </example>

        public string AccountNumber { get; set; } = string.Empty;
        /// <summary>
        /// Birth date
        /// </summary>
        /// <example>2025-04-06</example>
  
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// amount 
        /// </summary>
        /// <example>15</example>
        public decimal Amount { get; set; } = 0m;

    }
    
}
