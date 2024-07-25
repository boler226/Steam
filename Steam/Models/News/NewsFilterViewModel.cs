using Steam.Models.Pagination;
using System.Data;

namespace Steam.Models.News
{
    public class NewsFilterViewModel : PaginationViewModel
    {
        public bool? ByRelease { get; set; }
    }
}
