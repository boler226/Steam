using Steam.Interfaces;

namespace Steam.Services
{
    public class VideoService(
        IConfiguration configuration
        ) : IVideoService
    {
        public async Task<string> SaveVideoAsync(IFormFile video)
        {
            if (video == null || video.Length == 0)
                throw new ArgumentException("Video file is empty.");

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(video.FileName)}";
            string filePath = Path.Combine(VideosDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            return fileName;
        }
        public string VideosDir => Path.Combine(
        Directory.GetCurrentDirectory(),
        configuration["VideosDir"] ?? throw new NullReferenceException("Videos")
    );
    }
}
