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
        public DbSet<DiscountEntity> Discount { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<GameCommentEntity> GameComments { get; set; }
        public DbSet<NewsCommentEntity> NewsComments { get; set; }
        public DbSet<SystemRequirementsEntity> SystemRequirements { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<GameCategoryEntity> GameCategory { get; set; }
        public DbSet<NewsMediaEntity> NewsMedia { get; set; }
        public DbSet<GameMediaEntity> GameMedia { get; set; }
        public DbSet<MediaEntity> Media { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<UserEntity> Users {  get; set; }
        public DbSet<UserGameEntity> UserGames { get; set; }
        public DbSet<DeveloperGameEntity> DeveloperGames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Comment builder
            modelBuilder.Entity<CommentEntity>()
                .ToTable("tblComment")
                .HasMany(c => c.GameComments)
                .WithOne(gc => gc.Comment)
                .HasForeignKey(c => c.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NewsEntity>()
                .HasMany(c => c.NewsComments)
                .WithOne(gc => gc.News)
                .HasForeignKey(c => c.NewsId)
                .OnDelete(DeleteBehavior.Cascade);

            // GameComment builder
            modelBuilder.Entity<GameCommentEntity>()
                .HasKey(c => new { c.GameId, c.CommentId });

            modelBuilder.Entity<GameCommentEntity>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameComments)
                .HasForeignKey(gc => gc.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameCommentEntity>()
               .HasOne(gc => gc.Comment)
               .WithMany(g => g.GameComments)
               .HasForeignKey(gc => gc.CommentId);

            // NewsComment builder
            modelBuilder.Entity<NewsCommentEntity>()
                .HasKey(nc => new { nc.NewsId, nc.CommentId });

            modelBuilder.Entity<NewsCommentEntity>()
                .HasOne(nc => nc.News)
                .WithMany(n => n.NewsComments)
                .HasForeignKey(nc => nc.NewsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NewsCommentEntity>()
                .HasOne(nc => nc.Comment)
                .WithMany(c => c.NewsComments)
                .HasForeignKey(nc => nc.CommentId);

            // Media builder
            modelBuilder.Entity<MediaEntity>()
                .HasMany(m => m.GameMedia)
                .WithOne(gm => gm.Media)
                .HasForeignKey(gm => gm.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MediaEntity>()
                .HasMany(m => m.NewsMedia)
                .WithOne(nm => nm.Media)
                .HasForeignKey(nm => nm.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            // GameMedia builder
            modelBuilder.Entity<GameMediaEntity>()
                .HasKey(gm => new { gm.GameId, gm.MediaId });

            modelBuilder.Entity<GameMediaEntity>()
                .HasOne(gm => gm.Game)
                .WithMany(g => g.GameMedia)
                .HasForeignKey(gm => gm.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameMediaEntity>()
                .HasOne(gm => gm.Media)
                .WithMany(g => g.GameMedia)
                .HasForeignKey(gm => gm.MediaId);

            // NewsMedia builder
            modelBuilder.Entity<NewsMediaEntity>()
                .HasKey(nm => new { nm.NewsId, nm.MediaId });

            modelBuilder.Entity<NewsMediaEntity>()
                .HasOne(nm => nm.News)
                .WithMany(n => n.NewsMedia)
                .HasForeignKey(nm => nm.NewsId);

            modelBuilder.Entity<NewsMediaEntity>()
                .HasOne(nm => nm.Media)
                .WithMany(m => m.NewsMedia)
                .HasForeignKey(nm => nm.MediaId);

            // Game with News builder
            modelBuilder.Entity<GameEntity>()
                .HasMany(g => g.News)
                .WithOne(n => n.Game)
                .HasForeignKey(n => n.GameId)
                .OnDelete(DeleteBehavior.SetNull);

            // News with User builder
            modelBuilder.Entity<NewsEntity>()
                .HasOne(n => n.UserOrDeveloper)
                .WithMany(u => u.News)
                .OnDelete(DeleteBehavior.SetNull);

            // User with News builder
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.News)
                .WithOne(n => n.UserOrDeveloper)
                .OnDelete(DeleteBehavior.SetNull);

            // Discount builder
            modelBuilder.Entity<DiscountEntity>()
                .HasOne(d => d.Game)
                .WithOne(g => g.Discount)
                .HasForeignKey<DiscountEntity>(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // SystemRequirements builder
            modelBuilder.Entity<SystemRequirementsEntity>()
                .HasOne(sr => sr.Game)
                .WithOne(g => g.SystemRequirements)
                .HasForeignKey<SystemRequirementsEntity>(sr => sr.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // GameCategory builder
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

            // DeveloperGame builder
            modelBuilder.Entity<DeveloperGameEntity>()
                .HasKey(d => new { d.DeveloperId, d.GameId });

            modelBuilder.Entity<DeveloperGameEntity>()
                .HasOne(d => d.Developer)
                .WithMany(u => u.DevelopedGames)
                .HasForeignKey(d => d.DeveloperId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeveloperGameEntity>()
                .HasOne(d => d.Game)
                .WithMany()
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // User builder
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("tblUser");

                entity.HasMany(e => e.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.UserGames)
                    .WithOne(ug => ug.User)
                    .HasForeignKey(ug => ug.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.News)
                    .WithOne(n => n.UserOrDeveloper) 
                    .HasForeignKey(n => n.UserOrDeveloperId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.DevelopedGames)
                    .WithOne(dg => dg.Developer)
                    .HasForeignKey(dg => dg.DeveloperId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserRole builder
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


            // RoleEntity builder
            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId);
            });

            // UserGames builder
            modelBuilder.Entity<UserGameEntity>()
           .HasKey(ug => new { ug.UserId, ug.GameId });

            modelBuilder.Entity<UserGameEntity>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGames)
                .HasForeignKey(ug => ug.GameId);

            modelBuilder.Entity<UserGameEntity>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGames)
                .HasForeignKey(ug => ug.UserId);
        }

    }
}
