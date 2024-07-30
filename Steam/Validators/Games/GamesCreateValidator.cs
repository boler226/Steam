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
                .NotNull()
                    .WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Price cannot be negative.");

            RuleFor(i => i.Description)
                .NotEmpty()
                    .WithMessage("Description is required.")
                .MaximumLength(4000)
                    .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(i => i.SystemRequirements.OperatingSystem)
                .NotEmpty()
                    .WithMessage("OperatingSystem is required.")
                .MaximumLength(255)
                    .WithMessage("OperatingSystem cannot be longer than 255 characters.");

            RuleFor(i => i.SystemRequirements.Processor)
                .NotEmpty()
                    .WithMessage("OperatingSystem is required.")
                .MaximumLength(255)
                    .WithMessage("OperatingSystem cannot be longer than 255 characters.");

            RuleFor(i => i.SystemRequirements.RAM)
                .NotNull()
                    .WithMessage("RAM is required.");

            RuleFor(i => i.SystemRequirements.VideoCard)
                .NotEmpty()
                    .WithMessage("VideoCard is required.")
                .MaximumLength(255)
                    .WithMessage("VideoCard cannot be longer than 255 characters.");

            RuleFor(i => i.SystemRequirements.DiskSpace)
               .NotNull()
                   .WithMessage("DiskSpace is required.");


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

            RuleFor(i => i.ImagesAndVideos)
          .NotNull()
              .WithMessage("Images and videos are required.")
          .Must(files => files.Any())
              .WithMessage("At least one image or video is required.")
          .DependentRules(() =>
          {
              RuleFor(i => i.ImagesAndVideos)
                  .MustAsync(imageValidator.IsValidImagesAsync)
                      .WithMessage("One or more selected images are invalid");

              //RuleFor(i => i.ImagesAndVideos)
              //    .MustAsync(videoValidator.IsValidVideosAsync)
              //        .WithMessage("One or more selected videos are invalid");
          });
        }
    }
}
