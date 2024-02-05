
using Microsoft.AspNetCore.Authorization;
using ComparativeComber.Entities;
using ComparativeComber.Services;



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ComparativeComber.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        try
        {
            var result = await _userService.Register(model);
            var response = new RegisterResponse(result);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public class RegisterResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public RegisterResponse(IdentityResult result)
        {
            Succeeded = result.Succeeded;
            Errors = result.Errors.Select(e => e.Description);
        }
    }
}
