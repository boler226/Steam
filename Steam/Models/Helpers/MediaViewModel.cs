using Steam.Data.Entities;
using Steam.Models.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Models.Helpers
{
    public class MediaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
    }
}
