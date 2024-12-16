using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.DTOs.UserDTO;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.DataAccess.Repositories;
using Microsoft.Data.SqlClient;

namespace KatmanliSP.Service.Services
{
    public class UserService
    {
        private readonly ParameterList _parameterList;
        private readonly SqlContactBase _sqlcontactBase;

        public UserService(ParameterList parameterList, SqlContactBase sqlContactBase)
        {
            _parameterList = parameterList;
            _sqlcontactBase = sqlContactBase;
        }

        //geri dönüş tiplerini düzeltelim
        public object AddUser(CreateUserDTO createUserDTO)
        {
            //ParameterList parameterList = new ParameterList();                                  her metotta newlemek bellek şişirebilir. resetleyip kullanıyorum.

            _parameterList.Reset();

            _parameterList.Add("@Name", createUserDTO.Name);                                //parameterList'ten _parameterList'e çevrildi.
            _parameterList.Add("@Lastname", createUserDTO.Lastname);
            // parameterList.Add("@CreatedDate",DateTime.Now);

            var response = _sqlcontactBase.Contact(true, "sp_AddUser", _parameterList);

            return response;
        }

        public void DeleteUser(DeleteUserDTO deleteUserDTO)             // veri tabanından silme değil status 0 yapılmalıdır. 
        {
            _parameterList.Reset();

            _parameterList.Add("@Id", deleteUserDTO.Id);
            // spleri üst calss taki gibi dolduralım
            // return (int)Contact(false,"sp_DeleteCategory",parameters);
        }

        public void UpdateUser(UpdateUserDTO updateUserDTO)
        {
            _parameterList.Reset();

            _parameterList.Add("@Name", updateUserDTO.Name);
            _parameterList.Add("@Lastname", updateUserDTO.Lastname);

            //return (int)Contact(true,"sp_UpdateCategory",parameters);
        }
    }

}