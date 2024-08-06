using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Steam.Data.Entities
{
    [Table("tblComment")]
    public class CommentEntity : BaseEntity<int>
    {
        [Required, StringLength(500)]
        public string Comment { get; set; } = null!;
        [Required]
        public int Rating { get; set; }
        public ICollection<NewsCommentEntity> NewsComments { get; set; } = new List<NewsCommentEntity>();
        public ICollection<GameCommentEntity> GameComments { get; set; } = new List<GameCommentEntity>();
        public virtual UserEntity User { get; set; } = null!;
    }
}
