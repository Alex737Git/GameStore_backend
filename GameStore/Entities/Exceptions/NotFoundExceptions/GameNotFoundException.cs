using System;
using Entities.Exceptions.Abstract;

namespace Entities.Exceptions.NotFoundExceptions
{
    public sealed class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(Guid postId)
            : base($"The game with id: {postId} doesn't exist in the database.")
        {
        }
    }
}