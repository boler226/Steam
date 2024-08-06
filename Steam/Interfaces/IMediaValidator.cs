namespace Steam.Interfaces
{
    public interface IMediaValidator
    {
        Task<bool> IsValidMediaAsync(IEnumerable<IFormFile> mediaFiles, CancellationToken cancellationToken);
    }
}