using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myschool.Common;
using IBAL.User;
using Models.Common;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace myschool.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBalUser _userService;

        private IWebHostEnvironment _hostingEnvironment;
        public UserController(IBalUser userService, IWebHostEnvironment hostingEnvironmentinject)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironmentinject;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (Request.Headers["User-Agent"].Count > 0)
            {
                var x = Request.Headers["User-Agent"];
            }
            try
            {
                Users user = new Users();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                IActionResult response = Unauthorized();
                user.UserID = login.UserID;
                user.Password = login.Password;
                user.LoginDevice = login.LoginDevice;
                user.LoginIP = login.LoginIP;
                var Authresponse=new Result();
                try
                {
                     Authresponse = AuthenticateUser(user);
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                    throw;
                }
                if (Authresponse != null)
                {
                    if (Authresponse.IsSuccess)
                    {
                        var tokenstr= TokenManager.GenerateJWToken((Users)Authresponse.Data);
                        var userres = (Users)Authresponse.Data;
                        var loginData = new { id= userres.ID, uid=userres.UserID, schoolid=userres.SchId, 
                            fname=userres.FName, mname=userres.MName, lname=userres.LName, Roles= user.UserRoles,
                            token= tokenstr
                        };
                        Authresponse.Data = loginData;
                        return Ok(Authresponse);
                    }
                    else
                    {
                        Authresponse.Data = null;
                        return Ok(Authresponse);
                    }
                }
                //
                return response;
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            
        }

        [CustomAuthorization]
        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new Users { UserID = login.UserID, Password = login.OldPassword };
            var Authresponse = new Result();
            try
            {
                Authresponse = AuthenticateUser(user);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw;
            }
            if (Authresponse != null)
            {
                if (Authresponse.IsSuccess)
                {
                    user.PasswordSalt = PasswordManager.GetSalt(10);
                    user.Password = PasswordManager.EncodePassword(login.Password, user.PasswordSalt);
                    user.UpdatedBy = Convert.ToInt32(Request.getId());
                    user.ID = Convert.ToInt32(Request.getId());
                    _ = await _userService.UpdatePasswordAsync(user);
                    login.Password = null;
                    login.OldPassword = null;
                    Authresponse.Data = login;
                    return Ok(Authresponse);
                }
                else
                {
                    Authresponse.Data = null;
                    Authresponse.Message = UiConstants.WrongPassword;
                    return Ok(Authresponse);
                }
            }
            else
                return Unauthorized(Authresponse);

        }

        [CustomAuthorization]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody]Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IActionResult response = Unauthorized();
            try
            {
                
                var dbuser = _userService.GetUserLoginDetail(user);
                if (dbuser !=null)
                {
                    return Conflict(UiConstants.UserAlreadyRegistered);
                }

                user.PasswordSalt = PasswordManager.GetSalt(10);
                user.Password = PasswordManager.EncodePassword(user.Password, user.PasswordSalt);
                user = await _userService.RegisterUserAsync(user);
                if (user.ID > 0)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        

        private Result AuthenticateUser(Users login)
        {
            var response = _userService.GetUserLoginDetail(login);
            Users user;
            Result result;
            if (response.Result !=null )
            {
                var password = PasswordManager.EncodePassword(login.Password, response.Result.PasswordSalt);
                if (response.Result.Password == password)
                {
                    user = response.Result;
                    user.Password = null;
                    user.PasswordSalt = null;
                    user.LoginIP = login.LoginIP;
                    user.LoginDevice = login.LoginDevice;
                    result = new Result { IsSuccess = true, Data = user };
                    // _userService.UpdateLoginAsync(user);
                }
                else
                {
                    user = response.Result;
                    user.Password = null;
                    user.PasswordSalt = null;
                    result = new Result { IsSuccess = false, Data = user, Message = UiConstants.InvalidCreadentials };
                }
            }
            else
            {
                result = new Result { IsSuccess = false, Message = UiConstants.InvalidCreadentials };
            }

            return result;
        }

    }
}