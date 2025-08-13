namespace FifaWorldCupBetting.Domain.Entities;

public class Match
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public DateTime MatchDate { get; set; }
    public string Stage { get; set; } = string.Empty; // GroupStage, Round16, QuarterFinal, etc.
    public string? GroupLetter { get; set; } // Only for group stage matches
    public int? HomeScore { get; set; } // Actual scores (null until match is played)
    public int? AwayScore { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public Team HomeTeam { get; set; } = null!;
    public Team AwayTeam { get; set; } = null!;
    public ICollection<UserPrediction> Predictions { get; set; } = new List<UserPrediction>();
}
