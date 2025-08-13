using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FifaWorldCupBetting.Domain.Entities;

namespace FifaWorldCupBetting.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<UserPrediction> UserPredictions { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<TournamentStage> TournamentStages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User entity configuration (extends IdentityUser)
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        // UserRole entity configuration (extends IdentityRole)
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Team entity configuration
        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.GroupLetter).IsRequired().HasMaxLength(1);
            entity.Property(e => e.FlagUrl).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Match entity configuration
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Stage).IsRequired().HasMaxLength(50);
            entity.Property(e => e.GroupLetter).HasMaxLength(1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(e => e.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // UserPrediction entity configuration
        modelBuilder.Entity<UserPrediction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.User)
                .WithMany(u => u.Predictions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Match)
                .WithMany(m => m.Predictions)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure one prediction per user per match
            entity.HasIndex(e => new { e.UserId, e.MatchId }).IsUnique();
        });

        // Bet entity configuration
        modelBuilder.Entity<Bet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Stage).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.SelectedWinnerTeam)
                .WithMany(t => t.Bets)
                .HasForeignKey(e => e.SelectedWinnerTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure one bet per user per stage
            entity.HasIndex(e => new { e.UserId, e.Stage }).IsUnique();
        });

        // TournamentStage entity configuration
        modelBuilder.Entity<TournamentStage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}
