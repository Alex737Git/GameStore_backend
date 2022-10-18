using System.Threading.Tasks;
using Contracts;

namespace Contracts;

    public interface IRepositoryManager
    {
        IGameRepository Game { get; }
        ICategoryRepository Category { get; }
       
        Task SaveAsync();
    }
