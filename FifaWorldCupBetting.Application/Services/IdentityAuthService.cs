using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using FifaWorldCupBetting.Application.DTOs.Auth;
using FifaWorldCupBetting.Application.DTOs.User;
using FifaWorldCupBetting.Application.Interfaces;
using FifaWorldCupBetting.Domain.Entities;

namespace FifaWorldCupBetting.Application.Services;

public class IdentityAuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;
    private readonly ILogger<IdentityAuthService> _logger;

    public IdentityAuthService(
        UserManager<User> userManager,
        IJwtTokenService jwtTokenService,
        IEmailService emailService,
        ILogger<IdentityAuthService> logger)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        try
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed - user not found: {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning("Login failed - inactive user: {Email}", request.Email);
                throw new UnauthorizedAccessException("Account is deactivated. Please contact support.");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            
            if (!result)
            {
                _logger.LogWarning("Login failed for {Email}: Invalid password", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = await _jwtTokenService.GenerateTokenAsync(user);
            
            _logger.LogInformation("Successful login for user: {Email}", request.Email);
            
            return new LoginResponseDto
            {
                Token = token,
                Username = user.UserName!,
                Email = user.Email!,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Should match JWT expiration
            };
        }
        catch (UnauthorizedAccessException)
        {
            throw; // Re-throw authorization exceptions
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
            throw new Exception("An error occurred during login. Please try again.");
        }
    }

    public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
    {
        try
        {
            _logger.LogInformation("Registration attempt for email: {Email}", request.Email);

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed - email already exists: {Email}", request.Email);
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // Check if username already exists
            var existingUsername = await _userManager.FindByNameAsync(request.Username);
            if (existingUsername != null)
            {
                _logger.LogWarning("Registration failed - username already exists: {Username}", request.Username);
                throw new InvalidOperationException("A user with this username already exists.");
            }

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Registration failed for {Email}: {Errors}", request.Email, errors);
                throw new InvalidOperationException($"Registration failed: {errors}");
            }
            
            _logger.LogInformation("Successful registration for user: {Email}", request.Email);
            
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
        catch (InvalidOperationException)
        {
            throw; // Re-throw validation exceptions
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email: {Email}", request.Email);
            throw new Exception("An error occurred during registration. Please try again.");
        }
    }

    public async Task<bool> RequestPasswordResetAsync(ForgotPasswordRequestDto request)
    {
        try
        {
            _logger.LogInformation("Forgot password request for email: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user doesn't exist - return true for security
                _logger.LogInformation("Forgot password request for non-existent email: {Email}", request.Email);
                return true;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            // Update user with reset token (for our custom tracking)
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);
            await _userManager.UpdateAsync(user);

            var resetUrl = $"{request.ResetUrl}?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(request.Email)}";
            
            await _emailService.SendPasswordResetEmailAsync(user.Email!, resetUrl, user.UserName!);
            
            _logger.LogInformation("Password reset email sent for user: {Email}", request.Email);
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during forgot password for email: {Email}", request.Email);
            throw new Exception("An error occurred while processing your request. Please try again.");
        }
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        try
        {
            _logger.LogInformation("Reset password attempt for email: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("Reset password failed - user not found: {Email}", request.Email);
                throw new InvalidOperationException("Invalid reset request.");
            }

            // Check if custom token matches and hasn't expired
            if (user.PasswordResetToken != request.Token || 
                user.PasswordResetTokenExpires == null || 
                user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                _logger.LogWarning("Reset password failed - invalid or expired token: {Email}", request.Email);
                throw new InvalidOperationException("Invalid or expired reset token.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Reset password failed for {Email}: {Errors}", request.Email, errors);
                throw new InvalidOperationException($"Password reset failed: {errors}");
            }

            // Clear reset token
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            
            _logger.LogInformation("Successful password reset for user: {Email}", request.Email);
            
            return true;
        }
        catch (InvalidOperationException)
        {
            throw; // Re-throw validation exceptions
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during password reset for email: {Email}", request.Email);
            throw new Exception("An error occurred while resetting your password. Please try again.");
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync(int userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !user.IsActive)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user: {UserId}", userId);
            return null;
        }
    }
}
