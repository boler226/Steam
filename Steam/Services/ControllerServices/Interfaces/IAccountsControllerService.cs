using Steam.Data.Entities.Identity;
using Steam.Models.Account;

namespace Steam.Services.ControllerServices.Interfaces
{
    public interface IAccountsControllerService
    {
        Task<UserEntity> SignUpAsync(RegisterViewModel model);
    }
}
