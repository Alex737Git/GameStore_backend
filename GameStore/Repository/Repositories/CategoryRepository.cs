using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            var values = await FindAll(trackChanges).ToListAsync();
            return GroupCategories(values, 0);
        }

        private IEnumerable<Category> GroupCategories(IEnumerable<Category> categories, int level)
        {
            var children = categories.Where(c => c.ParentId != null && c.Level == level + 1);

            if (categories.Any(c => c.Level == level + 2))
            {
                children = GroupCategories(categories, level + 1);
            }

            var parents = categories.Where(c => c.Level == level);
            var items = new List<Category>();
            foreach (var category in parents)
            {
                var el = children.Where(c => c.ParentId.Equals(category.Id));
                items.Add(category);
                if (el.Any())
                {
                    foreach (var category1 in el)
                    {
                        items[^1].Children?.Add(category1);
                    }
                }
            }

            return items;
        }


        public async Task<Category?> GetCategoryAsync(Guid categoryId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);

        public async Task<IEnumerable<Category>?> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        
        public void DeleteCategory(Category category) => Delete(category);
      
       
    }
}