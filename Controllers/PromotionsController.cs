using BackEnd.Attributes;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Services;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PromotionsController(IPromotionService promotionService, IHttpContextAccessor httpContextAccessor)
        {
            _promotionService = promotionService;
            _httpContextAccessor = httpContextAccessor;
        }
        //https://localhost:7002/api/Promotions/GetPromotions
        [HttpGet("GetPromotions")]
        // [AdminAuthorize]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromotions()
        {          
            var promotions = await _promotionService.GetAllPromotionsAsync();
            return Ok(promotions);
        }
        //IEnumerable:  lặp qua các phần tử của một tập hợp và không cần thêm, xóa hoặc sửa đổi các phần tử.
        //List<T>:cần một tập hợp có thể thay đổi được, có thể thêm, xóa, hoặc sửa đổi các phần tử, và cần truy cập ngẫu nhiên tới các phần tử của nó.
        //https://localhost:7002/api/Promotions/search/{id}
        [HttpGet("search/{id}")]
        [AdminAuthorize]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return Ok(promotion);
        }
        //https://localhost:7002/api/Promotions/add
        [HttpPost("add")]
        [AdminAuthorize]
        //ok
        public async Task<ActionResult> CreatePromotion(PromotionViewModel promotion)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han");}
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _promotionService.CreatePromotionAsync(admin, promotion);
            return CreatedAtAction(nameof(GetPromotion), new { id = promotion.Name }, promotion);
        }

        //https://localhost:7002/api/Promotions/update/{id}
        //ok
        [HttpPut("update/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> UpdatePromotion(int id, [FromBody] Promotion promotion)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            if (id != promotion.PromotionId)
            {
                return BadRequest();
            }
            await _promotionService.UpdatePromotionAsync(admin,id, promotion);
            return NoContent();
        }
        //https://localhost:7002/api/Promotions/DeletePromotion/{id}
        [HttpDelete("DeletePromotion/{id}")]
        [AdminAuthorize]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            int? admin = _httpContextAccessor.HttpContext.GetAdminId();
            if (admin == null) { throw new Exception("phien dang nhap het han"); }
            await _promotionService.DeletePromotionAsync(id);
            return NoContent();
        }
    }
}
