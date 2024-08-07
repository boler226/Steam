using Steam.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using AutoMapper;
using Steam.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Steam.Data.Entities.Identity;
using Steam.Interfaces;
using Steam.Services;
using FluentValidation;
using Steam.Services.ControllerServices.Interfaces;
using Steam.Services.ControllerServices;
using Steam.Models.News;
using Steam.Services.PaginationServices;
using Steam.Models.Game;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppEFContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppEFContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSecretKey")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register the logging services
builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
    config.AddDebug();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 1048576000; // if don't set default value is: 1000 MB 
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AppMapProfile(provider.GetService<AppEFContext>()));
}).CreateMapper());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IMediaService, MediaService>();
builder.Services.AddTransient<IMediaValidator, MediaValidator>();
builder.Services.AddTransient<IVideoValidator, VideoValidator>();
builder.Services.AddTransient<IImageValidator, ImageValidator>();


builder.Services.AddTransient<IAccountsControllerService, AccountsControllerService>();

builder.Services.AddTransient<INewsControllerService, NewsControllerService>();

builder.Services.AddTransient<IGamesControllerService, GamesControllerService>();

builder.Services.AddTransient<IExistingEntityCheckerService, ExistingEntityCheckerService>();
builder.Services.AddTransient<IPaginationService<NewsItemViewModel, NewsFilterViewModel>, NewsPaginationService>();
builder.Services.AddTransient<IPaginationService<GameItemViewModel, GameFilterViewModel>, GamesPaginationService>();


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var imageDir = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["ImagesDir"]);

if (!Directory.Exists(imageDir))
{
    Directory.CreateDirectory(imageDir);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageDir),
    RequestPath = "/images"
});

var videoDir = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["VideosDir"]);

if (!Directory.Exists(videoDir))
{
    Directory.CreateDirectory(videoDir);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(videoDir),
    RequestPath = "/videos"
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedData();

app.Run();
