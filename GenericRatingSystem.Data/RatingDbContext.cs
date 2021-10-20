using GenericRatingSystem.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GenericRatingSystem.Data
{
    public partial class RatingDbContext : DbContext
    {
        public RatingDbContext(DbContextOptions<RatingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RatingPerGroup> RatingPerGroups { get; set; }
        public virtual DbSet<UserRating> UserRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<RatingPerGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("RatingPerGroup");

                entity.Property(e => e.GroupId).HasMaxLength(50);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RatingAvg).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.HasKey(e => new { e.UserEmail, e.ExternalId });
                entity.ToTable("UserRatings");


                entity.Property(e => e.UserEmail).HasMaxLength(50);

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.External)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.ExternalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersRatings_RatingPerGroup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
