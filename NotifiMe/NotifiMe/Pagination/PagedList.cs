using System.Security.Principal;

namespace NotifiMe.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int TotalCount { get; set; }
    public int TotalPage { get; set; }
    public int PageNumber{ get; set; }
    public int PageSize{ get; set; }

    public bool HasPrevius => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPage;

    public PagedList(IEnumerable<T>items,int count,int pageSize,int pageNumber)
    {
        AddRange(items);
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPage = (int)Math.Ceiling((double)pageSize / pageNumber);
    }

    public static PagedList<T> ToPagedList<T>(IEnumerable<T> items, int pageSize, int pageNumber) where T : class
    {
        var count = items.Count();
        var itemsSort = items.Skip((pageSize - 1) * pageSize).Take(pageSize);
        return new PagedList<T>(itemsSort, count, pageSize, pageNumber);
    }
}