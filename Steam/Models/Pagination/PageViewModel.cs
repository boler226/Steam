namespace Steam.Models.Pagination
{
    public class PageViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageAvailable { get; set; }
        public int ItemsAvailable { get; set; }
    }
}
