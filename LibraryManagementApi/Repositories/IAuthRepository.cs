using LibraryManagementApi.DTOs.Authentication;
using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Repositories
{
    public interface IAuthRepository
    {
        Task<string?> LoginAsync(LoginDto loginDto);
        Task<User> RegisterAsync(RegisterDto registerDto);
    }
}
