using WebApiConfigurationn.DAL.Repositories.Abstract;

namespace WebApiConfigurationn.DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository  { get; }

        Task SaveAsync();

    }
}
