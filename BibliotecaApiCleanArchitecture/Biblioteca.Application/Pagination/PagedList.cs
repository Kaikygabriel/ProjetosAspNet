namespace Biblioteca.Application.Pagination;

public class PagedList<T> : List<T> where T : class
{
     public int PageNumber { get; set; }
     public int PageSize { get; set; }
     public int  TotalCount{ get; set; }
     public int TotalPage{ get; set; }

     public bool HasPrevius => PageNumber > 0;
     public bool HasNext => PageNumber < TotalPage ;

     public PagedList(IEnumerable<T> list, int pageNumber,int pageSize,int count)
     {
          AddRange(list);
          PageNumber = pageNumber;
          PageSize= pageSize;
          TotalCount = count;
          TotalPage = (int)Math.Abs((pageNumber - 1) * pageSize);
     }

     public static PagedList<T> ToPagedList<T>(IEnumerable<T> list, QueryStringPagination paramter) where T :class
     {
          var count = list.Count();
          list.Skip((int)(paramter.PageNumber - 1) * paramter.PageNumber).Take(paramter.PageNumber);
          return new PagedList<T>(list, paramter.PageNumber, paramter.PageSize, count);
     }
}