namespace Stupify.Models;

public class Artist
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<Song> Songs { get; set; }

    public Artist()
    {
        Songs = new List<Song>();
    }
}

