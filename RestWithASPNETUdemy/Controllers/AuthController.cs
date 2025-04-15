using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Controllers;

[ApiVersion("1")]
[Route("api/[controller]/v{version:apiVersion}")]
[ApiController]
public class AuthController : ControllerBase
{
    private ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult Signin([FromBody] UserDTO userDto)
    {
        if (userDto == null) return BadRequest("Invalid client request");
        var token = _loginService.ValidateCredentials(userDto);
        if (token == null) return Unauthorized();
        return Ok(token);
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh([FromBody] TokenDTO tokenDto)
    {
        if (tokenDto == null) return BadRequest("Invalid client request");
        var token = _loginService.ValidateCredentials(tokenDto);
        if (token == null) return Unauthorized();
        return Ok(token);
    }

    [HttpGet]
    [Route("revoke")]
    [Authorize("Bearer")]
    public IActionResult Revoke()
    {
        var userName = User.Identity.Name;
        var result = _loginService.RevokeToken(userName);

        if (!result) return BadRequest("Invalid client request");
        return NoContent();
    }
}