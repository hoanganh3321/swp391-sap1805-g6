using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackEnd.Models;

namespace BackEnd.Filters
{
    public class StaffAuthorizerFilter : IAsyncAuthorizationFilter
    {
        private readonly IConfiguration _config;
        public StaffAuthorizerFilter(IConfiguration config)
        {
            _config = config;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"] ?? "");
           
            try
            {
                var claimPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //neu token het han
                    //thi goi phuong thuc validate token 
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);
                var jwttoken = (JwtSecurityToken)validatedToken;
                if (jwttoken.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var StaffID = int.Parse(jwttoken.Claims.First().Value);
                context.HttpContext.Items["StaffID"] = StaffID;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
