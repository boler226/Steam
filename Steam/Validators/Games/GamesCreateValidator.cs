using FluentValidation;
using Steam.Interfaces;
using Steam.Models.Game;

namespace Steam.Validators.Games
{
    public class GamesCreateValidator : AbstractValidator<GameCreateViewModel>
    {
        public GamesCreateValidator(IExistingEntityCheckerService checker, IImageValidator imageValidator) 
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                    .WithMessage("Name is required.")
                .MaximumLength(50)
                    .WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(i => i.Price)
                .NotEmpty()
                    .WithMessage("Price is required.");

            RuleFor(i => i.Description)
                .NotEmpty()
                    .WithMessage("Description is required.")
                .MaximumLength(4000)
                    .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(i => i.DateOfRelease)
               .NotEmpty()
                    .WithMessage("Date of release is required.");

            RuleFor(i => i.SystemRequirements)
                .NotEmpty()
                    .WithMessage("System requirements is required.")
                .MaximumLength(1000)
                    .WithMessage("System requirements cannot be longer than 1000 characters.");

            RuleFor(g => g.CategoryIds)
                .MustAsync(async (ids, cancellationToken) =>
                {
                    foreach (var id in ids)
                    {
                        if (!await checker.IsCorrectCategoryId(id, cancellationToken))
                            return false;
                    }
                    return true;
                })
                .WithMessage("One or more selected category ids are invalid");

            RuleFor(i => i.Images)
                .NotNull()
                    .WithMessage("Image is required.")
                .DependentRules(() =>
                {
                    RuleFor(i => i.Images).MustAsync(imageValidator.IsValidImagesAsync)
                        .WithMessage("One or more selected images are invalid");
                });
        }
    }
}
