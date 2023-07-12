using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim> {
                 new Claim("Id", userAccounts.Id.ToString()),
                 new Claim(ClaimTypes.Name, userAccounts.UserName),
                 new Claim(ClaimTypes.Email, userAccounts.EmailId),
                 new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                 new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyyy HH:mm:ss tt")),
            };



            //Con estos If creo Roles distintos, Utilizando como identificador el Name pero se puede utilizar cualquier premiza
            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }else if (userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly","User 1"));
            }

            return claims;
        }


        //Genera un nuevo Id
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }


        public static UserTokens GetTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if (model==null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //Obtain SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSiningKey);

                Guid Id;

                //Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity of out token
                userToken.Validity = expireTime.TimeOfDay;

                // Generate Our JWT
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials( //  Esto va a sifrar todo
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256));


                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;


            }
            catch (Exception exception)
            {
                throw new Exception("Error Generating the JWT", exception);
            }


        }


    }
}
