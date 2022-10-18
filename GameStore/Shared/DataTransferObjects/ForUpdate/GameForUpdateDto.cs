using System;
using Shared.DataTransferObjects.ForCreation;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class GameForUpdateDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public double Price { get; set; }
        public string? PhotoUrl { get; set; }
         public IEnumerable<Guid>? Categories { get; set; }
        
    }
}