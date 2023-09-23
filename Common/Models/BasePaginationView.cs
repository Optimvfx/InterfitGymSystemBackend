namespace Common.Models;

public abstract class BasePaginationView<T>
{
    public bool PageOutOfRange(uint page) => 
        GetPagesRange().OutOfRange(page);

    public IEnumerable<T> Get(uint page)
    {
        if (PageOutOfRange(page))
            throw new IndexOutOfRangeException();

        return GetByPage(page);
    }
    
    protected abstract IEnumerable<T> GetByPage(uint page);
    public abstract ValueRange<uint> GetPagesRange();
}