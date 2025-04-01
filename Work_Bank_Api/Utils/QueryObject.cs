using Microsoft.AspNetCore.Mvc;
using Work_Bank_Api.Models;

namespace Work_Bank_Api.Utils
{
    public class QueryObject
    {
        public bool? IsEdited { get; set; }
        public bool? IsDeleted { get; set; }
      

        public TransactionType? Type { get; set; } 
        

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SortBy { get; set; } = null;

        [FromQuery(Name = "isDescending")]
        public bool IsDecsending { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
