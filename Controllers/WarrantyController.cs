using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swp391_sap1805_g6.Entities;
using swp391_sap1805_g6.Filters;
using swp391_sap1805_g6.Reporitories;

namespace swp391_sap1805_g6.Controllers
{
    
    [ApiController]
    public  class WarrantyController : ControllerBase
    {
        //get all war info 
        [Route("/Warranty")]
        [HttpGet]
        public IActionResult GetAllWar()
        {
            return Ok(WarrantyRepo.GetAllWar());
        }
        //update duration by proid
        [Route("Warranty/{id}")]
        [HttpPut]
        [IDProFilter]
        public IActionResult UpdateWarranty(int id, Warranty war)
        {
            if (id != war.WarrantyId)
            {
                return BadRequest();
            }
            try
            {
                WarrantyRepo.Update(war);
            }
            catch
            {
                if (!ProductRepo.ProsExist(id))

                    return NotFound();
                throw;
            }
            return NoContent();
        }
    }
}
