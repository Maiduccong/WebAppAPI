using Microsoft.Data.SqlClient;
using System.Data;

namespace EpicorAPI.DAO
{
    public class DataProvider
    {
        private IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();


        public DataTable ExecuteQuery(string query, object[]? parameter = null)
        {
            var defaultConnection = configuration.GetConnectionString("DataHR");
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(defaultConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.CommandTimeout = 3600;
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[]? parameter = null)
        {
            var defaultConnection = configuration.GetConnectionString("DataHR");
            int data = 0;

            using (SqlConnection connection = new SqlConnection(defaultConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.CommandTimeout = 3600;
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[]? parameter = null)
        {
            var defaultConnection = configuration.GetConnectionString("DataHR");
            object data = 0;

            using (SqlConnection connection = new SqlConnection(defaultConnection))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.CommandTimeout = 3600;
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
