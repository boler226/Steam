using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Steam.Interfaces;
using Steam.Models.Pagination;

namespace Steam.Services.PaginationServices.Base
{
    public abstract class PaginationService <EntityType, EntityViewModelType, PaginationViewModelType>(
        IMapper mapper
        ) : IPaginationService<EntityViewModelType, PaginationViewModelType> where PaginationViewModelType : PaginationViewModel
    {
        public async Task<PageViewModel<EntityViewModelType>> GetPageAsync(PaginationViewModelType model)
        {
            if (model.PageSize is not null && model.PageIndex is null)
                throw new Exception("PageIndex is required if PageSize is initialized");

            if (model.PageIndex < 0)
                throw new Exception("PageIndex less than 0");

            if (model.PageSize < 1)
                throw new Exception("PageSize is invalid");

            var query = GetQuery();

            query = FilterQuery(query, model);

            int count = await query.CountAsync();

            int pagesAvailable;

            if (model.PageSize is not null)
            {
                pagesAvailable = (int)Math.Ceiling(count / (double)model.PageSize);
                query = query
                    .Skip((int)model.PageIndex! * (int)model.PageSize)
                    .Take((int)model.PageSize);
            }
            else
            {
                pagesAvailable = (count > 0) ? (1) : (0);
            }

            var data = await MapAsync(query);

            return new PageViewModel<EntityViewModelType>
            {
                Data = data,
                PageAvailable = pagesAvailable,
                ItemsAvailable = count
            };
        }

        protected abstract IQueryable<EntityType> GetQuery();
        protected abstract IQueryable<EntityType> FilterQuery(IQueryable<EntityType> query, PaginationViewModelType model);
        protected virtual async Task<IEnumerable<EntityViewModelType>> MapAsync(IQueryable<EntityType> query)
        {
            return await query
                .ProjectTo<EntityViewModelType>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}
