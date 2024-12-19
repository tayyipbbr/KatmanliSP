using KatmanliSP.Core.DTOs.ProductDTO;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KatmanliSP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        private readonly ProductService _productService ;

        public ProductAPI(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Create")]
        public IActionResult CreateProduct(CreateProductDTO createProductDTO)
        {
            try
            {
                _productService.AddProduct(createProductDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Product başarı ile oluşturuldu."
                );

                return Ok(response); // HTTP 200 ve response objesi döner
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Product oluşturulamadı."
                );

                return StatusCode(500, response); // HTTP 500 hatası döner
            }
        }

        [HttpDelete("Delete")]      
        public IActionResult DeleteProduct(DeleteProductDTO deleteProductDTO)
        {
            try
            {
                _productService.DeleteProduct(deleteProductDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Product silme başarı ile gerçekleşti."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Product silme başasırız."
                );

                return StatusCode(500, response);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            try
            {
                _productService.UpdateProduct(updateProductDTO);

                var response = new Response<string>(
                    issuccess: true,
                    message: "Product güncelleme işlemi başarı ile gerçekleşti."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Product güncelleme başasırız."
                );

                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllProducts()
        {
            try
            {
                _productService.GetAllProduct();

                var response = new Response<string>(
                    issuccess: true,
                    message: "Tüm product'lar çağırıldı."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(
                    issuccess: false,
                    message: "Product çağırma başarısız."
                );

                return StatusCode(500, response);
            }
        }
    }
}

// eski hali

//public List<GetAllProductDTO> GetAllProducts()
//{
//    var response = _productService.GetAllProduct();

//    return response;
//}