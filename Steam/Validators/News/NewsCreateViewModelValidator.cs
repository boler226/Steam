using FluentValidation;
using Steam.Interfaces;
using Steam.Models.News;

namespace Steam.Validators.News
{
    public class NewsCreateViewModelValidator : AbstractValidator<NewsCreateViewModel>
    {
        public NewsCreateViewModelValidator(IExistingEntityCheckerService checker, IImageValidator imageValidator) 
        {
          

            RuleFor(i => i.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(255)
                .WithMessage("Title cannot be longer than 255 characters.");

            RuleFor(i => i.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(4000)
                .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(i => i.ImageOrVideo)
                .NotNull()
                .WithMessage("Image or video is required.")
                .DependentRules(() =>
                {
                    RuleFor(i => i.ImageOrVideo).MustAsync(imageValidator.IsValidImageAsync)
                    .WithMessage("Image or video valid.");
                });
        }
    }
}
