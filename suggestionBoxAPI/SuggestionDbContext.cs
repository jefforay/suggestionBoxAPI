using Microsoft.EntityFrameworkCore;
using SuggestionAPI.Domain;

namespace SuggestionAPI.Repositories;

public sealed partial class SuggestionDbContext : DbContext
{
    public SuggestionDbContext(DbContextOptions<SuggestionDbContext> options)
        : base(options)
    {
    }
    public DbSet<Suggestion> Suggestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.ToTable("Suggestions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DateTimeStart).HasColumnType("datetime");
            entity.Property(e => e.DateTimeEnd).HasColumnType("datetime");
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.EventType).HasMaxLength(255);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}