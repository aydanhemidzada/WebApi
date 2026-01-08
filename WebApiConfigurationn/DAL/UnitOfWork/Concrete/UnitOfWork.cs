using Microsoft.EntityFrameworkCore;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.DAL.Repositories.Concrete.EFCore;
using WebApiConfigurationn.DAL.UnitOfWork.Abstract;

namespace WebApiConfigurationn.DAL.UnitOfWork.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApiDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new EfCategoryRepository(_context);

        public IProductRepository ProductRepository => _productRepository ?? new EfProductRepository(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
