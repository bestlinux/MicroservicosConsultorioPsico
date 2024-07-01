﻿using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UsersController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser([FromBody] User model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest("Usuário ou senha inválidos");
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                         userInfo.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return BuildToken(userInfo);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "login inválido.");
            return BadRequest(ModelState);
        }
    }

    private UserToken BuildToken(User userInfo)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("consultorio", "https://www.consultoriocida.net"),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // tempo de expiração do token: 3 horas
        var expiration = DateTime.Now.AddHours(3);

        JwtSecurityToken token = new JwtSecurityToken(
           issuer: null,
           audience: null,
           claims: claims,
           expires: expiration,
           signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
