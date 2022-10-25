namespace Shared.DataTransferObjects.ForCreation
{
    public class CommentForCreationDto
    {
        public string? Body { get; set; }
        public Guid? ParentId { get; set; }
        public int Level { get; set; }
        public Guid GameId { get; set; }
    }
}