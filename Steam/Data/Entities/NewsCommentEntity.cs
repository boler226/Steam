using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblNewsComments")]
    public class NewsCommentEntity
    {
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public NewsEntity News { get; set; } = null!;

        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public CommentEntity Comment { get; set; } = null!;
    }
}
