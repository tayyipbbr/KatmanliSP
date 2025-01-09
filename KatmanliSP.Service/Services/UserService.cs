using KatmanliSP.Core.Base;
using KatmanliSP.Core.DTOs.UserDTO;
using KatmanliSP.Core.Entities;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.Service.Security;
using Newtonsoft.Json;

namespace KatmanliSP.Service.Services
{
    public class UserService : IUserService
    {
        private readonly ParameterList _parameterList;
        private readonly SqlContactBase _sqlcontactBase;
        private readonly ITokenCreator _tokenCreator;

        public UserService(ParameterList parameterList, 
                           SqlContactBase sqlContactBase,
                           ITokenCreator tokenCreator)
        {
            _parameterList = parameterList;
            _sqlcontactBase = sqlContactBase;
            _tokenCreator = tokenCreator;
        }

        //geri dönüş tiplerini düzeltelim
        //public IResponse<string> AddUser(CreateUserDTO createUserDTO)
        //{
        //    //ParameterList parameterList = new ParameterList();                                  her metotta newlemek bellek şişirebilir. resetleyip kullanıyorum.

        //    _parameterList.Reset();

        //    _parameterList.Add("@Name", createUserDTO.Name);                                //parameterList'ten _parameterList'e çevrildi.
        //    _parameterList.Add("@Lastname", createUserDTO.Lastname);
        //    _parameterList.Add("@Username", createUserDTO.Username);                                //parameterList'ten _parameterList'e çevrildi.
        //    _parameterList.Add("@Password", createUserDTO.Password);
        //    // parameterList.Add("@CreatedDate",DateTime.Now);
        //    _parameterList.Add("@Status", 1);                                //parameterList'ten _parameterList'e çevrildi.
        //    _parameterList.Add("@CreatedBy", 1);
        //    _parameterList.Add("@CreateDate", DateTime.Now);

        //    var response = _sqlcontactBase.Contact("sp_AddUser", _parameterList);

        //    if (response != null)
        //    {
        //        return new Response<int>
        //        {
        //            IsSuccess = true,
        //            Message = "Kullanıcı başarıyla oluşturuldu.",
        //            Data = response // Oluşturulan kullanıcının ID'si
        //        };
        //    }

        //    return response;
        //}

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

        // TODO: KULLANICI VAR MI? KONTROLÜ EKLENMELİ

        public IResponse<string> Register(CreateUserDTO createUserDTO)
        {
            string hashedPassword = _tokenCreator.GenerateHashPassword(createUserDTO.Password);
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@Name", createUserDTO.Name);                                //parameterList'ten _parameterList'e çevrildi.
                _parameterList.Add("@Lastname", createUserDTO.Lastname);
                _parameterList.Add("@Username", createUserDTO.Username);                                //parameterList'ten _parameterList'e çevrildi.
                _parameterList.Add("@Password", hashedPassword);
                // parameterList.Add("@CreatedDate",DateTime.Now);
                _parameterList.Add("@Status", 1);                                //parameterList'ten _parameterList'e çevrildi.
                _parameterList.Add("@CreatedBy", 1);
                _parameterList.Add("@CreateDate", DateTime.Now);

                var returnedUsereId = _sqlcontactBase.Contact("sp_RegisterUser", _parameterList);

                int catchId = Convert.ToInt32(returnedUsereId[0]["UserId"]);

                //return new Successful<string>("Kullanıcı başarı ile oluşturuldu.");

                _parameterList.Reset();
                _parameterList.Add("@UserId", catchId); // sp_RegisterUser'dan dönen ID
                _parameterList.Add("@RoleId", 2); // 2 = User
                _sqlcontactBase.Contact("sp_AssignRoleToUser", _parameterList);

                return new Response<string>(true)
                {
                    Issuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Failed<string>(ex.Message);
            }
        }

        // hepsi DTO dönmeli, ortak bir yapı olmalı.
        // bu yapı superadmin tarafından yeni bir SP ile role değiştirme işlemini yapacak şekilde düzenlenebilir.
        public UserRoleDTO AssignRole(int userId, int roleId)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@UserId", userId);
                _parameterList.Add("@RoleId", roleId);

