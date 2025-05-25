using Microsoft.Extensions.Logging;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Common.Interfaces;
using TricycleFareAndPassengerManagement.Application.Common.Models;
using TricycleFareAndPassengerManagement.Domain.Entities;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Infrastructure.Services
{
    public class AuthService(IUserRepository _userRepository,
    IRoleRepository _roleRepository,
    ITokenService _tokenService,
    ILogger<AuthService> _logger) : IAuthService
    {
        #region Public Methods

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null || !user.IsActive)
                {
                    return new AuthResult { Success = false, Errors = { "Invalid credentials" } };
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new AuthResult { Success = false, Errors = { "Invalid credentials" } };
                }

                var roles = await _roleRepository.GetUserRolesAsync(user.Id);
                var roleNames = roles.Select(r => r.Name).ToList();

                var token = _tokenService.GenerateAccessToken(user, roleNames);
                var refreshToken = _tokenService.GenerateRefreshToken();

                // Update last login
                await _userRepository.UpdateAsync(user);

                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    User = new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Roles = roleNames
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email {Email}", email);
                return new AuthResult { Success = false, Errors = { "An error occurred during login" } };
            }
        }

        public async Task<AuthResult> RegisterAsync(string fullName, string email, string password)
        {
            try
            {
                if (await _userRepository.ExistsAsync(email))
                {
                    return new AuthResult { Success = false, Errors = { "User with this email already exists" } };
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User
                {
                    FullName = fullName,
                    Email = email.ToLower(),
                    PasswordHash = passwordHash,
                };

                var createdUser = await _userRepository.CreateAsync(user);

                // Assign default "User" role
                var userRole = await _roleRepository.GetByNameAsync("DefaultUser");
                if (userRole != null)
                {
                    await _roleRepository.AssignRoleToUserAsync(createdUser.Id, userRole.Id);
                }

                var roles = new List<string> { "DefaultUser" };
                var token = _tokenService.GenerateAccessToken(createdUser, roles);
                var refreshToken = _tokenService.GenerateRefreshToken();

                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    User = new UserDto
                    {
                        Id = createdUser.Id,
                        FullName = createdUser.FullName,
                        Email = createdUser.Email,
                        Roles = roles
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email {Email}", email);
                return new AuthResult { Success = false, Errors = { "An error occurred during registration" } };
            }
        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            // Implementation for refresh token logic
            // This is a simplified version - in production, you'd store refresh tokens in database
            await Task.CompletedTask;
            return new AuthResult { Success = false, Errors = { "Invalid refresh token" } };
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            // Implementation for token revocation
            await Task.CompletedTask;
            return true;
        }

        #endregion Public Methods
    }
}