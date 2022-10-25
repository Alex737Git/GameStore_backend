namespace Entities.Models;

public class Comment:BaseEntity
{
    public Guid? ParentId { get; set; }
    public string? Body { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CommentDate { get; set; }
    public int Level { get; set; }
    
    public Game? Game { get; set; }
    public User? User { get; set; }
    public IList<Comment>? Children { get; set; } = new List<Comment>();
    
}