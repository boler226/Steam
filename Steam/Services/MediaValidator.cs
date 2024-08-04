using Steam.Interfaces;

namespace Steam.Services
{
    public class MediaValidator(
        IImageValidator imageValidator,
        IVideoValidator videoValidator
        ) : IMediaValidator
    {
        public async Task<bool> IsValidMediaAsync(IFormFile media, CancellationToken cancellationToken)
        {
            var extension = Path.GetExtension(media.FileName).ToLowerInvariant();

            if (IsImage(extension))
            {
                return await imageValidator.IsValidImageAsync(media, cancellationToken);
            }
            else if (IsVideo(extension))
            {
                return await videoValidator.IsValidVideoAsync(media, cancellationToken);
            }

            return false;
        }
        private bool IsImage(string extension) => new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" }.Contains(extension);
        private bool IsVideo(string extension) => new[] { ".mp4", ".avi", ".mov", ".mkv" }.Contains(extension);
    }
}
