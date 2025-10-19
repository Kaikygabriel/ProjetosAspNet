namespace ProductsApi.Domain.BackOffice.ObjectValue;

public class QueryStringParameters
{
    private const int ValueMaxPageSize= 50;
    private int _pageNumber;
    private int _pageSize;

    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }
        set
        {
            _pageNumber = (value < 1) ? 1 : value;
        }
    }

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > ValueMaxPageSize) ? ValueMaxPageSize : value;
        }
    }

    public QueryStringParameters()
    {
        
    }

    public QueryStringParameters(int pageSize,int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}