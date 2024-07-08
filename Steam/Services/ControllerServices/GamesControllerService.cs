using AutoMapper;
using Steam.Data;
using Steam.Interfaces;
using Steam.Services.ControllerServices.Interfaces;
using Steam.Models.Game;
using Steam.Data.Entities;

namespace Steam.Services.ControllerServices
{
    public class GamesControllerService(
        AppEFContext context,
        IMapper mapper,
        IImageService imageService
        ) : IGamesControllerService
    {
        public async Task CreateAsync(GameCreateViewModel model)
        {
            var game = mapper.Map<GameEntity>(model);

            try
            {
                var gameImages = await imageService.SaveImagesAsync(model.ImageUrls);
                game.GameImages = gameImages.Select(name => new GameImageEntity { Name = name }).ToList();

                game.GameCategories = model.SelectedCategoryIds.Select(id => new GameCategoryEntity { CategoryId = id }).ToList();

                context.Games.Add(game);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating the game", ex);
            }
        }
    }
}
