using POS.Domain.Entities;
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
    }
}
