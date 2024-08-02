using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblDeveloperGame")]
    public class DeveloperGameEntity
    {
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public UserEntity Developer { get; set; } = null!;

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public GameEntity Game { get; set; } = null!;
    }
}
