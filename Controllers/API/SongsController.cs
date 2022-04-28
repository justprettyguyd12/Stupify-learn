using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;

namespace Stupify.Controllers;

[ApiController]
[Route("[controller]")]
public class SongsController : ControllerBase
{
    private readonly SongService songService;

    public SongsController(SongService songService)
    {
        this.songService = songService;
    }
    
    /// <summary>
    /// Получить полный писок песен
    /// </summary>
    [HttpGet]
    public IEnumerable<Song?> Get()
    {
        return songService.GetList();
    }

    /// <summary>
    /// Получить песню по id
    /// </summary>
    /// <param name="id">Идентификатор песни</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public Song Get(int id)
    {
        return songService.Get(id);
    }

    /// <summary>
    /// Создание песни
    /// </summary>
    /// <param name="song"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<Song> Post(Song song)
    {
        song.Id = 0;
        songService.Create(song);
        return Ok(song);
    }

    /// <summary>
    /// Обновление песни
    /// </summary>
    /// <param name="song">Параметры песни</param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult Put(Song song)
    {
        songService.Update(song);
        return Ok(song);
    }

    /// <summary>
    /// Удаление песни
    /// </summary>
    /// <param name="id">Идентификатор песни</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        songService.Delete(id);
        return Ok("Песня успешно удалена");
    }
}