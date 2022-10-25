namespace Shared.DataTransferObjects.ForShow
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string? Body { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CommentDate { get; set; }
        public int Level { get; set; }
        public string? FirstName { get; set; }
        public string? UserId { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public Guid? ParentId { get; set; }
        public IList<CommentDto>? Children { get; set; } = new List<CommentDto>();
    }
}