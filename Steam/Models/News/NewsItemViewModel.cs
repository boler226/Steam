using Steam.Data.Entities.Identity;
using Steam.Models.Account;
using Steam.Models.Game;
using Steam.Models.Helpers;
using Steam.Models.Pagination;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Models.News
{
    public class NewsItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfRelease { get; set; }
        public List<MediaViewModel> Media { get; set; }
        public int Rating { get; set; }
        public GameItemViewModel Game { get; set; }
        public List<CommentsViewModel> Comments { get; set; }
        public UserViewModel UserOrDeveloper { get; set; }
    }
}
