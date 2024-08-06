using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblMedia")]
    public class MediaEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; } = null!;
        public int Priority { get; set; }

        public ICollection<GameMediaEntity> GameMedia { get; set; } = new List<GameMediaEntity>();
        public ICollection<NewsMediaEntity> NewsMedia { get; set; } = new List<NewsMediaEntity>();

    }
}
