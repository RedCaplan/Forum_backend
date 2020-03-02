using AutoMapper;
using Forum.Core.Model;
using Forum.Web.DTO;

namespace Forum.Web.Mapping
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
