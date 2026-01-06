using System.Linq.Expressions;
using WebApiConfigurationn.Entities;

namespace WebApiConfigurationn.DAL.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync(Expression<Func<Category, bool>> filter = null, params string[] includes);
        Task<List<Category>> GetAllPaginatedAsync(int page, int size, Expression<Func<Category, bool>> filter = null, params string[] includes);
        Task<Category> Get(Expression<Func<Category, bool>> filter, params string[] includes);
        Task AddCategoryAsync(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Guid id);
        Task SaveAsync();
        
    }
}  
