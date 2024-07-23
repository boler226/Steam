using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Steam.Data.Entities.Identity;
using Steam.Interfaces;
using Steam.Models.Account;

namespace Steam.Validators.Account
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly UserManager<UserEntity> _userManager;

        public RegisterValidator(UserManager<UserEntity> userManager, IImageValidator imageValidator)
        {
            _userManager = userManager;

            RuleFor(r => r.Email)
                .NotEmpty()
                    .WithMessage("Email is empty or null")
                .MaximumLength(100)
                    .WithMessage("Email is too long")
                .EmailAddress()
                    .WithMessage("Email is invalid")
                .MustAsync(IsNewEmail)
                    .WithMessage("There is already a user with this email");

            RuleFor(r => r.NickName)
                .NotEmpty()
                    .WithMessage("Username is empty or null")
                .MaximumLength(100)
                    .WithMessage("Username is too long")
                .MustAsync(IsNewUserName)
                    .WithMessage("There is already a user with this username");
            
            RuleFor(r => r.Image)
                .NotNull()
                    .WithMessage("Image is not selected")
                .MustAsync(imageValidator.IsValidImageAsync)
                    .WithMessage("Image is not valid");
        }

        private async Task<bool> IsNewEmail(string email, CancellationToken token)
        {
            return await _userManager.FindByEmailAsync(email) is null;
        }

        private async Task<bool> IsNewUserName(string userName, CancellationToken _)
        {
            return await _userManager.FindByNameAsync(userName) is null;
        }
    }
}
