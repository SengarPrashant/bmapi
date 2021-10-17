using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBAL.Common;
using IBAL.Firm;
using Microsoft.AspNetCore.Authorization;
using Models.Common;
using Models.Helpers;
using Razorpay.Api;

namespace myschool.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IBALCommon _CommonService;
        private readonly IBalFirm _FirmService;
        public CommonController(IBALCommon  baLCommon, IBalFirm balFirm)
        {
            _CommonService = baLCommon;
            _FirmService = balFirm;
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
                customer.Id = cusId;
                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GenerateOrder")]
        public async Task<IActionResult> GenerateOrder(CustomerBasicDetail customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.Name) ||
                    string.IsNullOrEmpty(customer.Email) ||
                    string.IsNullOrEmpty(customer.Mobile))
                {
                    return BadRequest();
                }
                var _plan= await _FirmService.GetPlanTypes(customer.PlanId, true, customer.PlanTypeId);
                if(_plan.ToList().Count > 0)
                {
                    var currentPlan = _plan.ToList()[0];
                    //var cusId = await _CommonService.SaveCustomerbasicDetails(customer);
                    //customer.Id = cusId;
                    var _transactionId = CommonUtil.GetTransactionID(customer.Id.ToString());
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    var _amount= CommonUtil.GetRPFormatedAmount(currentPlan.Price, currentPlan.Currency);
                    options.Add("amount", _amount); // amount in the smallest currency unit
                    options.Add("receipt", _transactionId);
                    options.Add("currency", currentPlan.Currency);
                    var client = new RazorpayClient(CommonUtil.RPDevKey, CommonUtil.RPDevSecret);
                    Order order = client.Order.Create(options);
                    CustomerOrder orderModel = new CustomerOrder
                    {
                        orderId = order.Attributes["id"],
                        razorpayKey = CommonUtil.RPDevKey,
                        amount = _amount,
                        currency = currentPlan.Currency,
                        name = customer.Name,
                        email = customer.Email,
                        contactNumber = customer.Mobile
                        //address = "delhi",
                        //description = "Testing description"
                    };
                    return Ok(orderModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
