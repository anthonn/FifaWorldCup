using Microsoft.AspNetCore.Identity;

namespace FifaWorldCupBetting.Domain.Entities;

public class UserRole : IdentityRole<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
