using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonalTreker.DataAccess.Entities;

namespace PersonalTreker.DataAccess
{
    public partial class IssueBoardContext : DbContext
    {
        public DbSet<IssueEntity> Issue { get; set; }
        public DbSet<BoardEntity> Board { get; set; }
        
        public IssueBoardContext() { }
        
        public IssueBoardContext(DbContextOptions<IssueBoardContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueEntity>(entity =>
            {
                entity.Property(e => e.Id).
                    UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Status).HasDefaultValue(false);

                entity.HasOne(b => b.Board)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(b => b.BoardId);

                entity.Property(e => e.Description).HasMaxLength(255);

            });

            modelBuilder.Entity<BoardEntity>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Title).IsRequired();
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}