using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblNews")]
    public class NewsEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Title { get; set; }
        [Required, StringLength(4000)]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfRelease { get; set; }
        [Required, StringLength(500)]
        public string Image { get; set; }
        [Required, StringLength(500)]
        public GameVideoEntity Video { get; set; }
        [Required]
        public int Rating { get; set; }
       
        public virtual ICollection<CommentsEntity> Comments { get; set; } = new List<CommentsEntity>();

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity UserOrDeveloper { get; set; }

    }
}
