using BusinessLayer.Autho;
using Comman.Autho;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace APISERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthBL authBL=new AuthBL();
        APIResponse response=new APIResponse();

        [HttpPost]
        [Route("ValidateUser")]
        public IActionResult ValidateUser(UserLogin user)
        {
            try
            {
                var result=authBL.ValidateUser(user);
                if(string.IsNullOrEmpty(result.Item1))
                {
                    response.Message = "User validated successfully!";
                    response.Status = 200;
                    response.Ok = true;
                    response.Data = result.Item2;

                }
                else
                {
                    response.Message = result.Item1;
                    response.Status = -100;
                    response.Ok = false;
                    response.Data = null;

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return Ok(response);
        }
    }
}
