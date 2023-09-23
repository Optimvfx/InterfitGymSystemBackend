namespace Common.Models;

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