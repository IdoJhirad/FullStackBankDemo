
namespace Service.Validators
{
    public class TransactionValidator : AbstractValidator<Work_Bank_Api.Dtos.CreateTtansactionDto>
    {
        public TransactionValidator()
        {
            const string REGEX_FOR_HEBREW_NAME = @"^(?=.*[\u05D0-\u05EA])[\u05D0-\u05EA\s'\-]{1,20}$";
            const string REGEX_FOR_ENGLISH_NAME = "^(?=.*[[A-Za-z])[A-Za-z\\s'-]{1,15}$";
            const string REGEX_FOR_ID = @"^\d{9}$";
            const string REGEX_FOR_ACCOUNT_NUMBER = @"^\d{1,9}$";

            RuleFor(transaction => transaction.HebrewName)
                .NotNull()
                .NotEmpty()
                .Matches(REGEX_FOR_HEBREW_NAME)
                .MaximumLength(20);

            RuleFor(transaction => transaction.EnglishName)
                .NotNull()
                .NotEmpty()
                .Matches(REGEX_FOR_ENGLISH_NAME)
                .MaximumLength(15);

            RuleFor(transaction => transaction.IdNumber)
              .NotNull()
              .NotEmpty()
              .Matches(REGEX_FOR_ID);

            RuleFor(transaction => transaction.AccountNumber)
             .NotNull()
             .NotEmpty()
             .Matches(REGEX_FOR_ACCOUNT_NUMBER);

            RuleFor(transaction => transaction.BirthDate)
                .NotNull();
            
            RuleFor(transaction => transaction.Amount)
                .NotNull()
                .NotEmpty()
                .Must(amount => amount > 1 && amount < 100000);
        }
    }
}
