using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Interfaces;
using Steam.Models.News;
using Steam.Services.ControllerServices.Interfaces;

namespace Steam.Services.ControllerServices
{
    public class NewsControllerService(
        AppEFContext context,
        IMapper mapper,
        IImageService imageService
        ) : INewsControllerService
    {
        public async Task CreateAsync(NewsCreateViewModel model)
        {
            var news = mapper.Map<NewsEntity>(model);

            try
            {
                news.Image = await imageService.SaveImageAsync(model.Image);
                news.DateOfRelease = DateTime.UtcNow;
                await context.News.AddAsync(news);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                imageService.DeleteImageIfExists(news.Image);
                throw new Exception("Error news created!");
            }
        }
        
        public async Task UpdateAsync(NewsEditViewModel model)
        {
            var news = await context.News.FirstOrDefaultAsync(n => n.Id == model.Id);

            try
            {
                if (news == null)
                    throw new Exception("News not found");

                var oldImage = news.Image;

                if(model.Title != null)
                    news.Title = model.Title;

                if (model.Description != null)
                    news.Description = model.Description;

                if(model.Image != null)
                {
                    news.Image = await imageService.SaveImageAsync(model.Image);
                    imageService.DeleteImageIfExists(oldImage);
                }

                if(model.VideoURL != null)
                    news.VideoURL = model.VideoURL;

                if(model.GameId != null)
                    news.GameId = model.GameId;


                context.News.Update(news);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                imageService.DeleteImageIfExists(news.Image);
                throw new Exception("Error news update!");
            }
        }

        public async Task DeleteIfExistsAsync(int id)
        {
            var news = await context.News.FirstOrDefaultAsync(n => n.Id == id);
            
            try
            {
                if (news == null)
                    throw new Exception("News not found!");

                imageService.DeleteImageIfExists(news.Image);
                context.News.Remove(news);
                await context.SaveChangesAsync();
            } 
            catch(Exception)
            {
                throw new Exception("Error news delete!");
            }
        }
    }
}
