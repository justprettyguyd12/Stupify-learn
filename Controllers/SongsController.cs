using Microsoft.AspNetCore.Mvc;

namespace Stupify.Controllers;

[ApiController]
[Route("[controller]")]
public class SongsController : ControllerBase
{
    private static List<Song> Songs;

    public SongsController()
    {
        Songs = new List<Song>()
        {
            new Song() {Id = 1, Address = "any", Artist = "adele", Name = "hello"},
            new Song() {Id = 2, Address = "any", Artist = "Кровосток", Name = "СШБ"}
        };
    }
    
    [HttpGet]
    public IEnumerable<Song> Get()
    {
        return Songs;
    }

    [HttpGet("{id:int}")]
    public Song Get(int id)
    {
        return Songs.FirstOrDefault(s => s.Id == id)!;
    }
}