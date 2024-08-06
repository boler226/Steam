namespace Steam.Interfaces
{
    public interface IMediaService
    {
        Task<List<string>> SaveMediaAsync(IEnumerable<IFormFile> files);
    }
}
