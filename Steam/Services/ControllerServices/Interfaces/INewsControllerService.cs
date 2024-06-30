using Steam.Models.News;

namespace Steam.Services.ControllerServices.Interfaces
{
    public interface INewsControllerService
    {
        Task CreateAsync(NewsCreateViewModel model);
        Task UpdateAsync(NewsEditViewModel model);
        Task DeleteIfExistsAsync(int id);
    }
}
