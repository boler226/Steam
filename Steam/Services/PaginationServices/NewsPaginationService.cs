using AutoMapper;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.News;
using Steam.Services.PaginationServices.Base;

namespace Steam.Services.PaginationServices
{
    public class NewsPaginationService(
        AppEFContext context,
        IMapper mapper
        ) : PaginationService<NewsEntity, NewsItemViewModel, NewsFilterViewModel>(mapper)
    {
        protected override IQueryable<NewsEntity> GetQuery() => context.News.OrderBy(n => n.Id);

        protected override IQueryable<NewsEntity> FilterQuery(IQueryable<NewsEntity> query, NewsFilterViewModel paginationVm)
        {
           return query.OrderByDescending(n => n.DateOfRelease);
        }
        
    }
}
