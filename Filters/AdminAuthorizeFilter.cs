﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackEnd.Models;

namespace BackEnd.Filters
{
    //REFERENCE NGUYEN DUC HOANG BKHN https://www.facebook.com/nguyen.duc.hoang.bk
    public class AdminAuthorizationFilter : IAsyncAuthorizationFilter // kiểm tra quyền truy cập trong ASP.NET Core để đảm bảo rằng chỉ những người dùng có quyền admin mới được phép truy cập vào các hành động cụ thể của controller.
    {
            private readonly IConfiguration _config;
            public AdminAuthorizationFilter(IConfiguration config)
            {
                _config = config;
            }
        //OnAuthorizationAsync : được gọi trước khi hành động controller được thực hiện. 
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

