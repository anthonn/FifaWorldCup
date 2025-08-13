using Microsoft.EntityFrameworkCore;
using FifaWorldCupBetting.Domain.Entities;
using FifaWorldCupBetting.Infrastructure.Data;

namespace FifaWorldCupBetting.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.TournamentStages.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Seed Tournament Stages
        var stages = new List<TournamentStage>
        {
            new() { Name = "GroupStage", Order = 1, IsActive = true, CreatedAt = DateTime.UtcNow },
            new() { Name = "Round16", Order = 2, IsActive = false, CreatedAt = DateTime.UtcNow },
            new() { Name = "QuarterFinal", Order = 3, IsActive = false, CreatedAt = DateTime.UtcNow },
            new() { Name = "SemiFinal", Order = 4, IsActive = false, CreatedAt = DateTime.UtcNow },
            new() { Name = "Final", Order = 5, IsActive = false, CreatedAt = DateTime.UtcNow }
        };

        context.TournamentStages.AddRange(stages);

        // Seed Teams (example for FIFA World Cup)
        var teams = new List<Team>
        {
            // Group A
            new() { Name = "Qatar", GroupLetter = "A", FlagUrl = "/flags/qatar.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Ecuador", GroupLetter = "A", FlagUrl = "/flags/ecuador.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Senegal", GroupLetter = "A", FlagUrl = "/flags/senegal.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Netherlands", GroupLetter = "A", FlagUrl = "/flags/netherlands.png", CreatedAt = DateTime.UtcNow },

            // Group B
            new() { Name = "England", GroupLetter = "B", FlagUrl = "/flags/england.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Iran", GroupLetter = "B", FlagUrl = "/flags/iran.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "USA", GroupLetter = "B", FlagUrl = "/flags/usa.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Wales", GroupLetter = "B", FlagUrl = "/flags/wales.png", CreatedAt = DateTime.UtcNow },

            // Group C
            new() { Name = "Argentina", GroupLetter = "C", FlagUrl = "/flags/argentina.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Saudi Arabia", GroupLetter = "C", FlagUrl = "/flags/saudi-arabia.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Mexico", GroupLetter = "C", FlagUrl = "/flags/mexico.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Poland", GroupLetter = "C", FlagUrl = "/flags/poland.png", CreatedAt = DateTime.UtcNow },

            // Group D
            new() { Name = "France", GroupLetter = "D", FlagUrl = "/flags/france.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Australia", GroupLetter = "D", FlagUrl = "/flags/australia.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Denmark", GroupLetter = "D", FlagUrl = "/flags/denmark.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Tunisia", GroupLetter = "D", FlagUrl = "/flags/tunisia.png", CreatedAt = DateTime.UtcNow },

            // Group E
            new() { Name = "Spain", GroupLetter = "E", FlagUrl = "/flags/spain.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Costa Rica", GroupLetter = "E", FlagUrl = "/flags/costa-rica.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Germany", GroupLetter = "E", FlagUrl = "/flags/germany.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Japan", GroupLetter = "E", FlagUrl = "/flags/japan.png", CreatedAt = DateTime.UtcNow },

            // Group F
            new() { Name = "Belgium", GroupLetter = "F", FlagUrl = "/flags/belgium.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Canada", GroupLetter = "F", FlagUrl = "/flags/canada.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Morocco", GroupLetter = "F", FlagUrl = "/flags/morocco.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Croatia", GroupLetter = "F", FlagUrl = "/flags/croatia.png", CreatedAt = DateTime.UtcNow },

            // Group G
            new() { Name = "Brazil", GroupLetter = "G", FlagUrl = "/flags/brazil.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Serbia", GroupLetter = "G", FlagUrl = "/flags/serbia.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Switzerland", GroupLetter = "G", FlagUrl = "/flags/switzerland.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Cameroon", GroupLetter = "G", FlagUrl = "/flags/cameroon.png", CreatedAt = DateTime.UtcNow },

            // Group H
            new() { Name = "Portugal", GroupLetter = "H", FlagUrl = "/flags/portugal.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Ghana", GroupLetter = "H", FlagUrl = "/flags/ghana.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "Uruguay", GroupLetter = "H", FlagUrl = "/flags/uruguay.png", CreatedAt = DateTime.UtcNow },
            new() { Name = "South Korea", GroupLetter = "H", FlagUrl = "/flags/south-korea.png", CreatedAt = DateTime.UtcNow }
        };

        context.Teams.AddRange(teams);
        await context.SaveChangesAsync();
    }
}
