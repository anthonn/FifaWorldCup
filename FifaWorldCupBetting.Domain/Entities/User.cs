using Microsoft.AspNetCore.Identity;

namespace FifaWorldCupBetting.Domain.Entities;

public class User : IdentityUser<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpires { get; set; }
    
    // Navigation properties
    public ICollection<UserPrediction> Predictions { get; set; } = new List<UserPrediction>();
    public ICollection<Bet> Bets { get; set; } = new List<Bet>();
}
