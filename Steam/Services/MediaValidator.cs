using Steam.Interfaces;

namespace Steam.Services
{
    public class MediaValidator(
        IImageValidator imageValidator,
        IVideoValidator videoValidator
        ) : IMediaValidator
    {
        public async Task<bool> IsValidMediaAsync(IEnumerable<IFormFile> mediaFiles, CancellationToken cancellationToken)
        {
            if (mediaFiles == null)
                return true;

            // Split media files into images and videos
            var imageFiles = mediaFiles.Where(file => IsImage(Path.GetExtension(file.FileName).ToLowerInvariant()));
            var videoFiles = mediaFiles.Where(file => IsVideo(Path.GetExtension(file.FileName).ToLowerInvariant()));

            // Validate images and videos separately
            var imageValidationTask = imageValidator.IsValidImagesAsync(imageFiles, cancellationToken);
            var videoValidationTask = videoValidator.IsValidVideosAsync(videoFiles, cancellationToken);

            var results = await Task.WhenAll(imageValidationTask, videoValidationTask);

            // Check if all validations passed
            return results.All(r => r);
        }
        private bool IsImage(string extension) => new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" }.Contains(extension);
        private bool IsVideo(string extension) => new[] { ".mp4", ".avi", ".mov", ".mkv" }.Contains(extension);
    }
}
