using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.DTOs.ProductDTO;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace KatmanliSP.Service.Services
{
    public class ProductService
    {
        private readonly ParameterList _parameterList;
        private readonly SqlContactBase _sqlcontactBase;

        public ProductService(ParameterList parameterList, SqlContactBase sqlContactBase)
        {
            _parameterList = parameterList;
            _sqlcontactBase = sqlContactBase;
        }

        //TODO: geri dönüş tiplerini düzeltelim MESSAGE vermeliyim, deleted,upped gibi.
        public string AddProduct(CreateProductDTO createProductDTO)
        {
            //ParameterList parameterList = new ParameterList();            her metotta new yerine reset _param kullanıyorum.

            _parameterList.Reset();

            _parameterList.Add("@Name", createProductDTO.Name);
            _parameterList.Add("@Description", createProductDTO.Description);
            _parameterList.Add("@InStock", createProductDTO.InStock);
            // parameterList.Add("@CreatedDate",DateTime.Now);

            var response = _sqlcontactBase.Contact(true, "sp_AddProduct", _parameterList);

            return response;
        }

        public string DeleteProduct(DeleteProductDTO deleteProductDTO)
        {
            _parameterList.Reset();

            
            _parameterList.Add("@Id", deleteProductDTO.Id);
            var response = _sqlcontactBase.Contact(false, "sp_DeleteProduct", _parameterList);

            return response;

        }

        public void UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", updateProductDTO.Id);
            _parameterList.Add("@InStock", updateProductDTO.InStock);
            _parameterList.Add("@Name", updateProductDTO.Name);
            _parameterList.Add("@Description", updateProductDTO.Description);

            _sqlcontactBase.Contact(true, "sp_UpdateProduct", _parameterList);
            //return (int)Contact(true,"sp_UpdateCategory",parameters);
        }

        public List<GetAllProductDTO> GetAllProduct()
        {    
            _parameterList.Reset();

            var response = _sqlcontactBase.Contact(true, "sp_GetAllProducts", _parameterList);
            var product = JsonConvert.DeserializeObject<List<GetAllProductDTO>>(response);

            return product;
        }
    }

}