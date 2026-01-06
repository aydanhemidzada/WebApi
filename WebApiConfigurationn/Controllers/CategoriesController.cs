using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Categories;

namespace WebApiConfigurationn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository)
        {

            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories(int page=1, int size=10)
        {
            var category = await _categoryRepository.GetAllPaginatedAsync(page, size, null);
            var result = _mapper.Map < List<GetCategoryDto>>(category);

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

            await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveAsync();
            return Ok();

        }


        [HttpPut]
        
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO dto)
        {
            Category validcategory = await _categoryRepository.Get(c => c.Id == id);
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
            _categoryRepository.Update(validcategory);
            await _categoryRepository.SaveAsync();
            return Ok();

        }



        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            Category category= await _categoryRepository.Get(c=>c.Id == id);
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
            Category deletecategory=await _categoryRepository.Get(c=>c.Id==id); 
            _categoryRepository.Delete(deletecategory.Id);
            await _categoryRepository.SaveAsync();
            return Ok();
        }
    }
}
