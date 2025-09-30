namespace Biblioteca.Application.Pagination;

public class ToPagedList<T> : List<T> where T : class
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
     
     public static void 
}