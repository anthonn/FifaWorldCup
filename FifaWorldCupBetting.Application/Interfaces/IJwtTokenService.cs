using FifaWorldCupBetting.Domain.Entities;

namespace FifaWorldCupBetting.Application.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(User user);
    bool ValidateToken(string token);
    int GetUserIdFromToken(string token);
}
