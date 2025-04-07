namespace Work_Bank_Api.Model.Dtos
{
    public class DTOTransaction
    {
        /// <summary>
        /// id of user 
        /// </summary>
        /// <example>1234588</example>
        public int Id { get; set; }
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
        public decimal Amount { get; set; }
        /// <summary>
        ///  date
        /// </summary>
        /// <example>2025-04-06</example>

        public DateTime Date { get; set; }
        /// <summary>
        /// transaction type 
        /// </summary>
        /// <example>Deposite/ withdreawal</example>
        public TransactionType Type { get; set; }
        /// <summary>
        /// transaction staus 
        /// </summary>
        /// <example>sucsess faild pending</example>
        public TransactionStatus Status { get; set; }
        /// <summary>
        /// if transaction edited  
        /// </summary>
        /// <example>true / fals</example>
        public bool IsEdited { get; set; }
        /// <summary>
        /// if transaction deleted 
        /// </summary>
        /// <example>true / false</example>
        public bool IsDeleted { get; set; }
    }
}
