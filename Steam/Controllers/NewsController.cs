using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Interfaces;
using Steam.Models.News;

namespace Steam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController(AppEFContext context,
        IValidator<NewsCreateViewModel> createValidator,
        IMapper mapper,
        IImageService imageService) : ControllerBase
    {
        // Переглянути список новин
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var news = await context.News
                    .Select(n => new NewsItemViewModel
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        DateOfRelease = n.DateOfRelease,
                        Image = n.Image,
                        VideoURL = n.VideoURL,
                        GameId = n.GameId
                    }).ToListAsync();   
                
                return Ok(news);
            }
            catch (Exception) 
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Найти новину за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int? id)
        {
            var newsEntity = await context.News
                .Include(n => n.Game)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (newsEntity == null)
            {
                return NotFound();
            }

            var model = new NewsItemViewModel
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Description = newsEntity.Description,
                DateOfRelease = newsEntity.DateOfRelease,
                Image = newsEntity.Image,
                VideoURL = newsEntity.VideoURL,
                GameId = newsEntity.GameId
            };

            return Ok(model);
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
                news.Image = await imageService.SaveImageAsync(model.Image);
                await context.News.AddAsync(news);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(Create), new { id = news.Id}, news);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var newsEntity = await context.News.FindAsync(id);
            if (newsEntity == null)
            {
                return NotFound();
            }

            var model = new NewsEditViewModel
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Description = newsEntity.Description,
                DateOfRelease = newsEntity.DateOfRelease,
                Image = newsEntity.Image,
                VideoURL = newsEntity.VideoURL,
                GameId = newsEntity.GameId
            };

            return Ok(model);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, NewsEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var newsEntity = await context.News.FindAsync(id);
                if (newsEntity == null)
                {
                    return NotFound();
                }

                newsEntity.Title = model.Title;
                newsEntity.Description = model.Description;
                newsEntity.DateOfRelease = model.DateOfRelease;
                newsEntity.Image = model.Image;
                newsEntity.VideoURL = model.VideoURL;
                newsEntity.GameId = model.GameId;

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var newsEntity = await context.News.FindAsync(id);
            if(newsEntity == null)
            {
                return NotFound();
            }

            context.News.Remove(newsEntity);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
