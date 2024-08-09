using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblGame")]
    public class GameEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required, StringLength(2000)]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime DateOfRelease { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int CommentsCount { get; set; }
        public ICollection<GameCommentEntity> GameComments { get; set; } = new List<GameCommentEntity>();
        [Required]
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public UserEntity Developer { get; set; } = null!;

        public SystemRequirementsEntity SystemRequirements { get; set; } = null!;
        public DiscountEntity Discount { get; set; } = null!;

        public ICollection<GameCategoryEntity> GameCategories { get; set; } = new List<GameCategoryEntity>();
        public  ICollection<GameMediaEntity> GameMedia { get; set; } = new List<GameMediaEntity>();
        public ICollection<NewsEntity> News { get; set; } = new List<NewsEntity>();
        public ICollection<UserGameEntity> UserGames { get; set; } = new List<UserGameEntity>();
    }
}
