using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBAL.Common;
using Microsoft.AspNetCore.Authorization;
using Models.Common;

namespace myschool.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IBALCommon _CommonService;
        public CommonController(IBALCommon  baLCommon)
        {
            _CommonService = baLCommon;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPageInfo")]
        public async Task<IActionResult> GetPageInfo(string pageCode)
        {
            try
            {
                if (string.IsNullOrEmpty(pageCode))
                {
                    return BadRequest();
                }
                var infos = await _CommonService.GetPageInfo(pageCode);
                var pagelinks = await _CommonService.GetPageLinks(pageCode);
                return Ok(new { Info= infos, PageLinks= pagelinks });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetLocation")]
        public async Task<IActionResult> GetLocation(string? searchText, int? startRow, int? pageSize, bool exact)
        {
            try
            {
                var location = await _CommonService.GetLocation(searchText, startRow, pageSize, exact);
                return Ok(location);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("SaveCustomer")]
        public async Task<IActionResult> SaveCustomerbasicDetail(CustomerBasicDetail customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.Name) ||
                    string.IsNullOrEmpty(customer.Email) ||
                    string.IsNullOrEmpty(customer.Mobile))
                {
                    return BadRequest();
                }
                var cusId = await _CommonService.SaveCustomerbasicDetails(customer);
                return Ok(cusId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
