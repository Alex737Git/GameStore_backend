using System;

namespace Shared.DataTransferObjects.ForShow
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public double Price { get; set; }
        public string? PhotoUrl { get; set; }
        public string? OwnerId { get; set; }
        public IEnumerable<string>? Categories { get; set; }
       
    }
}