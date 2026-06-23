
namespace Contactos.Application.Features.DTOs
{
    public class PaginatedResult<T>
    {
        public PaginatedResult()
        {
            Items = new List<T>();
        }

        public PaginatedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex {get; set;}
        public int PageSize {get; set; }
        public int TotalCount {get; set;}
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public List<T> Items { get; set; }

        
    }
}
