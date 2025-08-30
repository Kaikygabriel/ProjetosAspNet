namespace NotifiMe.Pagination;

public abstract class QueryStringParameters
{
        const int MaxPageSize = 50;
        private int _pageNumber { get; set; } = 1;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber=(value <=0)? 1 : value;
            }
        }

        private int _pageSize = MaxPageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
}