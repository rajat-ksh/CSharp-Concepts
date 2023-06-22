using CatalogService.Models;
using CatalogService.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ICategoryRepository categoryRepository;
        public CatalogController(ICategoryRepository Repository)
        {
            categoryRepository = Repository;
        }

        [HttpGet]
        public IActionResult GetValue()
        {
            return Ok(categoryRepository.GetAllCategories());
        }

        [HttpPost]
        public IActionResult AddValue(Category category)
        {
            try
            {
                categoryRepository.AddCategory(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
