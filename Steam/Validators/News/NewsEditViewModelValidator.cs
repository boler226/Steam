using FluentValidation;
using Steam.Interfaces;
using Steam.Models.News;

namespace Steam.Validators.News
{
    public class NewsEditViewModelValidator : AbstractValidator<NewsEditViewModel>
    {
        public NewsEditViewModelValidator(IExistingEntityCheckerService checker, IImageValidator imageValidator) 
        {
            RuleFor(c => c.GameId)
                .MustAsync(checker.IsCorrectGameId)
                    .WithMessage("Game with this id is not exists");

            RuleFor(c => c.Title)
                .MinimumLength(10)
                    .WithMessage("Title min length is 10")
                .MaximumLength(255)
                    .WithMessage("Title is too long");

            RuleFor(c => c.Description)
                .MinimumLength(10)
                    .WithMessage("Description min length is 10")
                .MaximumLength(4000)
                    .WithMessage("Description is too long");

            RuleFor(i => i.Image)
                .NotNull()
                .WithMessage("Image is required.")
                .DependentRules(() =>
                {
                    RuleFor(i => i.Image).MustAsync(imageValidator.IsValidImageAsync)
                    .WithMessage("Image is not valid.");
                });

        }
    }
}
