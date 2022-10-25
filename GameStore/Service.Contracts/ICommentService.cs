using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service.Contracts;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllCommentsAsync(Guid gameId, bool trackChanges);
    Task<CommentDto> GetCommentAsync(Guid commentId, bool trackChanges);

    Task<CommentDto> CreateCommentAsync(CommentForCreationDto comment, string name);

    Task DeleteCommentAsync(Guid commentId, bool trackChanges);

    Task UpdateCommentAsync( CommentForUpdateDto commentForUpdate, bool trackChanges);
}