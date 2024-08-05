using FluentValidation;
using Steam.Interfaces;
using Steam.Models.Game;

namespace Steam.Validators.Games
{
    public class GamesCreateValidator : AbstractValidator<GameCreateViewModel>
    {
        public GamesCreateValidator(IExistingEntityCheckerService checker, IMediaValidator mediaValidator) 
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                    .WithMessage("Name is required.")
                .MaximumLength(50)
                    .WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(i => i.Price)
                .NotNull()
                    .WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price cannot be negative.");

            RuleFor(i => i.Description)
                .NotEmpty()
                    .WithMessage("Description is required.")
                .MaximumLength(2000)
                    .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(i => i.OperatingSystem)
                .NotEmpty()
                    .WithMessage("OperatingSystem is required.")
                .MaximumLength(255)
                    .WithMessage("OperatingSystem cannot be longer than 255 characters.");

            RuleFor(i => i.Processor)
                .NotEmpty()
                    .WithMessage("OperatingSystem is required.")
                .MaximumLength(255)
                    .WithMessage("OperatingSystem cannot be longer than 255 characters.");

            RuleFor(i => i.RAM)
                .NotNull()
                    .WithMessage("RAM is required.");

            RuleFor(i => i.VideoCard)
                .NotEmpty()
                    .WithMessage("VideoCard is required.")
                .MaximumLength(255)
                    .WithMessage("VideoCard cannot be longer than 255 characters.");

            RuleFor(i => i.DiskSpace)
               .NotNull()
                   .WithMessage("DiskSpace is required.");

            RuleFor(i => i.DiscountPercentage)
                .LessThanOrEqualTo(100)
                     .WithMessage("Discount percentage cannot be more than 100.");


            RuleFor(g => g.Categories)
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

            RuleFor(i => i.Media)
          .NotNull()
              .WithMessage("Images and videos are required.")
          .Must(files => files.Any())
              .WithMessage("At least one image or video is required.")
          .DependentRules(() =>
          {
              RuleFor(i => i.Media)
                  .MustAsync(mediaValidator.IsValidMediaAsync)
                      .WithMessage("One or more selected media are invalid");
          });
        }
    }
}
