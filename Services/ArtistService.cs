using Microsoft.EntityFrameworkCore;
using Stupify.Models;

namespace Stupify.Services;

public class ArtistService
{
    private readonly ApplicationContext _context;

    public ArtistService(ApplicationContext context)
    {
        _context = context;
    }

    public List<Artist> GetList()
    {
        return _context.Artists.Include(a => a.Songs).ToList();
    }

    public Artist Get(int id)
    {
        return _context.Artists.Include(a => a!.Songs)
            .FirstOrDefault(a => a.Id == id)!;
    }

    public void Create(Artist newArtist)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Artists.Add(newArtist);
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public void Update(Artist updatedArtist)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            _context.Entry(updatedArtist).State = EntityState.Modified;        
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
        var artistToDelete = _context.Artists.FirstOrDefault(a => a.Id == id);
        
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Artists.Remove(artistToDelete);       
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}