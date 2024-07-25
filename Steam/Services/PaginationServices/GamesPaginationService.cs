using AutoMapper;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.Game;
using Steam.Services.PaginationServices.Base;

namespace Steam.Services.PaginationServices
{
    public class GamesPaginationService(
        AppEFContext context,
        IMapper mapper
        ) : PaginationService<GameEntity, GameItemViewModel, GameFilterViewModel>(mapper)
    {
        protected override IQueryable<GameEntity> GetQuery() => context.Games.OrderBy(n => n.Id);

        protected override IQueryable<GameEntity> FilterQuery(IQueryable<GameEntity> query, GameFilterViewModel paginationVm)
        {
            return query.OrderByDescending(n => n.Id);
        }
    }
}
