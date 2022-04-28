using System.Linq;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Stupify.Models;

namespace Stupify.Services;

public class SongService
{
    private readonly ApplicationContext _context;

    public SongService(ApplicationContext context)
    {
        _context = context;
    }

    public List<Song> GetList()
    {
        return _context.Songs
            .Include(s => s.Artist)
            .ToList();
    }

    public Song Get(int id)
        => _context.Songs
            .Include(s => s!.Artist)
            .FirstOrDefault(s => s.Id == id)!;

    public void Create(Song newSong)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Songs.Add(newSong);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }

    public void Update(Song updatedSong)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Entry(updatedSong).State = EntityState.Modified;
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }

    public void Delete(int id)
    {
        var songToDelete = _context.Songs
            .FirstOrDefault(s => s!.Id == id);
        
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Songs.Remove(songToDelete);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }
}