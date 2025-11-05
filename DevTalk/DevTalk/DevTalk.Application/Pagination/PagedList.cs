namespace DevTalk.Application.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int PageSize { get;private set; }
    public int PageNumber { get; private set; }
    public int TotalPage { get; private set; }

    public bool HasPrevius => PageNumber > TotalPage;
    public bool HasNext => PageNumber < TotalPage;
    
    public PagedList(IEnumerable<T> list, int count,int pageNumber,int pageSize)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalPage = count/((pageNumber - 1) * pageSize);
        AddRange(list);
    }
}