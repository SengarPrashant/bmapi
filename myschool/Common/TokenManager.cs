using JWT;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Common;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace myschool.Common
{
    public class TokenManager
    {
        //private readonly IConfiguration _config;
        public static string secretKey = "123456qwerty1df12dsfd1f2dg545d1g5d4g5s1vhchd545scfcs55nmkhkk43";
        public static string issuer = "test";
        public TokenManager()
        {

        }
      
        public static string DecodeToken(string token)
        {
            try
            {
                var json = new JwtBuilder()
                    .WithSecret(secretKey)
                    .MustVerifySignature()
                    .Decode(token);
                return json;
            }
            catch (TokenExpiredException ex)
            {
                return ex.Message;
            }
            catch (SignatureVerificationException ex)
            {
                return ex.Message;
            }
        }

        public static string GenerateJWToken(Users userInfo)
        {
            var roles = "";
            foreach (var item in userInfo.UserRoles)
            {
                roles = roles + item.Id + ",";
            }
            
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("id",userInfo.ID.ToString()),
                        new Claim("uid",userInfo.UserID),
                        new Claim("schid",userInfo.SchId.ToString()),
                        new Claim("roles",roles[0..^1])
                }),
                Expires = userInfo.LoginDevice=="ph" ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddMinutes(20),
                Issuer = issuer,
                Audience = issuer,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        

        public static string ValidateToken(string token)
        {
            var status = string.Empty;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                var jwttoken = tokenHandler.ReadToken(token);
                if(jwttoken.ValidTo< DateTime.UtcNow)
                {
                    status = "Token expired.";
                }

            }
            catch
            {
                status = "Invalid token.";
            }
            return status;
        }

        public static string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        public static void SetupJWTServices(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters();
            });
        }

    }
}
