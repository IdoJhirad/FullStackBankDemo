




namespace Service.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionModel, TransactionDto>();
            CreateMap<CreateTtansactionDto, TransactionModel>();
        }
    }
}
