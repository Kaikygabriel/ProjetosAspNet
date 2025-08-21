namespace ApiCompras.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext => PageNumber < TotalPage;
    public bool HasPrevius=> PageNumber > 0;
    public PagedList(int pageSize, int pageNumber, int count, IEnumerable<T> data)
    {
        AddRange(data);
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = count;
        TotalPage = (int)Math.Ceiling((double)count / pageSize);
    }
    public static PagedList<T> CreatedPagedList(IEnumerable<T> values, int pageSize, int PageNumber)
    {
        var count = values.Count();
        var data = values.Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(pageSize, PageNumber, count, data);
    }
}