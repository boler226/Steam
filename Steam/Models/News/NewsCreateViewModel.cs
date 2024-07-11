using System.ComponentModel.DataAnnotations;

namespace Steam.Models.News
{
    public class NewsCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string VideoURL { get; set; }
        public int GameId { get; set; }
    }
}
