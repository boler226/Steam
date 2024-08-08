namespace Steam.Interfaces
{
    public interface IMediaService
    {
        Task<List<string>> SaveMediaAsync(IEnumerable<IFormFile> files);
        public string DetermineMediaType(string fileName);
    }
}
