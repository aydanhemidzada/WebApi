using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.DAL.UnitOfWork.Abstract;
using WebApiConfigurationn.Entities;
using WebApiConfigurationn.Entities.DTOs.Categories;

namespace WebApiConfigurationn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetAllCategories(int page=1, int size=10)
        {
            var category = await _unitOfWork.CategoryRepository.GetAllPaginatedAsync(page, size);
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

            await _unitOfWork.CategoryRepository.AddAsync(category);

            await _unitOfWork.SaveAsync();
            return Ok();

        }


        [HttpPut]
        
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO dto)
        {
            Category validcategory = await _unitOfWork.CategoryRepository.Get(c => c.Id == id);
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
            _unitOfWork.CategoryRepository.Update(validcategory);
            await _unitOfWork.SaveAsync();
            return Ok();

        }



        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            Category category= await _unitOfWork.CategoryRepository.Get(c=>c.Id == id);
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
            Category deletecategory=await _unitOfWork.CategoryRepository.Get(c=>c.Id==id);
            _unitOfWork.CategoryRepository.Delete(deletecategory.Id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
