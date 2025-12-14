using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbestraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IManagerService manager;

        public CategoriesController(IManagerService manager)
        {
            this.manager = manager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnCategoryDto>>> GetAllCategories()
        {
            var categories =await manager.category.GetAllCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnCategoryDto?>> GetCategory(int id)
        {
            var category=await manager.category.GetCategoryById(id);
            if (category == null)
            {
                return NotFound($"Category with {category?.Name} not found.");
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<ReturnCategoryDto>> AddCategory(ReturnCategoryDto category)
        {
            var ExistCategory=await manager.category.GetCategoryById(category.Id);
            if (ExistCategory != null)
            {
                return Conflict($"Category with ID {category.Id} already exists.");
            }
            return await manager.category.AddCategory(category);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var IsExist =await manager.category.GetCategoryById(id);
            if (IsExist == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            await manager.category.DeleteCategory(IsExist);
            return Ok("Category Deleted");   
        }
        [HttpPut]
        public async Task<ActionResult<ReturnCategoryDto>> UpdateCategory(ReturnCategoryDto category)
        {
            var IsExist=await manager.category.GetCategoryById(category.Id);
            if(IsExist ==null)
            {
                return NotFound($"Category with ID {category.Id} not found.");
            }
             await manager.category.UpdateCategory(category);
            return Ok(category);
        }
    }
}
