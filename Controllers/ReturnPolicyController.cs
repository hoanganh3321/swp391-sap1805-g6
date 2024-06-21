using BackEnd.Attributes;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnPolicyController : ControllerBase
    {
        private readonly IReturnPolicyService _returnPolicyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReturnPolicyController(IReturnPolicyService returnPolicyService, IHttpContextAccessor httpContextAccessor)
        {
            _returnPolicyService = returnPolicyService;
            _httpContextAccessor = httpContextAccessor;
        }

        //https://localhost:7002/api/ReturnPolicy/add
        [HttpPost("add")]
        [AdminAuthorize]
        public async Task<IActionResult> AddReturnPolicy(ReturnPolicy returnPolicy)
        {
            
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("Phien dang nhap het han"); }
            var newReturnPolicy = await _returnPolicyService.AddReturnPolicyAsync(returnPolicy);
            return Ok(newReturnPolicy);
        }

        //https://localhost:7002/api/ReturnPolicy/search/{id}
        [HttpGet("search/{id}")]
        public async Task<IActionResult> SearchReturnPolicyAsync(int? id)
        {
            var resultReturnPolicy = await _returnPolicyService.SearchReturnPolicyAsync(id);
            if (resultReturnPolicy == null)
            {
                return NotFound();
            }
            return Ok(resultReturnPolicy);
        }

        //https://localhost:7002/api/ReturnPolicy/list
        //ok
        [HttpGet("list")]
        public async Task<IActionResult> GetAllReturnPolicies()
        {
            var policies = await _returnPolicyService.GetAllReturnPoliciesAsync();
            return Ok(policies);
        }

        //https://localhost:7002/api/ReturnPolicy/update/{id}
        //ok
        [HttpPut("update/{id}")]
        [AdminAuthorize]  
        public async Task<IActionResult> UpdateReturnPolicy(int id, ReturnPolicy returnPolicy)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("Phien dang nhap het han"); }
            var updated = await _returnPolicyService.UpdateReturnPolicyAsync(id, returnPolicy);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }

        //https://localhost:7002/api/product/delete/{id}
        [HttpDelete("delete/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeleteReturnPolicy(int id)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("Phien dang nhap het han"); }
            var deleted = await _returnPolicyService.DeleteReturnPolicyAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
