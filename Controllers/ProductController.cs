using E_CommerceAPIs.Models;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly List<Product> productTable = new List<Product>();

        public ProductController(IProductRepository productRepository)
        {
            _productRepo = productRepository;
            for(int i = 1; i <= 100; i++)
            {
                productTable.Add(new Product { ProductId = i , ProductName = "Product", Price = "1000"}); 
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepo.GetAllAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }
        [HttpPost("Add")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(ProductDTOs productDTOs)
        {
            var product = new Product
            {
                CategoryId = productDTOs.CategoryId,
                ProductName = productDTOs.ProductName,
                Price = productDTOs.Price,
                ImageUrl = productDTOs.ImageUrl,
            };

            await _productRepo.AddAsync(product);
            return Ok(product);
        }
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id,ProductDTOs productDTOs)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return BadRequest();

            product.ProductName = productDTOs.ProductName;
            product.Price = productDTOs.Price;
            product.ImageUrl = productDTOs.ImageUrl;

            await _productRepo.UpdateAsync(product);
            return Ok("Product Updated Successfully");
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.DeleteAsync(id);
            if (!product) return NotFound();

            return Ok("Product Deleted Successfully");
        }
        [HttpPost("UploadImage")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No Image uploaded");

            var folder = Path.Combine("wwwroot", "Images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, file.FileName);
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $"{Request.Scheme}://{Request.Host}/image/{file.FileName}";
            return Ok(new { imageUrl = url });

        }
        [HttpGet("Pagination")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
        {
            var totalCount = productTable.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var producrPerPage = productTable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(producrPerPage);
        }
        [HttpPost("AddProductCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddPoduct([FromForm]Product model, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(image != null)
            {
                //Generate a unique filename
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
                Directory.CreateDirectory(uploadDir);

                var filePath = Path.Combine(uploadDir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                model.ImageUrl = $"/images{fileName}";
            }
            try
            {
                var product = _productRepo.AddAsync(model);
                return Ok(product);
            }
            catch (Exception ex)
            {
                //Return error message if something goes wrong
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
