using Steam.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class GameCreateViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string OperatingSystem { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public string VideoCard { get; set; }
        public int DiskSpace { get; set; }
        public int? DiscountPercentage { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public int DeveloperId { get; set; } // only for swager
        public List<int> Categories { get; set; }
        public List<IFormFile> Media { get; set; }
    }
}
