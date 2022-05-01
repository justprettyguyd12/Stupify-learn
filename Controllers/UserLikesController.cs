using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;

namespace Stupify.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class UserLikesController : Controller
{
    private readonly UserLikeService _likeService;
    private readonly UserService _userService;
    
    public UserLikesController(UserLikeService likeService, UserService userService)
    {
        _likeService = likeService;
        _userService = userService;
    }

    [HttpPost("Like/{songId:int}")]
    public IActionResult Like(int songId)
    {
        var user = _userService.GetList().FirstOrDefault(u => u.Login == User.Identity!.Name);
        if (user == null)
            return BadRequest();
        var like = new UserLike {UserId = user.Id, SongId = songId};
        _likeService.Create(like);
        return Ok();
    }

    [HttpPost("Dislike/{songId:int}")]
    public IActionResult Dislike(int songId)
    {
        var user = _userService.GetList().FirstOrDefault(u => u.Login == User.Identity!.Name);
        if (user == null)
            return BadRequest();
        var like = _likeService.GetList().FirstOrDefault(l => l.UserId == user.Id && l.SongId == songId);
        if (like == null)
            return BadRequest();
        return Ok();
    }
}