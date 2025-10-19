namespace ProductsApi.Application.Pagination;

public class PagedList<T> : List<T> where T: class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }

    public bool HasPrevius => PageNumber > 1;
    public bool HasNext => PageNumber< TotalPage;

    public PagedList(int pageNumber,int pageSize,IEnumerable<T>list)
    {
        AddRange(list);
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPage = pageSize / list.Count();
    } 
    
}