using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Steam.Data.Entities
{
    public class CommentsEntity : BaseEntity<int>
    {
        [Required, StringLength(500)]
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        public int? NewsId { get; set; }
        [ForeignKey("NewsId")]
        public virtual NewsEntity? News { get; set; }
        public int? GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual GameEntity? Game { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
