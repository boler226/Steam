using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblGame")]
    public class GameEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfRelease { get; set; }
        [Required]
        public int Rating { get; set; }
        public virtual ICollection<CommentsEntity> Comments { get; set; } = new List<CommentsEntity>();

        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public virtual UserEntity Developer { get; set; }

        public virtual SystemRequirementsEntity SystemRequirements { get; set; }

        public virtual ICollection<GameCategoryEntity> GameCategories { get; set; } = new List<GameCategoryEntity>();
        public virtual ICollection<GameFiles> GameFiles { get; set; } = new List<GameFiles>();
        public virtual ICollection<NewsEntity> News { get; set; } = new List<NewsEntity>();
        public virtual ICollection<UserGameEntity> UserGames { get; set; } = new List<UserGameEntity>();
    }
}
