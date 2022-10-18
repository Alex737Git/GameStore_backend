using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
        Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category);

        Task DeleteCategoryAsync(Guid categoryId, bool trackChanges);

        Task UpdateCategoryAsync( CategoryForUpdateDto categoryForUpdate, bool trackChanges);
        Task<IEnumerable<CategoryDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        
    }
}