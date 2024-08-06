using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblSystemRequirements")]
    public class SystemRequirementsEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string OperatingSystem { get; set; } = null!;
        [Required, StringLength(255)]
        public string Processor {  get; set; } = null!;
        [Required]
        public int RAM { get; set; }
        [Required, StringLength(255)]
        public string VideoCard { get; set; } = null!;
        [Required]
        public int DiskSpace { get; set; }
      
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; } = null!;
    }
}
