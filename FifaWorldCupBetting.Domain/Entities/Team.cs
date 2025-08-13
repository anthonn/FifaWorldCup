namespace FifaWorldCupBetting.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GroupLetter { get; set; } = string.Empty;
    public string? FlagUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    public ICollection<UserPrediction> Predictions { get; set; } = new List<UserPrediction>();
    public ICollection<Bet> Bets { get; set; } = new List<Bet>();
}
