using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace myschool.Common
{
    public class CommonUtil
    {
        public static long GetCurrentUser(HttpContext context)
        {
            if (context.Request.Headers["Authorization"].Count > 0)
            {
                var tokenJson = TokenManager.DecodeToken(context.Request.Headers["Authorization"]);
                var userdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenJson);
                return Convert.ToInt64(userdata["UserID"]);
            }
            return 0;
        }

        public void LogError(HttpContext context)
        {
            var tokenJson = string.Empty;
            var obj = new Error();
            if (context.Request.Headers["Authorization"].Count > 0)
            {
                tokenJson = TokenManager.DecodeToken(context.Request.Headers["Authorization"]);
                var userdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenJson);
                tokenJson = "UserID:" + userdata["UserID"] + ",Email:" + userdata["Email"];
                obj.IP = userdata["IP"];
            }
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var ex = context.Features.Get<IExceptionHandlerFeature>();
            var exp = (ExceptionHandlerFeature)ex;
            obj.UserDetails = tokenJson;
            obj.ErrorPath = exp.Path;
            obj.ErrorDateTime = DateTime.Now;
            obj.StackTrace = exp.Error.StackTrace;
            obj.Message = exp.Error.Message;
           // _commonUtilService = new BusinessService.CommonUtilService(new CommonUtilDal());
           // _commonUtilService.LogApplicationError(obj);
        }

    }
}
