namespace FifaWorldCupBetting.Domain.Entities;

public class Bet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Stage { get; set; } = string.Empty; // Winner, RunnerUp, TopScorer, etc.
    public int SelectedWinnerTeamId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Team SelectedWinnerTeam { get; set; } = null!;
}
