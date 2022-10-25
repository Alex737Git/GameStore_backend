using System;
using System.Threading.Tasks;
using Contracts;
using Repository.Repositories;

namespace Repository;

    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IGameRepository> _gameRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;
        

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _gameRepository = new Lazy<IGameRepository>(() => new GameRepository(repositoryContext));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(repositoryContext));
           
        }

        public IGameRepository Game => _gameRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public ICommentRepository Comment => _commentRepository.Value;
       
        
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
