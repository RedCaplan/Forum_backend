using AutoMapper;
using Forum.Api.DTO;
using Forum.DAL.Models.Entities;

namespace Forum.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to dto
            CreateMap<Category, CategoryDTO>();
            CreateMap<Thread, ThreadDTO>()
                .ForMember(dest =>
                    dest.CreatedBy,
                    opt => opt.MapFrom(src => src.UserAccount.UserName));

            //Dto to domain
            CreateMap<CategoryDTO, Category>();
            CreateMap<ThreadDTO, Thread>();
            CreateMap<RegisterDTO, UserAccount>();
            CreateMap<LoginDTO, UserAccount>();
        }
    }
}
