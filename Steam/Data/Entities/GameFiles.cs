using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Steam.Data.Entities
{
    [Table("tblGameFiles")]
    public class GameFiles : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual GameEntity Game { get; set; }
    }
}
