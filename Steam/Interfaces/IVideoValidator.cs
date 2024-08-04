namespace Steam.Interfaces
{
    public interface IVideoValidator
    {
        Task<bool> IsValidVideoAsync(IFormFile video, CancellationToken cancellationToken);
        Task<bool> IsValidNullPossibleVideoAsync(IFormFile video, CancellationToken cancellationToken);
        Task<bool> IsValidVideosAsync(IEnumerable<IFormFile> videos, CancellationToken cancellationToken);
        Task<bool> IsValidNullPossibleVideosAsync(IEnumerable<IFormFile> videos, CancellationToken cancellationToken);
    }
}
