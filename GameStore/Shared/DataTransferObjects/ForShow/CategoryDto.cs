using System;

namespace Shared.DataTransferObjects.ForShow
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
    
        public string? Body { get; set; }
		 public int Level { get; set; }
        public IList<CategoryDto>? Children { get; set; } = new List<CategoryDto>();
    }
}