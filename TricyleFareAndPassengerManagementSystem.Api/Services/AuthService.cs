using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TricyleFareAndPassengerManagementSystem.Api.Data;
using TricyleFareAndPassengerManagementSystem.Api.DTOs;
using TricyleFareAndPassengerManagementSystem.Api.Models;

namespace TricyleFareAndPassengerManagementSystem.Api.Services
{
    public interface IAuthService
    {
        #region Public Methods

        Task<AuthResponse> LoginAsync(LoginRequest request);

        Task<AuthResponse> RegisterAsync(RegisterRequest request);

        string GenerateJwtToken(string username);

        #endregion Public Methods
    }

    public class AuthService : IAuthService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        #endregion Fields

        #region Public Constructors

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            var token = GenerateJwtToken(user.Username);

            return new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Username = user.Username
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Username already exists"
                };
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user.Username);

            return new AuthResponse
            {
                Success = true,
                Message = "Registration successful",
                Token = token,
                Username = user.Username
            };
        }

        public string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Public Methods

        #region Private Methods

        private string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[16];
                rng.GetBytes(salt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                var hash = pbkdf2.GetBytes(20);

                var hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hashBytes = Convert.FromBase64String(storedHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

        #endregion Private Methods
    }
}