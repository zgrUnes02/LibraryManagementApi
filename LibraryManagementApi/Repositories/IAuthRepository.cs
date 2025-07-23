using LibraryManagementApi.DTOs.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Repositories
{
    public interface IAuthRepository
    {
        Task<string?> LoginAsync(LoginDto loginDto);
        Task<IActionResult> RegisterAsync(RegisterDto registerDto);
    }
}
