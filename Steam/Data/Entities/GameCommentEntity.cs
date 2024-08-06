using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblGameComments")]
    public class GameCommentEntity
    {
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public GameEntity Game { get; set; } = null!;

        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public CommentEntity Comment { get; set; } = null!;
    }
}
