using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public UnitOfWork(POSContext context)
        {
            this.context = context;
            CategoryRepository=new CategoryRepository(this.context);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChange()=>
            context.SaveChanges();

        public async Task SaveChangesAsync()=>await context.SaveChangesAsync();
    }
}
