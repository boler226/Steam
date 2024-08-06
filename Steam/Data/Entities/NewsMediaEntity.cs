using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblNewsMedia")]
    public class NewsMediaEntity
    {
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public MediaEntity Media { get; set; } = null!;
        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public NewsEntity News { get; set; } = null!;
    }
}
