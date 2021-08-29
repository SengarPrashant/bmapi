using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myschool.Common
{
    public class CustomAuthorization : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Request.Headers["auth"].Count > 0)
            {
                string token=context.HttpContext.Request.Headers["auth"].ToString();
                var status = TokenManager.ValidateToken(token);
                if (!string.IsNullOrEmpty(status))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var roles = TokenManager.GetClaim(token, "roles");
                var claimList = new List<Claim>();
                
                claimList.Add(new Claim("roles", roles));
                claimList.Add(new Claim("id", TokenManager.GetClaim(token, "id")));
                claimList.Add(new Claim("uid", TokenManager.GetClaim(token, "uid")));
                claimList.Add(new Claim("schid", TokenManager.GetClaim(token, "schid")));
                context.HttpContext.User.AddIdentity(new System.Security.Claims.ClaimsIdentity(claimList));
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

       
    }

    public static class RequestExt
    {
        public static string getUserId(this Microsoft.AspNetCore.Http.HttpRequest ob)
        {
            var claims = ob.HttpContext.User.Claims.ToList();
            return claims.Where(o => o.Type == "uid").FirstOrDefault().Value;
        }
        public static string getSchoolId(this Microsoft.AspNetCore.Http.HttpRequest ob)
        {
            var claims = ob.HttpContext.User.Claims.ToList();
            return claims.Where(o => o.Type == "schid").FirstOrDefault().Value;
        }
        public static string getId(this Microsoft.AspNetCore.Http.HttpRequest ob)
        {
            var claims = ob.HttpContext.User.Claims.ToList();
            return claims.Where(o => o.Type == "id").FirstOrDefault().Value;
        }
        public static string getRoles(this Microsoft.AspNetCore.Http.HttpRequest ob)
        {
            var claims = ob.HttpContext.User.Claims.ToList();
            return claims.Where(o => o.Type == "roles").FirstOrDefault().Value;
        }
    }

}
