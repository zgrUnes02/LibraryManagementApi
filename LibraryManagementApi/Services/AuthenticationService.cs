using LibraryManagementApi.Database;
using LibraryManagementApi.DTOs.Authentication;
using LibraryManagementApi.Models;
using LibraryManagementApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Services
{
    public class AuthenticationService : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthenticationService(AppDbContext context)
        {
            _context = context;
        }

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

                return "fghjkldsjdlkas";
            }
        }

        public Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
