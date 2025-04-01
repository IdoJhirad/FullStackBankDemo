using Work_Bank_Api.Dtos;
using Work_Bank_Api.Models;

namespace Work_Bank_Api.Utils
{
    public static class TransactionMapper
    {
        public static TransactionModel FromDtoToModel(this CreateTtansactionDto dto)
        {
            return new TransactionModel
            {
                AccountNumber = dto.AccountNumber,
                Amount = dto.Amount,
                BirthDate = dto.BirthDate,
                EnglishName = dto.EnglishName,
                HebrewName = dto.HebrewName,
                IdNumber = dto.IdNumber,               
            };
        }
        public static TransactionDto FromModelToDto(this TransactionModel dto)
        {
            return new TransactionDto
            {
                AccountNumber = dto.AccountNumber,
                Amount = dto.Amount,
                BirthDate = dto.BirthDate,
                EnglishName = dto.EnglishName,
                HebrewName = dto.HebrewName,
                IdNumber = dto.IdNumber,
                Type = dto.Type,
                Date = dto.Date,
                IsDeleted = dto.IsDeleted,
                IsEdited = dto.IsEdited,    
                Status = dto.Status,    
                Id = dto.Id 
            };
        }

    }
}
