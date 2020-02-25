namespace CinemaHour.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Models;
    using CinemaHour.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    /*
        Actor, Comment, Director, Genre, Movie, Producer MODELS FOR NOW
     */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<MovieDirector> MovieDirectors { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<MovieActors> MovieActors { get; set; }

        public DbSet<MovieComment> MovieComments { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserFavourite> Favourites { get; set; }

        public DbSet<UserWatched> Watched { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // User Movie (Watched) Relation
            builder.Entity<UserWatched>(entity =>
            {
                entity.HasKey(u => new { u.ApplicationUserId, u.MovieId });

                entity
                    .HasOne(u => u.User)
                    .WithMany(m => m.Watched)
                    .HasForeignKey(u => u.ApplicationUserId);
            });

            // User Movie (Favourites) Relation
            builder.Entity<UserFavourite>(entity =>
            {
                entity.HasKey(u => new { u.ApplicationUserId, u.MovieId });

                entity
                    .HasOne(u => u.User)
                    .WithMany(m => m.Favourites)
                    .HasForeignKey(u => u.ApplicationUserId);
            });

            // Movie Director Relation
            builder.Entity<MovieDirector>(entity =>
            {
                entity.HasKey(m => new { m.MovieId, m.DirectorId });

                entity
                    .HasOne(d => d.Director)
                    .WithMany(m => m.Movies)
                    .HasForeignKey(d => d.DirectorId);
            });

            // Movie Actors Relation
            builder.Entity<MovieActors>(entity =>
            {
                entity.HasKey(k => new { k.ActorId, k.MovieId });

                entity
                    .HasOne(m => m.Movie)
                    .WithMany(a => a.Actors)
                    .HasForeignKey(m => m.MovieId);

                entity
                    .HasOne(a => a.Actor)
                    .WithMany(m => m.Movies)
                    .HasForeignKey(a => a.ActorId);
            });

            // Movie Comments Relation
            builder.Entity<MovieComment>(entity =>
            {
                entity.HasKey(m => new { m.MovieId, m.CommentId });

                entity
                    .HasOne(m => m.Movie)
                    .WithMany(c => c.Comments)
                    .HasForeignKey(m => m.MovieId);
            });

            // Movie Genre Relation
            builder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(x => new { x.MovieId, x.GenreId });

                entity
                    .HasOne(g => g.Genre)
                    .WithMany(m => m.Movies)
                    .HasForeignKey(g => g.GenreId);

                entity
                    .HasOne(m => m.Movie)
                    .WithMany(g => g.Genres)
                    .HasForeignKey(m => m.MovieId);
            });

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
