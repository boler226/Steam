using Steam.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class GameCreateViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfRelease { get; set; }
        public int? Rating { get; set; }
        public SystemRequirementsViewModel SystemRequirements { get; set; }
        public int DeveloperId { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<IFormFile> ImagesAndVideos { get; set; }
    }
}
