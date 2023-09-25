namespace Common.Models.PaginationView;

public class PaginationView<T> : BasePaginationView<T>
{
    public readonly int PagesCount;
    public readonly int ValuesPerPage;
    
    private readonly IQueryable<T> _queryable;
    
    public PaginationView(IQueryable<T> queryable, int valuesPerPage)
    {
        if (valuesPerPage <= 0)
            throw new ArgumentException();
        
        _queryable = queryable;
        ValuesPerPage = valuesPerPage;
        PagesCount = GetPagesCountByTotalCount(_queryable.Count(), valuesPerPage);
    }
    
    private int GetPagesCountByTotalCount(int count, int valuesPerPage)
    {
        var fullpages = count / valuesPerPage;
        var left = count % valuesPerPage;

        if (left > 0)
            fullpages++;

        return fullpages;
    }

    protected override IEnumerable<T> GetByPage(uint page)
    {
        return _queryable.Skip((int)(page * ValuesPerPage))
            .Take(ValuesPerPage);
    }

    public override ValueRange<uint> GetPagesRange()
    {
        return new ValueRange<uint>(0, (uint)PagesCount);
    }
}

public class PaginationView<TInput, TOutput> : BasePaginationView<TOutput>
{
    public readonly int PagesCount;
    public readonly int ValuesPerPage;
    
    private readonly IQueryable<TInput> _queryable;

    private readonly Func<TInput, TOutput> _convertionFunction;

    public PaginationView(IQueryable<TInput> queryable, int valuesPerPage, Func<TInput, TOutput> convertionFunction)
    {
        if (valuesPerPage <= 0)
            throw new ArgumentException();
        
        _queryable = queryable;
        _convertionFunction = convertionFunction;
        
        ValuesPerPage = valuesPerPage;
        PagesCount = GetPagesCountByTotalCount(_queryable.Count(), valuesPerPage);
    }

    private int GetPagesCountByTotalCount(int count, int valuesPerPage)
    {
        var fullpages = count / valuesPerPage;
        var left = count % valuesPerPage;

        if (left > 0)
            fullpages++;

        return fullpages;
    }

    protected override IEnumerable<TOutput> GetByPage(uint page)
    {
        return _queryable.Skip((int)(page * ValuesPerPage))
            .Take(ValuesPerPage)
            .Select(e => _convertionFunction(e));
    }

    public override ValueRange<uint> GetPagesRange()
    {
        return new ValueRange<uint>(0, (uint)PagesCount);
    }
}