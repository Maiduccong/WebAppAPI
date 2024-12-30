using EpicorAPI.DAO;
using HRWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VinamAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAppController : ControllerBase
    {
        [HttpPost]
        public IActionResult LoginAppCheck(string Username, string Password)
        {
            bool result = false;

            if (Username == null)
            {
                Username = "";
            }
            if (Password == null)
            {
                Password = "";
            }
            if (Username != "" || Password != "")
            {
                if (new LoginDAO().checkUsername(Username) > 0)
                {
                    if (new LoginDAO().VeryfyHashPassWord(Password, Username))
                    {
                        // Trả về một JsonResult có giá trị là true
                        result = true;
                    }
                }
            }
            return new JsonResult(new { Success = result });
        }
    }
}
