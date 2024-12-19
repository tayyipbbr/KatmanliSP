using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.DTOs.UserDTO;
using KatmanliSP.Core.Entities;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace KatmanliSP.Service.Services
{
    public class UserService : IUserService
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

            var response = _sqlcontactBase.Contact("sp_AddUser", _parameterList);

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

        /*
         * TODO: INTERFACE'den login metot alındı, değişiklik kontrol edilmeli.
         * 
        public IResponse<LoginUserDTO> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@Username", loginUserDTO.Username);
                _parameterList.Add("@Password", loginUserDTO.Password);

                var response = _sqlcontactBase.Contact(true, "sp_LoginUser", _parameterList);
                var user = JsonConvert.DeserializeObject<LoginUserDTO>(response);

                if (user != null)
                {
                    return new Successful<LoginUserDTO>(user);
                }
                return new Failed<LoginUserDTO>("Kullanıcı adı veya şifre hatalı.");
            }
            catch (Exception ex)
            {
                return new Failed<LoginUserDTO>(ex.Message);
            }
        }

        */


        public IResponse<string> Register(RegisterUserDTO registerUserDTO)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@Username", registerUserDTO.Username);
                _parameterList.Add("@Password", registerUserDTO.Password);

                _sqlcontactBase.Contact("sp_RegisterUser", _parameterList);

                return new Successful<string>("Kullanıcı girişi başarılı.");
            }
            catch (Exception ex)
            {
                return new Failed<string>(ex.Message);
            }
        }

        public IResponse<string> AssignRole(int userId, int roleId)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@UserId", userId);
                _parameterList.Add("@RoleId", roleId);

                _sqlcontactBase.Contact("sp_AssignRoleToUser", _parameterList);
                return new Successful<string>("Rol atama başarılı.");
            }
            catch (Exception ex)
            {
                return new Failed<string>(ex.Message);
            }
        }

        public IResponse<OnlineUserDTO> Login(LoginUserDTO loginUserDTO) // returnler string message'a çevrilecek ve IResponse içi de değişecek.
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@Username", loginUserDTO.Username);
                _parameterList.Add("@Password", loginUserDTO.Password);

                var user = _sqlcontactBase.Contact("sp_LoginUser", _parameterList);

                if (user != null) // TODO: && user.userId > 0 yapamadim, user'da bir erisimim yok !!! 
                {
                    var result = new OnlineUserDTO()
                    {
                        UserId = Convert.ToInt32(user[0]["UserId"]),
                        Username = user[0]["Username"].ToString()
                    };
                    //var roles = GetUserRoles(); // user.Id çekmem gerekiyor. >> ROLLERİ çekebilmek için. OnlineUserDTO'da Id var

                    // MESAJA GEREK YOK, Data yani result geldi ise API'de if ile success mesajı verebilirim. !!!!!!

                    return new Successful<OnlineUserDTO>(result,"Başarılı bir şekilde giriş yapıldı."); // data yani user doner mi? nasil doner?
                }
                else
                {
                    return new Failed<OnlineUserDTO>("Kullanıcı adı veya şifre yanlış.");
                }
            }
            catch (Exception ex)
            {
                return new Failed<OnlineUserDTO>($"Error during login: {ex.Message}");
            }
        }

        public List<char> GetUserRoles(int userId)  // TODO: roles char donuyor?????
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@UserId", userId);

                var roles = _sqlcontactBase.Contact("sp_GetUserRoles", _parameterList).ToList(); // alt return kapattim ve tolist ekledim.

                return new List<char>();

                //return roles?.Select(role => role.ToString()).ToList() ?? new List<string>(); //ToList() ?? new List<string>(); // = role?. sonrasi
            }
            catch (Exception)
            {
                return new List<char>();        // <string> 'i <char>' a cevirdim.
            }
        }
    }

}