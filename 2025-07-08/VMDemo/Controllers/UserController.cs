using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMDemo.Models;
using VMDemo.Services;

namespace VMDemo.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> ViewAll()
    {
        List<User> users;
        try
        {
            users = await _userService.ViewAll();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        return Ok(users);
    }
    [HttpPost]
    public async Task<ActionResult<User>> Add(string name)
    {
        User user;
        try
        {
            user = await _userService.Add(name);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        return Ok(user);
    }
}