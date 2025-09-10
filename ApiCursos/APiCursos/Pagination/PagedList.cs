namespace APiCursos.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int PageSize { get; set; }
    public int PageNumber{ get; set; }
    public int TotalPage{ get; set; }
    public int TotalCount{ get; set; }

    public bool HasPrevius => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPage;

    public PagedList(int pageSize,int pageNumber,int count, IEnumerable<T>items)
    {
        AddRange(items);
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPage = (int)Math.Ceiling((double)count / pageSize);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> items, int pageNumber, int pageSize)
    {
        if (pageNumber <=0)
        {
            pageNumber = 1;
        }
        int count = items.Count();
        items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return new PagedList<T>(pageSize, pageNumber, count, items);
    }
}