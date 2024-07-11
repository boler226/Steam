using Steam.Models.Game;

namespace Steam.Services.ControllerServices.Interfaces
{
    public interface IGamesControllerService
    {
        Task CreateAsync(GameCreateViewModel model);
        Task UpdateAsync(GameEditViewModel model);
        Task DeleteIfExistsAsync(int id);
    }
}
