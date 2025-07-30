namespace FilmesApi.Pagination;

public class FilmePagination
{
    private const int _maxPageSize = 50;
    public  int PageNumber{ get; set; }
    private int _pageSize;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}