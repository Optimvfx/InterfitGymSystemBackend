using Common.Models;
using Common.Models.PaginationView;

namespace BLL.Services.PaginationViewFactory;

public interface IPaginationViewFactory
{
    BasePaginationView<T> CreatePaginationView<T>(IQueryable<T> queryable);

    BasePaginationView<TOutput> CreatePaginationView<TInput, TOutput>(IQueryable<TInput> queryable,
        Func<TInput, TOutput> convertionFunction);
    
    BasePaginationView<TOutput> CreatePaginationView<TInput, TOutput>(IQueryable<TInput> queryable);
}