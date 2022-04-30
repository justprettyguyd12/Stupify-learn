using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;

namespace Stupify.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public IEnumerable<User> Get()
    {
        return _userService.GetList();
    }
    
    [HttpGet("{id:int}")]
    public User Get(int id)
    {
        return _userService.Get(id);
    }
    
    [HttpPut]
    public ActionResult Put(User user)
    {
        _userService.Update(user);
        return Ok(user);
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok("Артист успешно удален");
    }
}