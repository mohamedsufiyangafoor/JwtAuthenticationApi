using JwtAuthenticationApi.Model;
using JwtAuthenticationApi.Model.ViewModels;
using JwtAuthenticationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace JwtAuthenticationApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDetail>> Register(UserVM request)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.AddUserDetails(request);
                return Ok("Registered Successfully");
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginVM request)
        {
            var result = _userService.CheckLogin(request);
            if(result == "No1")
            {
                return BadRequest("User Not Defined");
            }
            if(result == "No2")
            {
                return BadRequest("Wrong Password");
            }
            return result;
            
        }
        

        
    }
}
