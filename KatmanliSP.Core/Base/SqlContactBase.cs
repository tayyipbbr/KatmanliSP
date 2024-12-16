using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatmanliSP.Core.DTOs.CategoryDTO;
using KatmanliSP.Core.Entities;
using KatmanliSP.Core.ResponseMessages;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace KatmanliSP.Core.Base
{
    public class SqlContactBase
    {
        //private readonly ParameterList _parameterList;
        private readonly string _connectionString;

        
        public SqlContactBase()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KatmanliSP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }


    
        public string Contact(bool createOrdelete, string spName, ParameterList parameterlist) // TODO: string ve int 2 dönüş sebebi ile object yaptım / int çevirmek gerek?
        {
            if(createOrdelete) 
            {

                var results = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(spName, connection); // TODO: sp name param eklenecek (spName ??)
                    command.CommandType = CommandType.StoredProcedure;

                    foreach(var parameter in parameterlist) // değeri bilmediğin karşılamalar = var
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                row[columnName] = value;
                            }
                            results.Add(row);
                        }
                    }

                    //return JsonConvert.SerializeObject(results);

                    //command.ExecuteNonQuery();
                }

                return JsonConvert.SerializeObject(results);

                //return spName; // TODO: ResponseMessage ekle
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(spName, connection); // TODO: sp name param eklenecek (spName ??)
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameterlist) // değeri bilmediğin karşılamalar = var
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }

                    connection.Open(); 

                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        if (reader.Read()) 
                        {
                            // ilk sütun silinen nesnenin adını içerir.
                            return reader[0].ToString();
                        }
                        else 
                        {
                            return "Nothing deleted.";
                        }
                    }

                    //var rowsAffected = command.ExecuteReader();

                    //return rowsAffected;
                }
            }
        }
    }
}

/*

public string Read(string spName, ParameterList parameterList)
{
    List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        SqlCommand command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;

        foreach (var parameter in parameterList)
        {
            command.Parameters.AddWithValue(parameter.Name, parameter.Value);
        }

        connection.Open();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    row[columnName] = value;
                }
                results.Add(row);
            }
        }
    }

    return JsonConvert.SerializeObject(results);
}
}
}
*/