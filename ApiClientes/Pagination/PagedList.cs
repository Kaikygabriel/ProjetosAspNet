namespace ApiClientes.Pagination;

public class PagedList<T>  : List<T> where T : class 
{
    public int PageNumber{ get; set; }
    public int PageSize{ get; set; }
    public int TotalCount{ get; set; }
    public int TotalPage{ get; set; }

    public bool HasNext => PageNumber < TotalPage;
    public bool HasPrevius=> PageNumber > 1;

    public PagedList(IEnumerable<T> items, int count,int pageSize,int pageNumber)
    {
        AddRange(items);
        TotalCount = count;
        PageSize= pageSize;
        PageNumber= pageNumber;
        TotalPage = (int)Math.Ceiling((double)count/pageSize );
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> items, int pageSize, int pageNumber)
    {
        if (pageNumber < 1)
            pageNumber = 1;
        var listOrdenada = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return new PagedList<T>(listOrdenada, listOrdenada.Count(), pageSize, pageNumber);
    }
}