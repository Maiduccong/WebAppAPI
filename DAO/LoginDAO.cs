using EpicorAPI.DAO;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace HRWeb.Models
{
    public class LoginDAO
    {
        IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

        public int checkUsername(string username)
        {
            int rowcount = 0;
            string queryStr = "exec sp_CheckUserName @ID";
            DataTable dataTable = new DataProvider().ExecuteQuery(queryStr, new object[] {username});
            rowcount = dataTable.Rows.Count;
            return rowcount;
        }

        public string getPassFromUserUsername(string username)
        {
            string pass = "";
            string queryStr = "exec sp_GetPasswordLogin @ID";
            DataTable dataTable = new DataProvider().ExecuteQuery(queryStr, new object[] {username});
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            pass = dataTable.Rows[0]["Password"].ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return pass;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public bool VeryfyHashPassWord(string pass, string user)
        {
            var appSettings = new AppSettings();
            configuration.GetSection("AppSetting").Bind(appSettings);
            string hashTailValue = appSettings.HashTail;

            bool result = false;

            if(GetSHA256Hash(pass + hashTailValue).Equals(getPassFromUserUsername(user)))
            {
                result = true;
            }

            return result;
        }

        public string GetHashPassWord(string pass)
        {
            var appSettings = new AppSettings();
            configuration.GetSection("AppSetting").Bind(appSettings);
            string hashTailValue = appSettings.HashTail;

            return GetSHA256Hash(pass + hashTailValue);
        }

        public string GetNameFromUser(string user)
        {
            string query = "exec sp_GetTenNhanVien @user";
            DataTable dataTable = new DataProvider().ExecuteQuery(query, new object[] {user});
#pragma warning disable CS8603 // Possible null reference return.
            return dataTable.Rows[0]["TenNhanVien"].ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }

        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Chuyển đổi chuỗi thành mảng byte
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Mã hóa SHA-256 và chuyển đổi kết quả thành chuỗi hexa
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
