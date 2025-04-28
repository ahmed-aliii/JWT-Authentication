using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPIAssginment.Models;
using WebAPIAssginment.Models.DTOs;
using WebAPIAssginment.Models.Repository;

namespace WebAPIAssginment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        //api/Category/id
        [HttpGet("{id:int}")]
        public IActionResult GetCategoryByIdWithProudctsNames(int id)
        {
            Category category =
                categoryRepository.GetByIdWithIncluse(id);
           
            if(category == null || id <= 0)
            {
                return NotFound();
            }


            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                ProductsNames = category.Products.Select(p => p.Name).ToList()
            };

            return Ok(categoryDTO);
        }
    }
}
