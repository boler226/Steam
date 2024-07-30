using Steam.Models.Account;
using Steam.Models.Category;
using Steam.Models.Helpers;
using Steam.Models.News;

namespace Steam.Models.Game
{
    public class GameItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime DateOfRelease { get; set; }
        public int Rating { get; set; }

        public SystemRequirementsViewModel SystemRequirements { get; set; }
        public UserViewModel Developer { get; set; }
        public IEnumerable<CommentsViewModel> Comments { get; set; }
        public IEnumerable<VideoGameViewModel> GameVideos { get; set; }
        // public List<UserViewModel> UserGames { get; set; }
        public IEnumerable<CategoryItemViewModel> Categories { get; set; }
        public IEnumerable<GameImageViewModel> Images { get; set; }
        public IEnumerable<NewsItemViewModel> News { get; set; }
    }
}
