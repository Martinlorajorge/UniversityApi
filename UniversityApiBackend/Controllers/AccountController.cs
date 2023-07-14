using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IdentityModel.Tokens.Jwt;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }


        //To Do: Cambiar por usuarios Reales en la DB.
        private IEnumerable<User> Logins = new List<User>()
        {
            new User {
                Id = 1,
                Email = "martin@cualquiercosa.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User {
                Id = 2,
                Email = "pepe@cualquiercosa.com",
                Name = "User1",
                Password = "pepe"
            }

        };


        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any( user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                // Any es como decir si cualquiera coincide
                
                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),


                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);
            }catch (Exception ex)
            {
                throw new Exception("GetToken ERROR", ex);
            }
        }

        // Esto es para que solo los administradores puedan ver a todos los usuarios en el sistema
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
