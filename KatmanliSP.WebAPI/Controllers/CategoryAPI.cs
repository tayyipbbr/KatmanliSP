using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.DTOs.ProductDTO;
using KatmanliSP.Core.ResponseMessages;
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
        public IActionResult CreateCategory(CreateCategoryDTO createCategoryDTO)            // IAction 
        {
            try
            {
                _categoryService.AddCategory(createCategoryDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Kategori başarı ile oluşturuldu."
                );

                return Ok(response); // HTTP 200 ve response objesi döner
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Kategori oluşturulamadı."
                );

                return StatusCode(500, response); // HTTP 500 hatası döner
            }
        }

        [HttpDelete("Delete")]   
        public IActionResult DeleteCategory(DeleteCategoryDTO deleteCategoryDTO) 
        {
            try
            {
                _categoryService.DeleteCategory(deleteCategoryDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Kategori silme işlemi başarılı."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Kategori silme başarısız."
                );

                return StatusCode(500, response); 
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateCategory(UpdateCategoryDTO updateCategoryDTO) 
        {
            try
            {
                _categoryService.UpdateCategory(updateCategoryDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Kategori güncelleme işlemi başarılı."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Kategori güncelleme başarısız."
                );

                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCategory()
        {
            try
            {
                _categoryService.GetAllCategory();

                var response = new Response<string>(
                    issuccess: true,
                    message: "Tüm kategoriler çağırıldı."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Kategori çağırma başarısız."
                );

                return StatusCode(500, response);
            }
        }

    }
}
