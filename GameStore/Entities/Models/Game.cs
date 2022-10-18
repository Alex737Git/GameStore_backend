namespace Entities.Models;

public class Game:BaseEntity
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public double Price { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime GameDate { get; set; }
    
    public IEnumerable<Category>? Categories { get; set; }
    public User? User { get; set; }

    
}