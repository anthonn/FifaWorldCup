using FifaWorldCupBetting.Application.DTOs.Auth;
using FifaWorldCupBetting.Application.DTOs.User;

namespace FifaWorldCupBetting.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task<UserDto> RegisterAsync(RegisterRequestDto registerRequest);
    Task<bool> RequestPasswordResetAsync(ForgotPasswordRequestDto request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
    Task<UserDto?> GetCurrentUserAsync(int userId);
}
