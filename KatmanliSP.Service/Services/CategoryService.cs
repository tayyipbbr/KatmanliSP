using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace KatmanliSP.Service.Services
{
    public class CategoryService 
    {
        private readonly ParameterList _parameterList;
        private readonly SqlContactBase _sqlcontactBase;

        public CategoryService(ParameterList parameterList, SqlContactBase sqlContactBase)
        {
            _parameterList = parameterList; 
            _sqlcontactBase = sqlContactBase;
        }

        //geri dönüş tiplerini düzeltelim
        public string AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            //ParameterList parameterList = new ParameterList();                                  her metotta newlemek bellek şişirebilir. resetleyip kullanıyorum.

            _parameterList.Reset();

            _parameterList.Add("@Name", createCategoryDTO.Name);                                //parameterList'ten _parameterList'e çevrildi.
            _parameterList.Add("@Description", createCategoryDTO.Description);
           // parameterList.Add("@CreatedDate",DateTime.Now);

           var response = _sqlcontactBase.Contact(true, "sp_AddCategory", _parameterList);

            return response;
        }

        public string DeleteCategory(DeleteCategoryDTO deleteCategoryDTO) 
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", deleteCategoryDTO.Id);
            // spleri üst calss taki gibi doldur
            var response = _sqlcontactBase.Contact(false, "sp_DeleteCategory", _parameterList);

            return response;
        }

        public void UpdateCategory(UpdateCategoryDTO updateCategoryDTO) 
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", updateCategoryDTO.Id);
            _parameterList.Add("@Name", updateCategoryDTO.Name);
            _parameterList.Add("@Description", updateCategoryDTO.Description);

            var response = _sqlcontactBase.Contact(true,"sp_UpdateCategory",_parameterList);
        }

        public List<GetAllCategoryDTO> GetAllCategory() 
        {
            _parameterList.Reset();

            var response = _sqlcontactBase.Contact(true, "sp_GetAllCategories", _parameterList);

            var categories = JsonConvert.DeserializeObject<List<GetAllCategoryDTO>>(response);

            return categories;
        }
    }

}