using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class CommentRepository: RepositoryBase<Comment>, ICommentRepository
{
    public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync(Guid gameId, bool trackChanges)
    {
        var values = await FindByCondition(c => c.Game.Id.Equals(gameId), trackChanges)
            .Include(c => c.User)
            .ToListAsync();
        return GroupComments(values, 0);
    }

    private IEnumerable<Comment> GroupComments(IEnumerable<Comment> comments, int level)
    {
        var children = comments.Where(c => c.ParentId != null && c.Level == level + 1);

        if (comments.Any(c => c.Level == level + 2))
        {
            children = GroupComments(comments, level + 1);
        }

        var parents = comments.Where(c => c.Level == level);
        var items = new List<Comment>();
        foreach (var comment in parents)
        {
            var el = children.Where(c => c.ParentId.Equals(comment.Id));
            items.Add(comment);
            if (el.Any())
            {
                foreach (var comment1 in el)
                {
                    items[^1].Children?.Add(comment1);
                }
            }
        }

        return items;
    }


    public async Task<Comment?> GetCommentAsync(Guid commentId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(commentId), trackChanges)
            .SingleOrDefaultAsync();

     public async Task<IEnumerable<Comment>> GetCommentsByParentIdAsync(Guid commentId, bool trackChanges) =>
        await FindByCondition(c => c.ParentId.Equals(commentId), trackChanges)
            .ToListAsync();

    
    
    public void CreateComment(Comment comment) => Create(comment);
    
    public void DeleteComment(Comment comment) => Delete(comment);

}