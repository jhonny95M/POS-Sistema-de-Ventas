using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Helpers;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace POS.Infraestructure.Persistences.Repositories;

public class GenericRepository<T>: IGenericRepository<T> where T : BaseEntity
{
    private readonly POSContext context;
    private readonly DbSet<T> entity;

    public GenericRepository(POSContext context)
    {
        this.context = context;
        this.entity = this.context.Set<T>();
    }

    public async Task<bool> EditAsync(T entity)
    {
        entity.AuditUpdateUser = 1;
        entity.AuditUpdateDate = DateTime.Now;
        context.Update(entity);
        context.Entry(entity).Property(c => c.AuditCreateUser).IsModified = false;
        context.Entry(entity).Property(c => c.AuditCreateDate).IsModified = false;
        var recordsAffect = await context.SaveChangesAsync();
        return recordsAffect > 0;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var getAll=await entity
            .Where(c=>c.State.Equals((int)StateTypes.Active) && c.AuditDeleteDate==null && c.AuditDeleteUser==null)
            .AsNoTracking().ToListAsync();
        return getAll;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var getById=await entity.AsNoTracking().SingleOrDefaultAsync(c=>c.Id==id);
        return getById!;
    }

    public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = entity;
        if(filter != null)query = query.Where(filter);
        return query;
    }

    public async Task<bool> RegisterAsync(T entity)
    {
        entity.AuditCreateUser = 1;
        entity.AuditCreateDate = DateTime.Now;
        await context.AddAsync(entity);
        var recordsAffect = await context.SaveChangesAsync();
        return recordsAffect > 0;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        entity!.AuditDeleteUser = 1;
        entity.AuditDeleteDate = DateTime.Now;
        context.Update(entity);
        var recordsAffect = await context.SaveChangesAsync();
        return recordsAffect > 0;
    }

    public IQueryable<TResult> Ordering<TResult>(BasePaginationRequest paginationRequest, IQueryable<TResult> queryable, bool pagination = false)where TResult : class
    {
        IQueryable<TResult> queryDto = paginationRequest.Order == "desc" ? queryable.OrderBy($"{paginationRequest.Sort} descending") : queryable.OrderBy($"{paginationRequest.Sort} ascending");
        if (pagination) queryDto = queryDto.Paginate(paginationRequest);
        return queryDto;
    }
}
