namespace Steam.Models.Game
{
    public class DiscountViewModel
    {
        public int GameId { get; set; } 
        public int DiscountPercentage { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
    }
}
