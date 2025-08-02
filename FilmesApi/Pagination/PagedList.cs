namespace FilmesApi.Pagination;

public class PagedList<T> : List<T> 
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPage{ get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevius => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPage;

    public PagedList(IEnumerable<T>items,int count,int pageSize,int pageNumber)
    {
        PageNumber = pageNumber; 
        PageSize = pageSize;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> items, int pageSize, int pageNumber)
    {
        int count = items.Count();
        var list = items.Skip((pageNumber - 1) * pageSize).Take(pageNumber);
        return new PagedList<T>(items, count, pageSize, pageNumber);
    }
}