using Steam.Models.Pagination;
using System.Data;

namespace Steam.Models.News
{
    public class NewsFilterViewModel : PaginationViewModel
    {
        public DateTime dateOfRelease;
    }
}
