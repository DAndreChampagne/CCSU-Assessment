
namespace Assessment.Web.Filters {

    public class PaginationFilter {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalPages { get; set; } = 0;

        public PaginationFilter() {}
        public PaginationFilter(int pageNumber, int pageSize) {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 20 : pageSize;
        }
        public PaginationFilter(int pageNumber, int pageSize, int totalPages) : this(pageNumber, pageSize) {
            TotalPages = totalPages;
        }
    }

}