using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProductApi.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.Features.Authentication;

[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(GroupName = AppConstants.ApiGroupName)]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Hardcoded user for demo
        bool loginSuccess = false;
        bool isAdmin = false;
        if (model.Username == "user@zeiss.com" && model.Password == "password123")
        {
            loginSuccess = true;
        }

        if (model.Username == "admin@zeiss.com" && model.Password == "password123")
        {
            loginSuccess = true;
            isAdmin = true;
        }

        if (loginSuccess)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, UserRoleType.User)
            };

            if (isAdmin)
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, UserRoleType.Admin)).ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
             var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiryInMinutes", 10)),
                    signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        return Unauthorized();
    }
}
