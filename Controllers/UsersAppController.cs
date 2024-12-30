using EpicorAPI.DAO;
using HRWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VinamAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAppController : ControllerBase
    {
        [HttpGet]
        public IActionResult LoginAppCheck(string Username)
        {
            string result = "";

            if (Username == null)
            {
                Username = "";
            }
            if (Username != "")
            {
                string query = "Exec [dbo].[sp_GetTenNVByMaNV] @MaNV";
                result = new DataProvider().ExecuteQuery(query, new object[] {Username}).Rows[0][0].ToString();
            }
            return new JsonResult(new { Name = result });
        }
    }
}
