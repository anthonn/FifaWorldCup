namespace FifaWorldCupBetting.Domain.Entities;

public class UserPrediction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public int PredictedHomeScore { get; set; }
    public int PredictedAwayScore { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Match Match { get; set; } = null!;
}
