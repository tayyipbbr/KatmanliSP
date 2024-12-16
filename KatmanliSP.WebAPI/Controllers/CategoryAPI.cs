using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KatmanliSP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPI : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryAPI(CategoryService categoryService) { 
        
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public string CreateCategory(CreateCategoryDTO createCategoryDTO) 
        {
            var response = _categoryService.AddCategory(createCategoryDTO);

            return response;
        }

        [HttpDelete("Delete")]   
        public void DeleteCategory(DeleteCategoryDTO deleteCategoryDTO) 
        {
            _categoryService.DeleteCategory(deleteCategoryDTO);
        }

        [HttpPut("Update")]
        public void UpdateCategory(UpdateCategoryDTO updateCategoryDTO) 
        {
            _categoryService.UpdateCategory(updateCategoryDTO);
        }

        [HttpGet("GetAll")]
        public List<GetAllCategoryDTO> GetAllCategory()
        {
            var response = _categoryService.GetAllCategory();

            return response;
        }

    }
}
