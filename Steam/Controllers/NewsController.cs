using AutoMapper;
using FluentValidation;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Interfaces;
using Steam.Models.News;
using Steam.Services.ControllerServices.Interfaces;

namespace Steam.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController(AppEFContext context,
        IValidator<NewsCreateViewModel> createValidator,
        IValidator<NewsEditViewModel> editValidator,
        IMapper mapper,
        IImageService imageService,
        INewsControllerService service,
        IConfiguration configuration,
        IPaginationService<NewsItemViewModel, NewsFilterViewModel> pagination
        ) : ControllerBase
    {
        // Переглянути список новин
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var list = await context.News
                    .ProjectTo<NewsItemViewModel>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] NewsFilterViewModel model)
        {
            try
            {
                return Ok(await pagination.GetPageAsync(model));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Найти новину за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var news = await context.News
                    .ProjectTo<NewsItemViewModel>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (news is null)
                    return NotFound();

                return Ok(news);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsCreateViewModel model)
        {
            var validationResult = await createValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var news = mapper.Map<NewsEntity>(model);

            try
            {
                //news.NewsMedia = await imageService.SaveImageAsync(model.ImageOrVideo);
                await service.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Edit([FromForm] NewsEditViewModel model)
        {
            var validationResult = await editValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                await service.UpdateAsync(model);
                return Ok();
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
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
