using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebApiConfigurationn.Core.DAL.Repositories.Concrete.EFCore;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.Entities;

namespace WebApiConfigurationn.DAL.Repositories.Concrete.EFCore
{
    public class EfCategoryRepository : EfBaseRepository<Category, ApiDbContext>, ICategoryRepository

    {
        public EfCategoryRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
