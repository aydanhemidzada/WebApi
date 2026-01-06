using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.Entities;

namespace WebApiConfigurationn.DAL.Repositories.Concrete.EFCore
{
    public class EfCategoryRepository : ICategoryRepository

    {

        private readonly ApiDbContext _apiDbContext;

        public EfCategoryRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _apiDbContext.Categories.AddAsync(category);
        }


        public void DeleteCategory(Guid id)
        {
            var category = _apiDbContext.Categories.Find(id);

            _apiDbContext.Categories.Remove(category);

        }

        public Task<Category> Get(Expression<Func<Category, bool>> filter, params string[] includes)
        {

            IQueryable<Category> query = GetQuery(includes);
            return query.FirstOrDefaultAsync(filter);
        }

        public Task<List<Category>> GetAllCategoryAsync(Expression<Func<Category, bool>> filter = null, params string[] includes)
        {
            IQueryable<Category> query = GetQuery(includes);

            return filter == null ? query.ToListAsync() : query.Where(filter).ToListAsync();
        }

        public Task<List<Category>> GetAllPaginatedAsync(int page, int size, Expression<Func<Category, bool>> filter = null, params string[] includes)
        {
            IQueryable<Category> query = GetQuery(includes);

            return filter == null
                ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                : query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _apiDbContext.SaveChangesAsync();

        }

        public void UpdateCategory(Category category)
        {
            _apiDbContext.Categories.Update(category);
        }

        IQueryable<Category> GetQuery(string[] includes)
        {
            IQueryable<Category> query = _apiDbContext.Categories;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
