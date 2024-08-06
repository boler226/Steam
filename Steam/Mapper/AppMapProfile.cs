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
            CreateMap<DiscountEntity, DiscountViewModel>();
            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<MediaEntity, MediaViewModel>();
            CreateMap<NewsMediaEntity, MediaViewModel>();
            CreateMap<GameMediaEntity, MediaViewModel>();
            CreateMap<CommentEntity, CommentsViewModel>();
            CreateMap<NewsCommentEntity, CommentsViewModel>();

            CreateMap<GameCreateViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameMedia, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRequirements, opt => opt.Ignore());

            CreateMap<NewsCreateViewModel, NewsEntity>()
                .ForMember(dest => dest.NewsMedia, opt => opt.Ignore());

            CreateMap<GameEditViewModel, GameEntity>()
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GameMedia, opt => opt.Ignore());

            CreateMap<NewsCreateViewModel, NewsEntity>()
                .ForMember(dest => dest.NewsMedia, opt => opt.Ignore());

            CreateMap<NewsEntity, NewsItemViewModel>()
                       .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src.NewsMedia.Select(nm => nm.Media)))
                       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.NewsComments))
                       .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
                       .ForMember(dest => dest.UserOrDeveloper, opt => opt.MapFrom(src => src.UserOrDeveloper));

            CreateMap<GameEntity, GameItemViewModel>()
                .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src.GameMedia.Select(gm => gm.Media)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.GameComments.Select(gc => gc.Comment)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.GameCategories.Select(gc => gc.Category)));
        }
    }
}
