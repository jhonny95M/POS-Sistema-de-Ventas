using POS.Infraestructure.Commons.Bases;
using POS.Infraestructure.Helpers;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected IQueryable<TResult> Ordering<TResult>(BasePaginationRequest paginationRequest, IQueryable<TResult> queryable, bool pagination = false)where TResult : class
        {
            IQueryable<TResult> queryDto = paginationRequest.Order == "desc" ? queryable.OrderBy($"{paginationRequest.Sort} descending") : queryable.OrderBy($"{paginationRequest.Sort} ascending");
            if (pagination) queryDto = queryDto.Paginate(paginationRequest);
            return queryDto;
        }
    }
}
