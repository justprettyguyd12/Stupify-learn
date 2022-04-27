using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Stupify.Services;

public class SongService
{
    private readonly ApplicationContext context;

    public List<Song> GetList() => context.Songs.ToList();

    public Song Get(int id)
        => context.Songs
            .FirstOrDefault(s => s.Id == id);

    public void Create(Song newSong)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            context.Songs.Add(newSong);
            context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }

    public void Update(Song updatedSong)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            context.Entry(updatedSong).State = EntityState.Modified;
            context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }

    public void Delete(int id)
    {
        var songToDelete = context.Songs
            .FirstOrDefault(s => s.Id == id);
        
        using var transaction = context.Database.BeginTransaction();
        try
        {
            context.Songs.Remove(songToDelete);
            context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
        }
    }
}