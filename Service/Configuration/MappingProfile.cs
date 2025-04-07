namespace Service.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionModel, DTOTransaction>();
            CreateMap<DTORequestTransaction, TransactionModel>();
        }
    }
}
