using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblNews")]
    public class NewsEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Title { get; set; } = null!;
        [Required, StringLength(4000)]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime DateOfRelease { get; set; }
        public ICollection<NewsMediaEntity> NewsMedia { get; set; } = new List<NewsMediaEntity>();
        [Required]
        public int Rating { get; set; }
        [Required]
        public int CommentsCount { get; set; }
        public virtual ICollection<NewsCommentEntity> NewsComments { get; set; } = new List<NewsCommentEntity>();
        public int? GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; } = null!;

        public int UserOrDeveloperId { get; set; }
        [ForeignKey("UserOrDeveloperId")]
        public UserEntity UserOrDeveloper { get; set; } = null!;

    }
}
