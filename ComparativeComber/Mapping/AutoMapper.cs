using ComparativeComber.Entities;
using AutoMapper;
using ComparativeComber.Entities;

namespace ComparativeComber.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<ComparableSale, ComparableSaleDto>().ReverseMap();
    
        }
    }
}