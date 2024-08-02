using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Steam.Data.Entities
{
    [Table("tblDiscount")]
    public class DiscountEntity : BaseEntity<int>
    {
        [Required]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual GameEntity Game { get; set; }

        public int DiscountPercentage { get; set; } 

        public DateTime? DiscountStartDate { get; set; }

        public DateTime? DiscountEndDate { get; set; } 
    }
}
