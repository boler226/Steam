using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Steam.Data;
using Steam.Data.Entities.Identity;
using Steam.Common.Exceptions;
using Steam.Interfaces;
using Steam.Models.Account;
using Steam.Services.ControllerServices.Interfaces;
using Steam.Constants;

namespace Steam.Services.ControllerServices
{
    public class AccountsControllerService(
        AppEFContext context,
        UserManager<UserEntity> userManager,
        IMapper mapper,
        IImageService imageSercice
        ) : IAccountsControllerService 
    {
        public async Task<UserEntity> SignUpAsync(RegisterViewModel model)
        {
            var user = mapper.Map<RegisterViewModel, UserEntity>(model);
            user.Photo = await imageSercice.SaveImageAsync(model.Image);

            try
            {
                await CreateUserAsync(user, model.Password);
            }
            catch
            {
                imageSercice.DeleteImageIfExists(user.Photo);
                throw;
            }

            return user;
        }

        private async Task CreateUserAsync(UserEntity user, string? password = null)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                IdentityResult identityResult = await CreateUserInDatabaseAsync(user, password);
                if (!identityResult.Succeeded)
                    throw new IdentityException(identityResult, "User creating error");

                identityResult = await userManager.AddToRoleAsync(user, Roles.User);
                if (!identityResult.Succeeded)
                    throw new IdentityException(identityResult, "Role assignment error");

                await transaction.CommitAsync();
            }
            catch 
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<IdentityResult> CreateUserInDatabaseAsync(UserEntity user, string? password)
        {
            if (password is null)
                return await userManager.CreateAsync(user);

            return await userManager.CreateAsync(user, password);
        }
    }
}
