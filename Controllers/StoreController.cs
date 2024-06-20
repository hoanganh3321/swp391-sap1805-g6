using BackEnd.Attributes;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        //ok
        //https://localhost:7002/api/Store/GetAllStores
        [AdminAuthorize]
        [HttpGet("GetAllStores")]
        public async Task<ActionResult<IEnumerable<Store>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }
        //ok
        //https://localhost:7002/api/Store/searchStore/{storeId}
        [AdminAuthorize]
        [HttpGet("searchStore/{storeId}")]
        public async Task<ActionResult<Store>> GetStoreById(int storeId)
        {
            var store = await _storeService.GetStoreByIdAsync(storeId);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }
        //untest
        //https://localhost:7002/api/Store/addStore
        [AdminAuthorize]
        [HttpPost("addStore")]
        public async Task<IActionResult> AddStore(Store store)
        {
            await _storeService.CreateStoreAsync(store);
            return CreatedAtAction(nameof(GetStoreById), new { storeId = store.StoreId }, store);
        }
    }
}
