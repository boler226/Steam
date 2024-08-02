using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Steam.Data.Entities
{
    [Table("tblGameMedia")]
    public class GameMediaEntity : BaseEntity<int>
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public MediaEntity Media { get; set; } = null!;
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public GameEntity Game { get; set; } = null!;
    }
}
