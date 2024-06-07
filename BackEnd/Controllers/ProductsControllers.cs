using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swp391_sap1805_g6.Entities;
using swp391_sap1805_g6.Filters;
using swp391_sap1805_g6.Reporitories;

namespace swp391_sap1805_g6.Controllers
{

    [ApiController]
    // [Route("api/[controller]")]
    public class ProductsControllers : ControllerBase
    {
        //list pro
        [Route("/Products")]
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(ProductRepo.GetAllProducts());
        }
        //search pro by id
        [Route("/Products/{id}")]
        [HttpGet]
        [IDProFilter]
        public IActionResult GetProById(int id)
        {
            return Ok(ProductRepo.GetProductById(id));
        }
        //add
        [Route("/Products")]
        [HttpPost]
        public IActionResult AddProduct([FromBody]Product product)
        {
            ProductRepo.Add(product);
            return CreatedAtAction(nameof(GetProById), new { id = product.ProductId }, product);
        }
        //update pro by id
        [Route("/Products/{id}")]
        [HttpPut]
        [IDProFilter]
        public IActionResult UpdateProduct(int id,Product product)
        {
            if(id!=product.ProductId)
            {
                return BadRequest();
            }
            try
            {
                ProductRepo.Update(product);              
            }
            catch
            {
               if(!ProductRepo.ProsExist(id))               
                    return NotFound();
                throw;
            }
            return NoContent();
        }
        //delete pro by id
        [Route("/Products/{id}")]
        [HttpDelete]
        [IDProFilter]
        public IActionResult Delete(int id)
        {
            var pro = ProductRepo.GetProductById(id);
            var war = WarrantyRepo.GetWarrantyByProductById(id);
            ProductRepo.Delete(id);
            return Ok(pro);
        }


    }
}
