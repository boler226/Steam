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

            CreateMap<RegisterViewModel, UserEntity>()
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName));

            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryEntity>();

            CreateMap<GameEntity, GameItemViewModel>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.GameCategories.Select(gc => gc.Category)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.GameImages.Select(gi => new GameImageViewModel
                {
                    Id = gi.Id,
                    Name = gi.Name,
                    Priority = gi.Priority
                })))
                .ForMember(dest => dest.News, opt => opt.MapFrom(src => src.News));

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
