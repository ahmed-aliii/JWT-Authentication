using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIAssginment.Models;
using WebAPIAssginment.Models.Repository;
using WebAPIAssginment.Models.ViewModels;

namespace WebAPIAssginment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("{id:int}")] //api/Cateogry/id
        public IActionResult GetByCatId(int id)
        {
            Product product = productRepository.GetById(id);
            return Ok(product);
        }

        [Authorize]
        [HttpGet] //api/Cateogry
        public IActionResult GetAllCat()
        {
            return Ok(productRepository.GetAll());
        }
          

        [HttpPost] //api/Cateogry
        public IActionResult Addproduct(ProductDTO productVM) //Comes/Binding At Req Body
        {
            Product product = new Product()
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Description = productVM.Description,
                CategoryId = productVM.CategoryId
            };
            productRepository.Add(product);
            productRepository.Save();

            return CreatedAtAction("GetByCatId", new { id = product.Id }, product); 
        }


        [HttpPut("{id:int}")] //api/Cateogry/id
        public IActionResult Updateproduct(int id , ProductDTO productVM)
        {
            Product product = productRepository.GetById(id);

            if(product != null)
            {
                product.Name = productVM.Name;
                product.Price = productVM.Price;
                product.Description = productVM.Description;
                product.CategoryId = productVM.CategoryId;

                productRepository.Update(product);
                productRepository.Save();

                return CreatedAtAction("GetByCatId", new { id = product.Id }, product);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{id:int}")] //api/Cateogry/id
        public IActionResult Deleteproduct(int id)
        {
            Product product = productRepository.GetById(id);

            if (product != null)
            {
                productRepository.Remove(id);
                productRepository.Save();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }



    }
}
