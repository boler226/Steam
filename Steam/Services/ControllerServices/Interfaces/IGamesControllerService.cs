using Steam.Models.Game;

namespace Steam.Services.ControllerServices.Interfaces
{
    public interface IGamesControllerService
    {
        Task CreateAsync(GameCreateViewModel model);
    }
}
