using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class MusicController : Controller
{
    private readonly SongService _songService;
    private readonly UserService _userService;

    public MusicController(SongService songService, UserService userService)
    {
        _songService = songService;
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Songs = _songService.GetList();
        return View();
    }
    
    [HttpGet("My")]
    public ActionResult My()
    {
        var user = _userService.GetList().FirstOrDefault(u => u.Login == User.Identity.Name);
        if (user == null)
            return Redirect("/Account/Login");
        var likedSongs = user.Likes.Select(l => l.SongId).ToList();
            
        var songs = _songService.GetList().Where(s => likedSongs.Contains(s.Id)).ToList();
        ViewBag.LikedSongs = likedSongs;
        ViewBag.Songs = songs;
        return View("Index");
    }
}