                _sqlcontactBase.Contact("sp_AssignRoleToUser", _parameterList);
                return null; // new Successful<string>("Rol atama başarılı.");
            }
            catch (Exception ex)
            {
                //return new Failed<string>(ex.Message);
                return null; 
            }
        }
        // ortak DTO'da Token dönmeli, 
        public IResponse<UserRoleDTO> Login(LoginUserDTO loginUserDTO) // returnler string message'a çevrilecek ve IResponse içi de değişecek.
        {

            string hashedPassword = _tokenCreator.GenerateHashPassword(loginUserDTO.Password);

            _parameterList.Reset();

            _parameterList.Add("@Username", loginUserDTO.Username);
            _parameterList.Add("@Password", hashedPassword);

            //var listResult = new UserRoleDTO();

            //TODO: username veya şifre hatasında patlıyor- usercontrol çektim if içine (tek kontrole alınmalı)
            var user = _sqlcontactBase.Contact("sp_LoginUser", _parameterList); // dönen userId burada kullanılacak mı?

            var userControl = user[0].Any(x => x.Value == null);

            //if (!userControl)
            //{
            //    user.ForEach(x =>
            //    {
            //        var idto = new OnlineUserDTO()
            //        {
            //            UserId = Convert.ToInt32(x["UserId"]),
            //            Username = x["Username"].ToString()
            //        };
            //    });

            //}

            
            
            //onlineuserDTO'dan userId gelir
            //userId kullanılır ve userId'den roleId'de erişilir.



            if (!userControl) // TODO: && user.userId > 0 yapamadim, user'da bir erisimim yok !!! 
            {

                var idto = new OnlineUserDTO()
                {
                    UserId = Convert.ToInt32(user[0]["UserId"]),
                    Username = user[0]["Username"].ToString(),
                    RoleId = Convert.ToInt32(user[0]["RoleId"])
                };

                // role bilgisi gelecek (DTO veya listINT ile)
                // daha sonra var roles de tokene input olarak verilebilir.



                
                string token = _tokenCreator.GenerateToken(loginUserDTO.Username, idto.UserId, idto.RoleId); // token'e ne gömülmeli? 
                //roleid tokene yollanacak
                var dto = new UserRoleDTO()
                {
                    Token = token,
                    Username = loginUserDTO.Username
                };

                // JOIN ile yukarıda tek SP'den RoleId'de alındı -UserRoles klasörü joinlendi-

                //_parameterList.Reset();

                //_parameterList.Add("@UserId", idto.UserId);

                //var roleList = _sqlcontactBase.Contact("sp_GetUserRoles", _parameterList);

                //// giriş yapan kullanıcıyı ekrana basmak için ayarlayabilirim.

                //roleList.ForEach(x =>
                //{
                //    var roleId = Convert.ToInt32(x["RoleId"]);
                //    var roles = new UserRoleDTO()
                //    {
                //        RoleId = roleId,
                //        Rolename = roleId == 2 ? "User" :
                //                   roleId == 3 ? "Admin" : "Unknown"
                //    };
                //});



                return new Core.ResponseMessages.Response<UserRoleDTO>(true, dto)
                {
                    Issuccess = true,
                    Message = "Login successful",
                    Data = dto
                };

            //var result = new Successful<string>(token);

            //return result;

            /*
            var result = new OnlineUserDTO()
            {
                UserId = Convert.ToInt32(user[0]["UserId"]),
                Username = user[0]["Username"].ToString()
            };
            */
            //var roles = GetUserRoles(); // user.Id çekmem gerekiyor. >> ROLLERİ çekebilmek için. OnlineUserDTO'da Id var

            // MESAJA GEREK YOK, Data yani result geldi ise API'de if ile success mesajı verebilirim. !!!!!!

            // return new Successful<LoginUserDTO>(result,"Başarılı bir şekilde giriş yapıldı."); // data yani user doner mi? nasil doner?
            }
            else
            {
                return new Core.ResponseMessages.Response<UserRoleDTO>(false, "fail");

            }
        }

        public List<UserRoleDTO> GetUserRoles(int userId)  // TODO: roles char donuyor?????
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@UserId", userId);
                var listResult = new List<UserRoleDTO>();

                var roles = _sqlcontactBase.Contact("sp_GetUserRoles", _parameterList); // alt return kapattim ve tolist ekledim.

                if (roles != null)
                {
                    roles.ForEach(x =>
                    {
                        var dto = new UserRoleDTO()
                        {
                            RoleId = Convert.ToInt32(roles[0]["RoleId"])
                        };
                        listResult.Add(dto);
                    });
                }
                return listResult;
            }
            catch (Exception)
            {
                return null;       // ????
            }

            // listDTO yerine listINT de denenebilir.
        }

    }

}