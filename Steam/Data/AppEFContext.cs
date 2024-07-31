using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Data.Entities;
using Steam.Data.Entities.Identity;


namespace Steam.Data
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        { }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<CommentsEntity> Comments { get; set; }
        public DbSet<SystemRequirementsEntity> SystemRequirements { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<GameCategoryEntity> GameCategory { get; set; }
        public DbSet<GameVideoEntity> GameVideos { get; set; }
        public DbSet<GameImageEntity> GameImages { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<UserGameEntity> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameEntity>(entity =>
            {
                entity.ToTable("tblGame");
                entity.HasOne(g => g.Developer)
                    .WithMany(u => u.Games)
                    .HasForeignKey(g => g.DeveloperId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(g => g.Comments)
                      .WithOne(c => c.Game)
                      .HasForeignKey(c => c.GameId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(g => g.SystemRequirements)
                    .WithOne(sr => sr.Game)
                    .HasForeignKey<SystemRequirementsEntity>(sr => sr.GameId);
            });


            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("tblUser");
                entity.HasMany(u => u.News)
                    .WithOne(n => n.UserOrDeveloper)
                    .HasForeignKey(n => n.UserOrDeveloperId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GameCategoryEntity>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });

            modelBuilder.Entity<GameCategoryEntity>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.GameId);

            modelBuilder.Entity<GameCategoryEntity>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(gc => gc.CategoryId);

            modelBuilder.Entity<NewsEntity>(entity =>
            {
                entity.ToTable("tblNews");
                entity.HasOne(n => n.UserOrDeveloper)
                      .WithMany(d => d.News)
                      .HasForeignKey(n => n.UserOrDeveloperId);
                entity.HasOne(n => n.Game)
                      .WithMany(g => g.News)
                      .HasForeignKey(n => n.GameId);
                entity.HasMany(n => n.Comments)
                      .WithOne(c => c.News)
                      .HasForeignKey(c => c.NewsId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserGameEntity>()
           .HasKey(ug => new { ug.UserId, ug.GameId });

            modelBuilder.Entity<UserGameEntity>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGames)
                .HasForeignKey(ug => ug.GameId);
        }

    }
}
