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

namespace myschool.Controllers.Schools
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IBalUser _userService;

        private IWebHostEnvironment _hostingEnvironment;
        public SchoolsController(IBalUser userService, IWebHostEnvironment hostingEnvironmentinject)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironmentinject;
        }
        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IActionResult response = Unauthorized();
            try
            {
                //school created
                school = await _userService.CreateUpdateSchoolAsyc(school);
                if (school.Id > 0)
                {
                    school.SchoolCode = (1000 + school.Id).ToString();
                    var fileName = school.Id + "_" + school.SchoolCode + school.ImageExt;
                    var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "schoolLogo");
                    school.SchoolLogo = fileName;
                    school = await _userService.CreateUpdateSchoolAsyc(school);
                    System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(school.ImageData.Split(",")[1]));
                    school.ImageData = null;
                    return Ok(school);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CustomAuthorization]
        [HttpPut]
        public async Task<IActionResult> Put(int id,[FromBody]School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != school.Id)
            {
                return BadRequest(ModelState);
            }
            IActionResult response = Unauthorized();
            try
            {
                var result= await _userService.GetSchollDetailAsync(school);
                if (result.Id > 0)
                {
                    school.SchoolCode = result.SchoolCode;
                    if (string.IsNullOrEmpty(school.ImageData))
                    {
                        school.SchoolLogo = result.SchoolLogo;
                    }
                    else
                    {
                        var fileName = school.Id + "_" + school.SchoolCode + school.ImageExt;
                        var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "schoolLogo");
                        school.SchoolLogo = fileName;
                        System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(school.ImageData.Split(",")[1]));
                    }
                    school = await _userService.CreateUpdateSchoolAsyc(school);
                    school.ImageData = null;
                }
                
                if (school.Id > 0)
                {
                    school.SchoolCode = (1000 + school.Id).ToString();
                    
                    return Ok(school);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(int id = 0, string schoolcode = null)
        {
            if (id == 0 && string.IsNullOrEmpty(schoolcode))
            {
                return BadRequest();
            }
            else
            {
                School school = new School { Id = id, SchoolCode = schoolcode };
                var response = _userService.GetSchollDetailAsync(school);
                if (response.Result == null)
                {
                    return NotFound();
                }
                string path = _hostingEnvironment.WebRootPath + "/schoolLogo/" + response.Result.SchoolLogo;
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                response.Result.ImageData = Convert.ToBase64String(bytes);
                return Ok(response.Result);
            }
        }

        [CustomAuthorization]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                School school = new School { Id = id };
                var response = await _userService.GetSchollDetailAsync(school);
                if (response.Id == 0)
                {
                    return NotFound();
                }
                response.IsActive = false;
                _ = await _userService.CreateUpdateSchoolAsyc(school);
                return Ok();
            }
        }

        [CustomAuthorization]
        [HttpPost]
        [Route("GetSchoolList")]
        public async Task<IActionResult> GetSchoolList([FromBody]School school = null)
        {
            var result = await _userService.GetAllSchoolAsync(school);

            result.ForEach(el => {
                el.SchoolLogo = Request.Host.Host.Contains("localhost") ? UiConstants.baseUrlLocal + "/schoolLogo/" + el.SchoolLogo : UiConstants.baseUrl + "/schoolLogo/" + el.SchoolLogo;
            });

            foreach (var item in result)
            {
                item.ImageData = "";
            }

            return Ok(result);
        }
    }
}