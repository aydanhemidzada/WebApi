using WebApiConfigurationn.Core.DAL.Repositories.Concrete.EFCore;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.Entities;

namespace WebApiConfigurationn.DAL.Repositories.Concrete.EFCore
{
    public class EfProductRepository : EfBaseRepository<Product, ApiDbContext>, IProductRepository
    {
        public EfProductRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
