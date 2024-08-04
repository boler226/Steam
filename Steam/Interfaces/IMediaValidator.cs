namespace Steam.Interfaces
{
    public interface IMediaValidator
    {
        Task<bool> IsValidMediaAsync(IFormFile media, CancellationToken cancellationToken);
    }
}
