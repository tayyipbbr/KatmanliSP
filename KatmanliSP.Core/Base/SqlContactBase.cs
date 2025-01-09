using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace KatmanliSP.Core.Base
{
    public class SqlContactBase
    {
        private readonly string _connectionString;

        
        public SqlContactBase()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KatmanliSP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }


    
        public List<Dictionary<string, object>> Contact(string spName, ParameterList parameterlist) // TODO: string ve int 2 dönüş sebebi ile object yaptım / int çevirmek gerek?
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

                }
                return results;
        }
    }
}