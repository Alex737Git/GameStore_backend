using System;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class CommentForUpdateDto
    {
        public Guid Id { get; set; }
        public string? Body { get; set; }
        public bool IsDeleted { get; set; }
    }
}