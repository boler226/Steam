using Steam.Interfaces;

namespace Steam.Services
{
    public class MediaService(
        IImageService imageService,
        IVideoService videoService
        ) : IMediaService
    {
        public async Task<List<string>> SaveMediaAsync(IEnumerable<IFormFile> files)
        {
            var results = new List<string>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                // Перевірте розширення файлу, щоб визначити тип медіа
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (IsImage(extension))
                {
                    var imageName = await imageService.SaveImageAsync(file);
                    results.Add(imageName);
                }
                else if (IsVideo(extension))
                {
                    var videoName = await videoService.SaveVideoAsync(file);
                    results.Add(videoName);
                }
                else
                {
                    throw new NotSupportedException($"File type {extension} is not supported.");
                }
            }

            return results;
        }

        private bool IsImage(string extension)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            return imageExtensions.Contains(extension);
        }

        private bool IsVideo(string extension)
        {
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".mkv", ".webm" };
            return videoExtensions.Contains(extension);
        }
    }
}

