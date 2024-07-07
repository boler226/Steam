using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class GameCreateViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string SystemRequirements { get; set; }
        public List<int> SelectedCategoryIds { get; set; }
        public IEnumerable<IFormFile> ImageUrls { get; set; }
    }
}
