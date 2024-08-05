using AutoMapper;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Data.Entities.Identity;
using Steam.Models.Account;
using Steam.Models.Category;
using Steam.Models.Game;
using Steam.Models.Helpers;
using Steam.Models.News;

namespace Steam.Mapper
{
    public class AppMapProfile : Profile
    {
        private readonly AppEFContext _context;
        public AppMapProfile(AppEFContext context) 
        {
            _context = context;

            CreateMap<UserEntity, UserViewModel>();
            CreateMap<RegisterViewModel, UserEntity>();


            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryEntity>();

            CreateMap<SystemRequirementsEntity, SystemRequirementsViewModel>();

            CreateMap<GameEntity, GameItemViewModel>();

            CreateMap<GameCreateViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameMedia, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRequirements, opt => opt.Ignore());

            CreateMap<NewsCreateViewModel, NewsEntity>()
                .ForMember(dest => dest.NewsMedia, opt => opt.Ignore());

            CreateMap<GameEditViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameMedia, opt => opt.Ignore());

            CreateMap<NewsEntity, NewsItemViewModel>();

            CreateMap<NewsCreateViewModel, NewsEntity>()
                .ForMember(dest => dest.NewsMedia, opt => opt.Ignore());

            CreateMap<NewsEntity, NewsEditViewModel>();
        }
    }
}
