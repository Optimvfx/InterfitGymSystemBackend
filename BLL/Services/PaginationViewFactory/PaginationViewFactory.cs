using AutoMapper;
using Common.Models;
using Common.Models.PaginationView;

namespace BLL.Services.PaginationViewFactory;

public class PaginationViewFactory : IPaginationViewFactory
{
    private readonly int ValuesPerPage;
    private readonly IMapper _mapper;

    public PaginationViewFactory(int valuesPerPage, IMapper mapper)
    {
        ValuesPerPage = valuesPerPage;
        _mapper = mapper;
    }

    public BasePaginationView<T> CreatePaginationView<T>(IQueryable<T> queryable)
    {
        return new PaginationView<T>(queryable, ValuesPerPage);
    }
    
    public BasePaginationView<TOutput> CreatePaginationView<TInput, TOutput>(IQueryable<TInput> queryable)
    {
        return CreatePaginationView(queryable, e => _mapper.Map<TOutput>(e));
    }
    
    public BasePaginationView<TOutput> CreatePaginationView<TInput, TOutput>(IQueryable<TInput> queryable, Func<TInput, TOutput> convertionFunction)
    {
        return new PaginationView<TInput, TOutput>(queryable, ValuesPerPage, convertionFunction);
    }
}