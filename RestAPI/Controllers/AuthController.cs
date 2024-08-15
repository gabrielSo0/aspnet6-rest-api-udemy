using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
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
            if(user == null) return BadRequest("Invalid Client Request");

            var token = _loginService.ReturnUserToken(user);

            if(token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid Client Request");

            var token = _loginService.ReturnUserToken(tokenVO);

            if (token == null) return BadRequest("Invalid Client Request");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;

            var result = _loginService.RevokeToken(userName);

            if (!result) return BadRequest("Invalid Client Request");
            return NoContent();
        }
    }
}
