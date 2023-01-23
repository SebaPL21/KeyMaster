using KeyMaster.Context;
using KeyMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace KeyMaster.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ApplicationDBContext _context;

        public UserController(IConfiguration configuration, ApplicationDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (user != null && user.Email != null && user.Password != null)
            {
                var userData = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

                if (userData == null)
                {
                    return BadRequest();
                }
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (jwt != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("email", userData.Email),
                        new Claim("userId", userData.UserId.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: singIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("invalid credentials");
                }
            }
            else
            {
                return BadRequest("invalid credentials");
            }
        }
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == GetUserId());
            if (user == null) { return NotFound(); }
            return Ok(new UserModel { Email = user.Email });
        }
    }
}
