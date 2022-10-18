using System;

namespace Shared.DataTransferObjects.ForCreation
{
    public class GameForCreationDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public double Price { get; set; }
        public string? PhotoUrl { get; set; }
        public IEnumerable<Guid>? Categories { get; set; }
    }
}