using BackEnd.Attributes;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldPriceDisplayController : ControllerBase
    {
        private readonly IGoldPriceDisplayService _service;

        public GoldPriceDisplayController(IGoldPriceDisplayService service)
        {
            _service = service;
        }
        //https://localhost:7002/api/GoldPriceDisplay/getAllGoldPrice
        [HttpGet("getAllGoldPrice")]
        //ok
        public async Task<ActionResult<IEnumerable<GoldPriceDisplay>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        //https://localhost:7002/api/GoldPriceDisplay/GetGoldPriceById/{id}
        [HttpGet("GetGoldPriceById/{id}")]
        //ok
        [AdminAuthorize]
        public async Task<ActionResult<GoldPriceDisplay>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //https://localhost:7002/api/GoldPriceDisplay/AddGoldPrice
        [HttpPost("AddGoldPrice")]
        [AdminAuthorize]
        //ok
        public async Task<ActionResult> Add(GoldPriceDisplay goldPriceDisplay)
        {
            await _service.AddAsync(goldPriceDisplay);
            return CreatedAtAction(nameof(GetById), new { id = goldPriceDisplay.DisplayId }, goldPriceDisplay);
        }
        //https://localhost:7002/api/GoldPriceDisplay/UpdateGoldPrice/{id}
        //ok
        [AdminAuthorize]
        [HttpPut("UpdateGoldPrice/{id}")]
        public async Task<IActionResult> Update(int id, GoldPriceDisplay goldPriceDisplay)
        {
            if (id != goldPriceDisplay.DisplayId)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(goldPriceDisplay);
            return NoContent();
        }
        //https://localhost:7002/api/GoldPriceDisplay/DeleteGoldPrice/{id}
        [HttpDelete("DeleteGoldPrice/{id}")]
        [AdminAuthorize]
        //ok
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
