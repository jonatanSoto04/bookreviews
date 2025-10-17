using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookReviewsAPI.Models;
using BookReviewsAPI.DTOs;
using BookReviewsAPI.Services;

namespace BookReviewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTo model)
        {
            if (model.Password != model.ConfirmPassword)
                return BadRequest("Las contraseñas no coinciden");

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Usuario registrado exitosamente" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginDto model)
{
    var user = await _userManager.FindByEmailAsync(model.Email);
    
    if (user == null)
        return Unauthorized("Credenciales inválidas");

    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

    if (result.Succeeded)
    {
        var token = _jwtService.GenerateToken(user);
        
        return Ok(new authResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60),
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        });
    }

    return Unauthorized("Credenciales inválidas");
}
    }
}