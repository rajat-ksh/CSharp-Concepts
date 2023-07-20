using CatalogService.Data;
using CatalogService.Models.RequestModel;
using CatalogService.Models.ResponseModel;
using CatalogService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Filter;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly CatalogServiceAPIDbContext dbContext;
        private PaginationFilter paginationFilter;

        public ItemController(CatalogServiceAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            paginationFilter = new PaginationFilter();
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await dbContext.Item.ToListAsync();
            List<ItemResponse> responses = new List<ItemResponse>();
            foreach (var item in items)
            {
                responses.Add(CreateItemResponse(item));
            }

            return Ok(responses);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var item = await dbContext.Item.FindAsync(id);
            if (item != null)
            {
                return Ok(CreateItemResponse(item));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemRequest item)
        {
            var newItem = new Item()
            {
                Name = item.Name,
                CategoryId = item.CategoryId
            };
            try
            {
                await dbContext.Item.AddAsync(newItem);
                await dbContext.SaveChangesAsync();
                return Ok(CreateItemResponse(newItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, UpdateItemRequest updateItem)
        {
            var item = await dbContext.Item.FindAsync(id);
            if (item != null)
            {
                item.Name = updateItem.Name;
                item.CategoryId = updateItem.CategoryId;
                await dbContext.SaveChangesAsync();
                return Ok(CreateItemResponse(item));
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var item = await dbContext.Item.FindAsync(id);
            if (item != null)
            {
                dbContext.Remove(item);
                await dbContext.SaveChangesAsync();
                return Ok(CreateItemResponse(item));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ItemByCategory/{id:int}")]
        public async Task<IActionResult> GetItemByCategoryId([FromRoute] int id)
        {
            var items = await dbContext.Item.Where(item => item.CategoryId == id)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
               .Take(paginationFilter.PageSize).ToListAsync();

            List<ItemResponse> responses = new List<ItemResponse>();
            if (items.Count != 0)
            {
                foreach (var item in items)
                {
                    responses.Add(CreateItemResponse(item));
                }
                return Ok(responses);
            }
            return NotFound();
        }

        private ItemResponse CreateItemResponse(Item item)
        {
            return new ItemResponse { Id = item.Id, Name = item.Name, CategoryId = item.CategoryId };
        }
    }
}
