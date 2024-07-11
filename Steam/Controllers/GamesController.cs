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
        IValidator<GameEditViewModel> editValidator,
        IMapper mapper,
        IGamesControllerService service
        ) : ControllerBase
    {
        // Переглянути список ігор
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var list = await context.Games
              .ProjectTo<GameItemViewModel>(mapper.ConfigurationProvider)
              .ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Найти ігру за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var game = await context.Games
               .ProjectTo<GameItemViewModel>(mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(m => m.Id == id);

                if (game is null)
                    return NotFound();

                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GameCreateViewModel model)
        {
            try
            {
                var validatorResult = await createValidator.ValidateAsync(model);

                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.Errors);

                await service.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Edit([FromForm] GameEditViewModel model)
        {
            try
            {
                var validatorResult = await editValidator.ValidateAsync(model);

                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.Errors);

                await service.UpdateAsync(model);

                return Ok(model);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteIfExistsAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
