using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class GameCreateViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string SystemRequirements { get; set; }
        public List<int> Categories { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
