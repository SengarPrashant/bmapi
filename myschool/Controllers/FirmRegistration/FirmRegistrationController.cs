using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBAL.Firm;
using Microsoft.AspNetCore.Authorization;

namespace myschool.Controllers.FirmRegistration
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmRegistrationController : ControllerBase
    {

        private readonly IBalFirm _firmService;
        public FirmRegistrationController(IBalFirm firmService)
        {
            _firmService = firmService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPlans(string planCode)
        {
            try
            {
                if (string.IsNullOrEmpty(planCode))
                {
                    return BadRequest();
                }
                var plan = await _firmService.GetPlan(planCode, null);
                return Ok(plan);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
