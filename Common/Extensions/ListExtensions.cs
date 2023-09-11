namespace Common.Extensions;

public static class ListExtensions
{
    public static ICollection<T> AddRange<T>(this ICollection<T> collection, int count, Func<int, T> getElement)
    {
        if (count < 0)
            throw new ArgumentException();

        return AddRange(collection, (uint)count, getElement);
    }
    public static ICollection<T> AddRange<T>(this ICollection<T> collection, uint count, Func<int,T> getElement)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = getElement(i);
            collection.Add(obj);
        }

        return collection;
    }

    public static T Random<T>(this IReadOnlyList<T> collection, Random? rand = null)
    {
        if (collection.Count <= 0)
            throw new NullReferenceException();
        
        rand = rand ?? new Random();

        var randomIndex = rand.Next(collection.Count);
        return collection[randomIndex];
    }
    
    public static ICollection<T> Create<T>(int count, Func<int, T> getElement)
    {
        if (count < 0)
            throw new AggregateException();

        return Create((uint)count, getElement);
    }
    public static ICollection<T> Create<T>(uint count, Func<int, T> getElement)
    {
        var list = new List<T>();
        
        for (int i = 0; i < count; i++)
        {
            var obj = getElement(i);
            list.Add(obj);
        }

        return list;
    }
}