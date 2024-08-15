using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Data.VO;
using RestAPI.Services;
using System;

namespace RestAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if(user == null) return BadRequest("Invalid Cliente Request");

            var token = _loginService.ReturnUserToken(user);

            if(token == null) return Unauthorized();

            return Ok(token);
        }
    }
}
