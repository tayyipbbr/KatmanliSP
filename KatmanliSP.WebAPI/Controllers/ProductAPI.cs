using KatmanliSP.Core.DTOs.ProductDTO;
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
        public void CreateProduct(CreateProductDTO createProductDTO)
        {
            _productService.AddProduct(createProductDTO);
        }

        [HttpDelete("Delete")]      
        public void DeleteProduct(DeleteProductDTO deleteProductDTO)
        {
            _productService.DeleteProduct(deleteProductDTO);
        }

        [HttpPut("Update")]
        public void UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            _productService.UpdateProduct(updateProductDTO);
        }

        [HttpGet("GetAll")]
        public List<GetAllProductDTO> GetAllProducts()
        {
            var response = _productService.GetAllProduct();

            return response;
        }
    }
}
