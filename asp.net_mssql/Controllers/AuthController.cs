using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Meaurse.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class AuthController : ControllerBase
    {
      

        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login(string type,string name)
        {
            
            if(type != null && name !=null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("just do it, Don't forgive."));
                var signingCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:3000",
                    audience: "http://localhost:3000",
                    claims: new List<Claim>(){new Claim("type",type),new Claim("name",name)},
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Content(tokenString);
            }
            return Unauthorized();
        }
    }
}
