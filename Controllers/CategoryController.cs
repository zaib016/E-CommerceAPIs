using E_CommerceAPIs.Models;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _CategoryRepo;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _CategoryRepo = categoryRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _CategoryRepo.GetAllAsync());
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GatById(int id)
        {
            var category = await _CategoryRepo.GetByIdAsync(id);
            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add(CategoryDTOs categoryDTOs)
        {
            var category = new Category
            {
                CategoryName = categoryDTOs.CategoryName,
            };

            await _CategoryRepo.AddAsync(category);
            return Ok(category);
        }
    }
}
