using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackEnd.Models;

namespace BackEnd.Filters
{
        public class AdminAuthorizationFilter : IAsyncAuthorizationFilter
        {
            private readonly IConfiguration _config;
            public AdminAuthorizationFilter(IConfiguration config)
            {
                _config = config;
            }
            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                // Check if Authorization header is present
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
                        ClockSkew = TimeSpan.Zero

                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    if (jwtToken.ValidTo < DateTime.UtcNow)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    var AdminID = int.Parse(jwtToken.Claims.First().Value);
                    context.HttpContext.Items["AdminID"] = AdminID;
                }
                catch
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        } 
    }

