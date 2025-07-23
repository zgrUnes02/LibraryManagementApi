using LibraryManagementApi.DTOs.Authentication;
using LibraryManagementApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthenticationController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {
            var result = await _authRepository.LoginAsync(loginDto);

            if ( result == null )
            {
                return NotFound("Email or password not valid.");
            }

            return Ok(result);
        }

        //[HttpGet]
        //[Route("register")]
        //public Task<IActionResult> register(LoginDto loginDto)
        //{

        //}
    }
}
