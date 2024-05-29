using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Token;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] InputModel Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var rng = new RNGCryptoServiceProvider();
                var bytes = new byte[32];
                rng.GetBytes(bytes);
                var secretKey = Convert.ToBase64String(bytes);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(secretKey))
                    .AddSubject("Canal Dev Net Core")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("UsuarioAPINumero", "1")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.Value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}