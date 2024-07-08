using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.Category;
using Steam.Models.Game;
using Steam.Models.News;
using Steam.Services.ControllerServices.Interfaces;

namespace Steam.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GamesController(AppEFContext context,
        IValidator<GameCreateViewModel> createValidator,
        IMapper mapper,
        IGamesControllerService service
        ) : ControllerBase
    {
        // Переглянути список ігор
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await context.Games
                .ProjectTo<GameItemViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(list);
        }

        // Найти ігру за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var game = await context.Games
                .ProjectTo<GameItemViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GameCreateViewModel model)
        {
            var validatorResult = await createValidator.ValidateAsync(model);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            await service.CreateAsync(model);
            return Ok();
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var gameEntity = await context.Games
                .Include(g => g.GameCategories)
                .Include(g => g.GameImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(gameEntity == null)
            {
                return NotFound();
            }

            var model = mapper.Map<GameEditViewModel>(gameEntity);
            model.SelectedCategoryIds = gameEntity.GameCategories.Select(gc => gc.CategoryId).ToList();
            model.ImageUrls = gameEntity.GameImages.Select(gi => gi.Name).ToList();     

            return Ok(model);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, GameEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gameEntity = await context.Games
                        .Include(g => g.GameCategories)
                        .Include(g => g.GameImages)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if(gameEntity == null)
                    {
                        return NotFound();
                    }

                    mapper.Map(model, gameEntity);

                    // Оновлення категорій 
                    var existingCategories = gameEntity.GameCategories.ToList();
                    context.GameCategory.RemoveRange(existingCategories);

                   if(model.SelectedCategoryIds != null)
                   {
                        foreach(var categoryId in model.SelectedCategoryIds)
                        {
                            context.GameCategory.Add(new GameCategoryEntity
                            {
                                GameId = gameEntity.Id,
                                CategoryId = categoryId
                            });
                        }
                   }

                    // Оновлення фото
                    var existingImages = gameEntity.GameImages.ToList();
                    context.GameImages.RemoveRange(existingImages);

                    if(model.ImageUrls != null)
                    {
                        foreach (var imageUrl in model.ImageUrls)
                        {
                            context.GameImages.Add(new GameImageEntity
                            {
                                GameId = gameEntity.Id,
                                Name = imageUrl
                            });
                        }
                    }

                    context.Update(gameEntity);
                    await context.SaveChangesAsync();
                }
                // Виняток, викликаний DbContext , коли очікувалося, що SaveChanges для
                // сутності призведе до оновлення бази даних, але насправді рядки в базі
                // даних не змінилися
                catch (DbUpdateConcurrencyException)
                {
                    if(!GameExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Ok(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var gameEntity = await context.Games.FirstOrDefaultAsync();
            if(gameEntity == null)
            {
                return NotFound();
            }

            context.Games.Remove(gameEntity);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return context.Games.Any(e => e.Id == id);
        }
    }
}
