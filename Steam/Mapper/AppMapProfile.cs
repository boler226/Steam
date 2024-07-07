using AutoMapper;
using Steam.Data;
using Steam.Data.Entities;
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

            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryEntity>();

            CreateMap<GameEntity, GameItemViewModel>();

            CreateMap<GameCreateViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameImages, opt => opt.Ignore());

            CreateMap<GameEditViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameImages, opt => opt.Ignore());

            CreateMap<NewsEntity, NewsItemViewModel>();

            CreateMap<NewsCreateViewModel, NewsEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<NewsEntity, NewsEditViewModel>();
        }
    }
}
