using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;

namespace Stupify.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly ArtistService _artistService;

    public ArtistsController(ArtistService artistService)
    {
        _artistService = artistService;
    }
    
    /// <summary>
    /// Получить список артистов
    /// </summary>
    [HttpGet]
    public IEnumerable<Artist> Get() => _artistService.GetList();

    /// <summary>
    /// Получить песню по id
    /// </summary>
    /// <param name="id">Идентификатор песни</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public Artist Get(int id) => _artistService.Get(id);

    /// <summary>
    /// Создание артиста
    /// </summary>
    /// <param name="artist"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<Artist> Post(Artist artist)
    {
        artist.Id = 0;
        _artistService.Create(artist);
        return Ok(artist);
    }

    /// <summary>
    /// Обновление артиста
    /// </summary>
    /// <param name="artist">Параметры артиста</param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult Put(Artist artist)
    {
        _artistService.Update(artist);
        return Ok(artist);
    }

    /// <summary>
    /// Удаление песни
    /// </summary>
    /// <param name="id">Идентификатор артиста</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _artistService.Delete(id);
        return Ok("Артист успешно удален");
    }
}