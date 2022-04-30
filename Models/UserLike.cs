using System.ComponentModel.DataAnnotations.Schema;

namespace Stupify.Models;

public class UserLike
{
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    [ForeignKey("Songs")]
    public int SongId { get; set; }
    
    public Song Song { get; set; }
}