namespace APiCursos.Pagination;

public abstract class QueryStringPagination
{
    private const int MaxValuePage = 50;
    public int PageNumber { get; set; }
    private int _pageSize;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (MaxValuePage < value) ? MaxValuePage : value;
        }
    }
}