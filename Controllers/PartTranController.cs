using EpicorAPI.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinamAppWebAPI.DAO;

namespace VinamAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartTranController : ControllerBase
    {
        [HttpPost]
        public IActionResult UpdateUser([FromBody]UpdateUserRequest request)
        {
            try
            {
                int rowsAffected = 0;

                if (request.IssueLot == null)
                {
                    request.IssueLot = "";
                }

                if (request.IssueLot != "")
                {
                    string query = "Exec dbo.SP_UpdateIssueLotPartTranUD @TranNum , @IssueLot";
                    rowsAffected = new DataProvider().ExecuteNonQuery(query, new object[] { request.TranNum, request.IssueLot });
                }

                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Success" });
                }
                else
                {
                    return NotFound(new { message = "Not Found. " + " TranNum: " + request.TranNum + " IssueLot: " + request.IssueLot + " rowsAffected : " + rowsAffected});
                }
            }
            catch (Exception ex)
            {
                // X? lý l?i
                return StatusCode(500, new { message = "Error: " + ex.Message });
            }
        }
    }
}
