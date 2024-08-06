using Steam.Interfaces;

namespace Steam.Services
{
    public class VideoValidator : IVideoValidator
    {
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string>
        {
            ".mp4", ".avi", ".mov", ".mkv"
        };

        public async Task<bool> IsValidVideoAsync(IFormFile video, CancellationToken cancellationToken)
        {
            if (video == null)
                return false;

            var extension = Path.GetExtension(video.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
                return false;

            return true;
        }

        public async Task<bool> IsValidNullPossibleVideoAsync(IFormFile video, CancellationToken cancellationToken)
        {
            if (video == null)
                return true;

            return await IsValidVideoAsync(video, cancellationToken);
        }

        public async Task<bool> IsValidVideosAsync(IEnumerable<IFormFile> videos, CancellationToken cancellationToken)
        {
            var tasks = videos.Select(v => IsValidVideoAsync(v, cancellationToken));
            var results = await Task.WhenAll(tasks);

            return results.All(r => r);
        }

        public async Task<bool> IsValidNullPossibleVideosAsync(IEnumerable<IFormFile> videos, CancellationToken cancellationToken)
        {
            if (videos == null)
                return true;

            return await IsValidVideosAsync(videos.Where(v => v != null), cancellationToken);
        }
    }
}
