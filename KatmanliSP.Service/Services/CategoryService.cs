using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.ResponseMessages;
using KatmanliSP.DataAccess.Repositories;
using Microsoft.Data.SqlClient;

namespace KatmanliSP.Service.Services
{
    public class CategoryService
    {
        private readonly string _connnectionString;
        public CategoryService(string connnectionString)
        {
            _connnectionString = connnectionString;
        }

        public List<GetAllCategoryDTO> GetAllCategories()
        {
            List<GetAllCategoryDTO> Categories = new List<GetAllCategoryDTO>();

            using (SqlConnection connection = new SqlConnection(_connnectionString)) // _conn verisinden bir sql bağlantısı oluşturur.
            {
                SqlCommand command = new SqlCommand("sp_GetAllCategories", connection); // command yani bağlantı açıyoruz,
                command.CommandType = CommandType.StoredProcedure; // command tipini veriyoruz.
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Categories.Add(new GetAllCategoryDTO
                        {
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description"),
                            Id = reader.GetInt32("Id")
                            //Id = Convert.ToInt32(reader["Id"])
                            //Name = reader["Name"].ToString(),
                            //Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            return Categories;
        }

        // Add Category

        public void AddCategory(CreateCategoryDTO category)
        {
            using (SqlConnection connection = new SqlConnection(_connnectionString))
            {
                SqlCommand command = new SqlCommand("sp_AddCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Description", category.Description);

                connection.Open();
                command.ExecuteNonQuery(); // sonuç döndürmeyen - insert,del,up vb. - sorgular için kullanılır.
            }
        }

        // Update Category

        public void UpdateCategory(UpdateCategoryDTO category)
        {
            using (SqlConnection connection = new SqlConnection(_connnectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Description", category.Description);
                command.Parameters.AddWithValue("@Id", category.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCategory(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connnectionString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}