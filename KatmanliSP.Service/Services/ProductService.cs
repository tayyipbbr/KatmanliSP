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

        public List<Dictionary<string, object>> AddProduct(CreateProductDTO createProductDTO)
        {
            //ParameterList parameterList = new ParameterList();            her metotta new yerine reset _param kullanıyorum.

            _parameterList.Reset();

            _parameterList.Add("@Name", createProductDTO.Name);
            _parameterList.Add("@Description", createProductDTO.Description);
            _parameterList.Add("@InStock", createProductDTO.InStock);
            // parameterList.Add("@CreatedDate",DateTime.Now);

            var response = _sqlcontactBase.Contact("sp_AddProduct", _parameterList);

            return response;
        }

        public List<Dictionary<string, object>> DeleteProduct(DeleteProductDTO deleteProductDTO)
        {
            _parameterList.Reset();

            
            _parameterList.Add("@Id", deleteProductDTO.Id);
            var response = _sqlcontactBase.Contact("sp_DeleteProduct", _parameterList);

            return response;

        }

        public List<Dictionary<string, object>> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", updateProductDTO.Id);
            _parameterList.Add("@InStock", updateProductDTO.InStock);
            _parameterList.Add("@Name", updateProductDTO.Name);
            _parameterList.Add("@Description", updateProductDTO.Description);

            var response = _sqlcontactBase.Contact("sp_UpdateProduct", _parameterList);

            return response;
        }

        public List<GetAllProductDTO> GetAllProduct()
        {
            _parameterList.Reset();

            var listResult = new List<GetAllProductDTO>();

            var response = _sqlcontactBase.Contact("sp_GetAllProducts", _parameterList);

            if (response != null)
            {
                response.ForEach(x =>
                {
                    var dto = new GetAllProductDTO()
                    {
                        Id = Convert.ToInt32(x["Id"]),
                        Name = x["Name"].ToString(),
                        Description = x["Description"].ToString(),
                        InStock = Convert.ToInt32(x["InStock"])
                    };
                    listResult.Add(dto);
                });
            }

            return listResult;

        }
    }

}


// ÇALIAŞN KOD, response deserialize edielmiyor (ListDic TO string failed)

//public list<getallproductdto> getallproduct()
//{
//    _parameterlist.reset();

//    var response = _sqlcontactbase.contact(true, "sp_getallproducts", _parameterlist);
//    var product = jsonconvert.deserializeobject<list<getallproductdto>>(response);

//    return product;
//}