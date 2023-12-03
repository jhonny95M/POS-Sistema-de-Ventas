using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class CategoryRepository: IGenericRepository<Category>, ICategoryRepository
    {
        private readonly POSContext context;

        public CategoryRepository(POSContext context)
        {
            this.context = context;
        }

        public Task<Category> CategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> ListSelectCategories()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
