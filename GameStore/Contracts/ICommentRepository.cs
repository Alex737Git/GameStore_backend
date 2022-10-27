using Entities.Models;

namespace Contracts;

public interface ICommentRepository
{
     Task<IEnumerable<Comment>> GetAllCommentsAsync(Guid gameId, bool trackChanges);
     Task<Comment?> GetCommentAsync(Guid commentId, bool trackChanges);
     Task<IEnumerable<Comment>> GetCommentsByParentIdAsync(Guid commentId, bool trackChanges);
     Task<IEnumerable<Comment>> GetAllCommentsByUser(string userId);
    void CreateComment(Comment comment);
     void DeleteComment(Comment comment);
}