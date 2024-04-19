using System.Text;
using System.Security.Claims;
using Vezeeta.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Core.Contracts.Authentication;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Core.Models;
using Vezeeta.Core.Enums;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IBaseRepository<Doctor> _doctors;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IBaseRepository<Doctor> doctors)
    {
        _userManager = userManager;
        _configuration = configuration;
        _doctors = doctors;
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        var loginValidate = new LoginValidator();
        var validate = await loginValidate.ValidateAsync(login);
        if(!validate.IsValid)
        {
            return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
        }
        ApplicationUser? user = await _userManager.FindByEmailAsync(login.Email ?? "");
        if(user == null)
            return NotFound("This email is not registered");
        if (await _userManager.CheckPasswordAsync(user, login.Password??""))
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Role", userRoles.First()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRoles.First())
            };

            if(user.UserType.Equals(UserType.Doctor))
            {
                var doctor = await _doctors.GetByConditionAsync(e => e.ApplicationUserId == user.Id);
                if (doctor == null)
                    return NotFound("doctor not found");
                authClaims.Add(new("DoctorId", doctor.Id.ToString()));
            }

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]??""));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiaration = token.ValidTo });
        }
        return BadRequest("Wrong password");
    }
}
