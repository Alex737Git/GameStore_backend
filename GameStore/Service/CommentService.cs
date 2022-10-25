using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service
{
    internal sealed class CommentService : ICommentService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CommentService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync(Guid gameId, bool trackChanges)
        {
            
            var comments = await _repository.Comment.GetAllCommentsAsync(gameId, trackChanges);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetCommentAsync(Guid commentId, bool trackChanges)
        {
            var comment = await GetCommentAndCheckIfItExists(commentId, trackChanges);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> CreateCommentAsync(CommentForCreationDto comment, string name)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            commentEntity.CommentDate = DateTime.Now;
            commentEntity.User = await _userManager.FindByNameAsync(name);
            commentEntity.Game = await _repository.Game.GetGameAsync(comment.GameId, true);
           
           
           

            
            
            _repository.Comment.CreateComment(commentEntity);
            var children = await _repository.Comment.GetCommentsByParentIdAsync(commentEntity.Id, true); 
            if (children.Any())
            {
                foreach (var child in children)
                {
                    child.ParentId = commentEntity.Id;
                    child.Level += 1;
                }

            }
            await _repository.SaveAsync();
            
            

            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);

            return commentToReturn;
        }


        public async Task UpdateCommentAsync(
            CommentForUpdateDto commentForUpdate, bool trackChanges)
        {
            var comment = await GetCommentAndCheckIfItExists(commentForUpdate.Id , trackChanges);

            _mapper.Map(commentForUpdate, comment);
            await _repository.SaveAsync();
        }


        public async Task DeleteCommentAsync(Guid commentId, bool trackChanges)
        {
            var comment = await GetCommentAndCheckIfItExists(commentId, trackChanges);
            var children = await _repository.Comment.GetCommentsByParentIdAsync(commentId, true); 
            if (children.Any())
            {
                foreach (var child in children)
                {
                    child.ParentId = comment.ParentId;
                    child.Level = comment.Level;
                }

            }
            
            
            _repository.Comment.DeleteComment(comment);
            await _repository.SaveAsync();
        }

        private async Task<Comment> GetCommentAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var comment = await _repository.Comment.GetCommentAsync(id, trackChanges);
            if (comment is null)
                throw new CommentNotFoundException(id);

            return comment;
        }
        
       
    }
}