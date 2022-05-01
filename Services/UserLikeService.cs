using Microsoft.EntityFrameworkCore;
using Stupify.Models;

namespace Stupify.Services;

public class UserLikeService
{
    private readonly ApplicationContext _context;

    public UserLikeService(ApplicationContext context)
    {
        this._context = context;
    }

    public List<UserLike> GetList()
    {
        return _context.UserLikes.Include(u => u.Song).ToList();
    }

    public UserLike Get(int id)
    {
        return _context.UserLikes
            .Include(u => u.Song)
            .FirstOrDefault(u => u.Id == id)!;
    }

    public void Create(UserLike newUserLike)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.UserLikes.Add(newUserLike);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public void Update(UserLike updatedUserLike)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Entry(updatedUserLike).State = EntityState.Modified;
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public void Delete(int id)
    {
        var userLikeToDelete = _context.UserLikes
            .FirstOrDefault(x => x.Id == id);

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.UserLikes.Remove(userLikeToDelete);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}