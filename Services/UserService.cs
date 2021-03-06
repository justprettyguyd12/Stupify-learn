using Microsoft.EntityFrameworkCore;
using Stupify.Models;

namespace Stupify.Services;

public class UserService
{
    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }

    public List<User> GetList() => _context.Users.Include(u => u.Likes).ToList();

    public User Get(int id) => _context.Users
        .Include(u => u.Likes)
        .FirstOrDefault(u => u.Id == id)!;
    
    public void Create(User newUser)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public void Update(User updatedUser)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Entry(updatedUser).State = EntityState.Modified;
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
        var userToDelete = _context.Users
            .FirstOrDefault(x => x.Id == id);

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}