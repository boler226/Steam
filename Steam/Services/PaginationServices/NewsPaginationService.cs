using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        protected override IQueryable<NewsEntity> GetQuery()
        {
            return context.News.AsQueryable();
        }

        protected override IQueryable<NewsEntity> FilterQuery(IQueryable<NewsEntity> query, NewsFilterViewModel model)
        {
            if (model.ByRelease.HasValue && model.ByRelease.Value)
                query = query.OrderByDescending(n => n.DateOfRelease);

            return query; 
        }
        
    }
}
