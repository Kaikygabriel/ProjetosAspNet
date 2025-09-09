namespace AcademyPro.Pagination;

public class QueryStringParamters
{
    private const int minPageNumber = 1;
    private const int maxPageSize = 50;
    private int pageSize;
    private int pageNumber;
    
    public int PageSize
    {
        get
        {
            return pageSize;
        }
        set
        {
            pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
    public int PageNumber
    {
        get
        {
            return pageNumber;
        }
        set
        {
            pageNumber = (value > minPageNumber) ? minPageNumber : value;
        }
    }
}