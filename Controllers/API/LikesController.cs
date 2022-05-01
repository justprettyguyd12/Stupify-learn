using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;

namespace Stupify.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly UserLikeService _likeService;

    public LikesController(UserLikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet]
    public List<UserLike> Get() => _likeService.GetList();

    [HttpGet("{id:int}")]
    public UserLike Get(int id) => _likeService.Get(id);
    
    [HttpPost]
    public ActionResult<UserLike> Post(UserLike like)
    {
        like.Id = 0;
        _likeService.Create(like);
        return Ok(like);
    }
    
    [HttpPut]
    public ActionResult Put(UserLike like)
    {
        _likeService.Update(like);
        return Ok(like);
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        _likeService.Delete(id);
        return Ok("Артист успешно удален");
    }
}