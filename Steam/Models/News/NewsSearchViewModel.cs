using Steam.Models.Account;
using Steam.Models.Game;

namespace Steam.Models.News
{
    public class NewsSearchViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string ImageOrVideo { get; set; }
        public int Rating { get; set; }
        public List<CommentsViewModel> Comments { get; set; }
        public int GameId { get; set; }
        public GameItemViewModel Game { get; set; }
        public int? Page {  get; set; }
        public int? PageSize { get; set;}
    }
}
