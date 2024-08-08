using AutoMapper;
using Steam.Data;
using Steam.Interfaces;
using Steam.Services.ControllerServices.Interfaces;
using Steam.Models.Game;
using Steam.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Steam.Services.ControllerServices
{
    public class GamesControllerService(
        AppEFContext context,
        IMapper mapper,
        IMediaService mediaService
        ) : IGamesControllerService
    {
        public async Task CreateAsync(GameCreateViewModel model)
        {
            var game = mapper.Map<GameEntity>(model);

            try
            {
                game.DateOfRelease = DateTime.UtcNow;
                game.Name = model.Name;
                game.Description = model.Description;
                game.Price = model.Price;
                game.DeveloperId = model.DeveloperId;
                game.Rating = 0;
                game.CommentsCount = 0;

                game.DeveloperId = model.DeveloperId; // only for swager

                game.SystemRequirements = new SystemRequirementsEntity
                {
                    OperatingSystem = model.OperatingSystem,
                    Processor = model.Processor,
                    RAM = model.RAM,
                    VideoCard = model.VideoCard,
                    DiskSpace = model.DiskSpace
                };

                game.Discount = new DiscountEntity
                {
                    DiscountPercentage = model.DiscountPercentage ?? 0,
                    DiscountStartDate = model.DiscountStartDate,
                    DiscountEndDate = model.DiscountEndDate,
                };


                if (model.Media != null && model.Media.Any())
                {
                    var imageUrls = await mediaService.SaveMediaAsync(model.Media);
                    int priority = 1;

                    var mediaEntities = imageUrls.Select(url => new MediaEntity
                    {
                        Name = url,
                        Priority = priority++,
                        Type = url
                    }).ToList();

                    context.Media.AddRange(mediaEntities);
                    await context.SaveChangesAsync();

                    game.GameMedia = mediaEntities.Select(media => new GameMediaEntity
                    {
                        MediaId = media.Id,
                        GameId = game.Id
                    }).ToList();
                }

                if (model.Categories != null && model.Categories.Any())
                {
                    foreach (var categoryId in model.Categories)
                    {
                        game.GameCategories.Add(new GameCategoryEntity
                        {
                            CategoryId = categoryId
                        });
                    }
                }

                context.Games.Add(game);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating the game!", ex);
            }
        }

        public async Task UpdateAsync(GameEditViewModel model)
        {
            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == model.Id);

            try
            {
                if (game is null)
                    throw new Exception("Game not found.");

                if (model.Name != null)
                    game.Name = model.Name;

                if (model.Price != null)
                    game.Price = model.Price;

                if (model.Description != null)
                    game.Description = model.Description;

                //if (model.SystemRequirements != null)
                //    game.SystemRequirements = model.SystemRequirements;

                if (model.ImagesAndVideos.Any() && model.ImagesAndVideos != null)
                {
                    foreach (var image in game.GameMedia)
                        //imageService.DeleteImageIfExists(image.NewsMedia);

                    game.GameMedia.Clear();
                }

                if (model.ImagesAndVideos != null && model.ImagesAndVideos.Any())
                {
                    //int priorityIndex = 1;
                    foreach (var image in model.ImagesAndVideos)
                    {
                        game.GameMedia.Add(new GameMediaEntity
                        {
                            //Name = await imageService.SaveImageAsync(image),
                        });
                    }
                }

                if (model.CategoryIds != null && model.CategoryIds.Any())
                {
                    game.GameCategories.Clear();
                    foreach (var category in model.CategoryIds)
                    {
                        game.GameCategories.Add(new GameCategoryEntity
                        {
                            CategoryId = category
                        });
                    }
                }

                context.Games.Update(game);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error game update!", ex);
            }
        }

        public async Task DeleteIfExistsAsync(int id)
        {
            var game = await context.Games.FirstOrDefaultAsync(i => i.Id == id);

            try
            {
                if (game is null)
                    throw new Exception("Game not found!");

                foreach (var image in game.GameMedia)
                    //imageService.DeleteImageIfExists(image.NewsMedia);

                context.Games.Remove(game);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error game delete!", ex);
            }
        }
    }
}
