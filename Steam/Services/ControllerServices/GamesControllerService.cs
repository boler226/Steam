﻿using AutoMapper;
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
        IImageService imageService
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
                game.SystemRequirements = model.SystemRequirements;

                if(model.Images != null && model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        game.GameImages.Add(new GameImageEntity
                        {
                            Name = await imageService.SaveImageAsync(image),
                        });
                    }
                }

                if (model.CategoryIds != null && model.CategoryIds.Any())
                {
                    foreach (var categoryId in model.CategoryIds)
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
                throw new Exception("Error while creating the game", ex);
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

                if (model.SystemRequirements != null)
                    game.SystemRequirements = model.SystemRequirements;

                if (model.Images.Any() && model.Images != null)
                {
                    foreach (var image in game.GameImages)
                        imageService.DeleteImageIfExists(image.Name);

                    game.GameImages.Clear();
                }

                if (model.Images != null && model.Images.Any())
                {
                    //int priorityIndex = 1;
                    foreach (var image in model.Images)
                    {
                        game.GameImages.Add(new GameImageEntity {
                            Name = await imageService.SaveImageAsync(image),
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
            catch (Exception)
            {
                
                throw new Exception("Error news update!");
            }
        }
    }
}
