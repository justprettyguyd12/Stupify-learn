using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class ArtistsController : Controller
{
    private readonly ArtistService _artistService;
    
    public ArtistsController(ArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Artists = _artistService.GetList();
        return View();
    }

    [HttpGet("{id:int}")]
    public ActionResult Details(int id)
    {
        var artist = _artistService.Get(id);
        ViewBag.Artist = artist;
        ViewBag.Songs = artist.Songs;
        return View();
    }
    
}