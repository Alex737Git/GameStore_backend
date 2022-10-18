using Microsoft.EntityFrameworkCore;

namespace Entities.Models;

public class Category:BaseEntity
{
    public string? Title { get; set; }
    
    public string? Body { get; set; }

    public Guid? ParentId { get; set; }
   

     public Category? Parent { get; set; }
    public int Level { get; set; }
    public ICollection<Game>? Games { get; set; }

    public IList<Category>? Children { get; set; } = new List<Category>();

}