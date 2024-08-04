namespace Steam.Interfaces
{
    public interface IVideoService
    {
        Task<string> SaveVideoAsync(IFormFile video);
        string VideosDir { get; }
    }
}
