using FluentValidation;
using Steam.Interfaces;
using Steam.Models.Game;

namespace Steam.Validators.Games
{
    public class GameEditValidator : AbstractValidator<GameEditViewModel>
    {
        public GameEditValidator(IExistingEntityCheckerService checker, IImageValidator imageValidator) 
        {
            RuleFor(g => g.Id)
                .MustAsync(checker.IsCorrectGameId)
                    .WithMessage("Game with this id is not exists.");

            RuleFor(g => g.Name)
                .MinimumLength(3)
                    .WithMessage("Name min length is 3 symbol.")
                .MaximumLength(50)
                    .WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(i => i.Price)
                .NotNull()
                    .WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price cannot be negative.");

            RuleFor(g => g.Description)
                 .MaximumLength(4000)
                    .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(g => g.SystemRequirements)
                .MaximumLength(1000)
                    .WithMessage("System requirements cannot be longer than 1000 characters.");

            RuleFor(g => g.CategoryIds)
                .ForEach(id => id.MustAsync(checker.IsCorrectGameId))
                    .WithMessage("One or more selected category ids are invalid");

            RuleFor(g => g.Images).MustAsync(imageValidator.IsValidImagesAsync)
                       .WithMessage("One or more selected images are invalid");
        }
    }
}
