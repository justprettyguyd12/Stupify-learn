using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class MusicController : Controller
{
    private readonly SongService _songService;

    public MusicController(SongService songService)
    {
        _songService = songService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Songs = _songService.GetList();
        return View();
    }
}