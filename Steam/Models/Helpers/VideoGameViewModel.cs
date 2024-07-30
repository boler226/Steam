using Steam.Data.Entities;
using Steam.Models.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Models.Helpers
{
    public class VideoGameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
