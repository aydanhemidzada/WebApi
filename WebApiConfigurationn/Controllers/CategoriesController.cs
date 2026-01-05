using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Categories;

namespace WebApiConfigurationn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;

        public CategoriesController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _context.Categories.ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO dto)
        {
            //Category category = new Category()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = dto.Name,
            //    Description = dto.Description,
            //    CreatedAt = DateTime.UtcNow

            //};

            var category = _mapper.Map<Category>(dto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();

        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO dto)
        {
            Category validcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (validcategory == null)
            {
                return NotFound();
                //return BadRequest(new
                //{
                   
                //    status = HttpStatusCode.BadRequest,
                //    message="Category tapimadi"
                //});
            }


            validcategory.Name = dto.Name;
            validcategory.Description = dto.Description;
            validcategory.CreatedAt = DateTime.UtcNow;
            _context.Categories.Update(validcategory);
            await _context.SaveChangesAsync();
            return Ok();

        }



        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            Category category= await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            if(category == null)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = " Tapilmadi"
                });                 
                    
            }
            GetCategoryDto dto = new GetCategoryDto()
            {
                Name = category.Name
            };
            return Ok(dto);
        }




        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            Category deletecategory=await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id); 
            _context.Categories.Remove(deletecategory);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
