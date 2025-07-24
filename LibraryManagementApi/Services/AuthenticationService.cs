using LibraryManagementApi.Database;
using LibraryManagementApi.DTOs.Authentication;
using LibraryManagementApi.Models;
using LibraryManagementApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementApi.Services
{
    public class AuthenticationService : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Function to login user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if ( user is null )
            {
                return null;
            }
            else
            {
                var hashedPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, loginDto.Password);
                if ( hashedPassword == PasswordVerificationResult.Failed )
                {
                    return null;
                }

                return GenerateToken(user);
            }
        }

        /// <summary>
        /// Function to register user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            // Checking there is any account with this email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email.Trim());

            if ( user is not null ) 
            {
                return null;
            }

            // Hashing the password
            var password = registerDto.Password.Trim();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user!, password);

            // Inserting new user to the database
            User newUser = new User
            {
                FirstName = registerDto.FirstName.Trim(),
                LastName = registerDto.LastName.Trim(),
                Email = registerDto.Email.Trim(),
                Password = hashedPassword,
                Role = registerDto.Role,
                CreatedAt = DateTime.Now
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private string GenerateToken(User user)
        {
            // Creating claims
            var claims = new List<Claim>
            {
                new Claim("role", user.Role),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("email", user.Email),
            };

            // Creating signing key
            string key = _configuration["JwtSettings:Key"]!;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            // Create credentials
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);

            // Creatign token descriptio
            string issuer = _configuration["JwtSettings:Issuer"]!;
            string audience = _configuration["JwtSettings:Audience"]!;

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.Now.AddHours(1)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
