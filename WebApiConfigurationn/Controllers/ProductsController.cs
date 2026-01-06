using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Categories;
using WebApiConfigurationn.Entities.DTOs.Products;

namespace WebApiConfigurationn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;

        public ProductsController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    var result = await _context.Products.ToListAsync();
        //    return StatusCode((int)HttpStatusCode.OK, result);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            //Product product = new Product()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = dto.Name,
            //    Price = dto.Price,
            //    Currency = dto.Currency,
            //    Count = dto.Count,
            //    Description = dto.Description,
            //    CreatedAt = DateTime.UtcNow
            //};

            var product = _mapper.Map<Product>(dto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDTO dto)
        {
            Product validproduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (validproduct == null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Category tapimadi"
                });
            }


            validproduct.Name = dto.Name;
            validproduct.Currency = dto.Currency;
            validproduct.Price = dto.Price;
            validproduct.Count= dto.Count;
            validproduct.Description = dto.Description;
            validproduct.CreatedAt = DateTime.UtcNow;

           

            _context.Products.Update(validproduct);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<GetProductDTO>> GetProductById(Guid id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = " Tapilmadi"
                });

            }
            GetProductDTO dto = new GetProductDTO()
            {
                Name = product.Name
            };
            return Ok(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deleteproduct = await _context.Products.FirstOrDefaultAsync(p=> p.Id == id);
            _context.Products.Remove(deleteproduct);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await _context.Products.Include(p=>p.Category).Select(p => new GetProductDTO
            {
                Name = p.Name,
                CategoryName = p.Category.Name
            }).ToListAsync(); ;
            var result = _mapper.Map<List<GetProductDTO>>(product);
            return Ok(result);
        }
    }
}

