using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using apichipper.Auth;
using apichipper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apichipper.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AiController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    // [HttpPost]
    // [Route("ai")]
    // public async Task<IActionResult> Ai([FromBody] LoginModel model)
    // {
    //     var user = await _userManager.FindByNameAsync(model.Username);
    //     if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    //     {
    //         var userRoles = await _userManager.GetRolesAsync(user);
    //
    //         var authClaims = new List<Claim>
    //         {
    //             new Claim(ClaimTypes.Name, user.UserName),
    //             new Claim(ClaimTypes.Thumbprint, user.Id),
    //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //         };
    //
    //         foreach (var userRole in userRoles)
    //         {
    //             authClaims.Add(new Claim(ClaimTypes.Role, userRole));
    //         }
    //
    //         var token = GetToken(authClaims);
    //
    //         return Ok(new
    //         {
    //             token = new JwtSecurityTokenHandler().WriteToken(token),
    //             expiration = token.ValidTo,
    //             id = user.Id
    //         });
    //     }
    //
    //     return Unauthorized();
    // }
}