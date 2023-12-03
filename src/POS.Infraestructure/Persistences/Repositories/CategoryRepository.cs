using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly POSContext context;

        public CategoryRepository(POSContext context)
        {
            this.context = context;
        }

        public async Task<Category> CategoryById(int id)
        {
            var categoryId = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
            return categoryId!;
        }

        public async Task<bool> EditCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;
            context.Update(category);
            context.Entry(category).Property(c=>c.AuditCreateUser).IsModified=false;
            context.Entry(category).Property(c=>c.AuditCreateDate).IsModified=false;
            var recordsAffect = await context.SaveChangesAsync();
            return recordsAffect > 0;
        }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            var response=new BaseEntityResponse<Category>();
            var categories=(from c in context.Categories
                            where c.AuditDeleteUser ==null && c.AuditDeleteDate==null
                            select c).AsNoTracking().AsQueryable();
            if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch(filters.NumFilter)
                {
                    case 1:
                        categories = categories.Where(c => c.Name!.Contains(filters.TextFilter));
                        break;
                        case 2:
                        categories = categories.Where(c => c.Description!.Contains(filters.TextFilter));
                        break;
                }
            }
            if(filters.StateFilter is not null)
                categories = categories.Where(c => c.State.Equals(filters.StateFilter));
            if(!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                categories = categories.Where(c=>c.AuditCreateDate>=Convert.ToDateTime(filters.StartDate) && c.AuditCreateDate<=Convert.ToDateTime(filters.EndDate).AddDays(1));
            if (filters.Sort is null) filters.Sort = nameof(Category.CategoryId);
            response.TotalRecords=await categories.CountAsync();
            response.Items=await Ordering(filters,categories,!(bool)filters.Download!).ToListAsync();
            return response;
        }

        public async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var categories = await context.Categories.Where(c => c.State.Equals(1) && c.AuditDeleteDate == null && c.AuditDeleteUser == null).AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<bool> RegisterCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate = DateTime.Now;
            await context.AddAsync(category);
            var recordsAffect=await context.SaveChangesAsync();
            return recordsAffect > 0;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            var category=await context.Categories.AsNoTracking().SingleOrDefaultAsync(c=>c.CategoryId==id);
            category!.AuditDeleteUser= 1;
            category.AuditDeleteDate= DateTime.Now;
            context.Update(category);
            var recordsAffect = await context.SaveChangesAsync();
            return recordsAffect > 0;

        }
    }
}
