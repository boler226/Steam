using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Constants;
using Steam.Data.Entities;
using Steam.Data.Entities.Identity;
using Steam.Interfaces;
using System.Net.Http;
using System.Runtime.CompilerServices;



namespace Steam.Data
{
    public static class SeederDB
    {
        public static async Task SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;

                //Отримую посилання на наш контекст
                var context = service.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                var imageService = service.GetRequiredService<IImageService>();

                var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();

                var httpClient = new HttpClient();

                // Apply any pending migrations
                context.Database.Migrate();

                #region Додавання користувачів та ролей

                if (!context.Roles.Any())
                {
                    foreach (var role in Roles.All)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity
                        {
                            Name = role
                        }).Result;
                    }
                }

                if (!context.Users.Any())
                {
                    var user = new UserEntity
                    {
                        Email = "admin@gmail.com",
                        UserName = "SteamAdmin",
                        Photo = "admin",
                    };
                    var result = userManager.CreateAsync(user, "123456").Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, "Admin").Result;
                    }
                }
                #endregion

                #region Додавання категорій

                //if (context.Categories.Count() < 10)
                //{
                //    var gameCategories = new[]
                //    {
                //            "Action", "Adventure", "RPG", "Strategy", "Shooter",
                //            "Puzzle", "Racing", "Simulation", "Sports", "Fighting"
                //        };

                //    foreach (var categoryName in gameCategories)
                //    {
                //        var category = new CategoryEntity
                //        {
                //            Name = categoryName
                //        };
                //        context.Categories.Add(category);
                //    }
                //    context.SaveChanges();
                //}

                //if (context.Games.Count() < 10)
                //{
                //    var categoriesId = context.Categories.Select(c => c.Id).ToList();

                //    var fakeGame = new Faker<GameEntity>("uk")
                //        .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                //        .RuleFor(o => o.Price, f => Math.Round(f.Random.Decimal(10, 100), 2))
                //        .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                //        .RuleFor(o => o.DateOfRelease, f => f.Date.PastOffset(1).UtcDateTime)
                //        /*.RuleFor(o => o.SystemRequirements, f => f.Lorem.Sentence())*/;

                //    var games = fakeGame.Generate(10);

                //    foreach (var game in games)
                //    {
                //        context.Games.Add(game);
                //    }
                //    context.SaveChanges();

                //    // Додавання зв'язків між іграми та категоріями
                //    foreach (var game in games)
                //    {
                //        foreach (var categoryId in categoriesId)
                //        {
                //            context.GameCategory.Add(new GameCategoryEntity
                //            {
                //                GameId = game.Id,
                //                CategoryId = categoryId
                //            });
                //        }
                //    }
                //    context.SaveChanges();
                //}

                #endregion

                #region Додавання ігор і видалення старих
                //if (context.Games.Any())
                //{
                //    // Remove game images
                //    context.GameImages.RemoveRange(context.GameImages);
                //    // Remove games
                //    context.Games.RemoveRange(context.Games);
                //    context.SaveChanges();

                //    // Clear the change tracker to avoid any issues with cached entities
                //    context.ChangeTracker.Clear();
                //}

                //// Seed new games if none exist
                //if (!context.Games.Any())
                //{
                //    logger.LogInformation("Generating body of GameEntity...");
                //    var fakeGames = new Faker<GameEntity>("uk")
                //        .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                //        .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
                //        .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                //        .RuleFor(o => o.DateOfRelease, f => f.Date.Past(2).ToUniversalTime()) // Convert to UTC
                //        .RuleFor(o => o.SystemRequirements, f => f.Lorem.Paragraphs(2));

                //    var gamesList = fakeGames.Generate(10);
                //    logger.LogInformation("Generating photo for GameEntity. It may take some time, please wait...");
                //    foreach (var game in gamesList)
                //    {
                //        var imageUrls = new List<byte[]>();

                //        for (int i = 0; i < 5; i++)
                //        {
                //            var imageUrl = $"https://picsum.photos/2000?random={Guid.NewGuid()}";
                //            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                //            imageUrls.Add(imageBytes);
                //        }

                //        var imageNames = await imageService.SaveImagesAsync(imageUrls);
                //        foreach (var imageName in imageNames)
                //        {
                //            var gameImage = new GameImageEntity
                //            {
                //                Name = imageName
                //            };
                //            game.GameImages.Add(gameImage);
                //        }
                //    }

                //    // Add new games to the context
                //    context.Games.AddRange(gamesList);
                //    context.SaveChanges();
                //}

                #endregion

                #region Додавання новин і видалення старих


                //if (context.News.Any())
                //{
                //    context.News.RemoveRange(context.News);
                //    context.SaveChanges();

                //    // Clear the change tracker to avoid any issues with cached entities
                //    context.ChangeTracker.Clear();
                //}

                //// Seed new news if none exist
                //if (!context.News.Any())
                //{
                //    logger.LogInformation("Generating body of NewsEntity...");
                //    var gamesId = await context.Games.Select(g => g.Id).ToListAsync();

                //    if (gamesId.Count == 0)
                //    {
                //        logger.LogWarning("No games available to associate with news.");
                //        return;
                //    }

                //    var fakeNews = new Faker<NewsEntity>("uk")
                //        .RuleFor(o => o.Title, f => f.Lorem.Sentence())
                //        .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                //        .RuleFor(o => o.DateOfRelease, f => f.Date.Past(1).ToUniversalTime()) // Convert to UTC
                //        .RuleFor(o => o.VideoURL, f => $"https://videos.com/{f.Random.Guid()}.mp4");

                //    var newsList = fakeNews.Generate(10);
                //    logger.LogInformation("Generating photos for NewsEntity. It may take some time, please wait...");

                //    foreach (var news in newsList)
                //    {
                //        var imageUrl = $"https://picsum.photos/2000?random={Guid.NewGuid()}";
                //        var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                //        var imageName = await imageService.SaveImageAsync(imageBytes);

                //        news.Image = imageName;
                //        news.GameId = gamesId[new Random().Next(gamesId.Count)];
                //    }

                //    // Add new news to the context
                //    await context.News.AddRangeAsync(newsList);
                //    await context.SaveChangesAsync();
                //}

                #endregion

                #region Додавання користувачів та їхніх ігор

                //if (context.UserGames.Count() < 3)
                //{
                //    var users = context.Users.ToList();
                //    var games = context.Games.ToList();

                //    foreach (var someUser in users)
                //    {
                //        var userGames = new Faker<UserGameEntity>("uk")
                //            .RuleFor(o => o.UserId, someUser.Id)
                //            .RuleFor(o => o.GameId, f => games[f.Random.Number(0, games.Count - 1)].Id)
                //            .Generate(2);

                //        context.UserGames.AddRange(userGames);
                //    }
                //    context.SaveChanges();
                //}

                #endregion
            }




        }
    }
}
