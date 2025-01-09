using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;

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
        public List<Dictionary<string, object>> AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            //ParameterList parameterList = new ParameterList();                                  her metotta newlemek bellek şişirebilir. resetleyip kullanıyorum.

            _parameterList.Reset();

            _parameterList.Add("@Name", createCategoryDTO.Name);                                //parameterList'ten _parameterList'e çevrildi.
            _parameterList.Add("@Description", createCategoryDTO.Description);
            // parameterList.Add("@CreatedDate",DateTime.Now); SPDE YAPILIR

            var response = _sqlcontactBase.Contact("sp_AddCategory", _parameterList);

            return response;
        }

        public List<Dictionary<string, object>> DeleteCategory(DeleteCategoryDTO deleteCategoryDTO) 
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", deleteCategoryDTO.Id);

            var response = _sqlcontactBase.Contact("sp_DeleteCategory", _parameterList);

            return response;
        }

        public List<Dictionary<string, object>> UpdateCategory(UpdateCategoryDTO updateCategoryDTO) 
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", updateCategoryDTO.Id);
            _parameterList.Add("@Name", updateCategoryDTO.Name);
            _parameterList.Add("@Description", updateCategoryDTO.Description);

            var response = _sqlcontactBase.Contact("sp_UpdateCategory",_parameterList);

            return response;
        }

        public List<GetAllCategoryDTO> GetAllCategory()
        {
            _parameterList.Reset();

            var listResult = new List<GetAllCategoryDTO>();

            var response = _sqlcontactBase.Contact("sp_GetAllCategories", _parameterList);

            if (response != null)
            {
                response.ForEach(x =>
                {
                    var dto = new GetAllCategoryDTO()
                    {
                        Name = x["Name"].ToString(),
                        Id = Convert.ToInt32(x["Id"]),
                        Description = x["Description"].ToString()
                    };

                    listResult.Add(dto);
                });
            }

            return listResult;

            // listResult DTO'ları sorunsuz dönüyor. API'den çekilecek.
        }
    }
    
}