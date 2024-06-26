﻿using FluentValidation;
using Steam.Interfaces;
using Steam.Models.News;

namespace Steam.Validators.News
{
    public class NewsCreateViewModelValidator : AbstractValidator<NewsCreateViewModel>
    {
        public NewsCreateViewModelValidator(IExistingEntityCheckerService checker, IImageValidator imageValidator) 
        {
            RuleFor(g => g.GameId)
                .MustAsync(checker.IsCorrectGameId)
                .WithMessage("Game with this id is not exists");

            RuleFor(i => i.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(255)
                .WithMessage("Title cannot be longer than 255 characters.");

            RuleFor(i => i.Description)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(4000)
                .WithMessage("Description cannot be longer than 4000 characters.");

            RuleFor(i => i.DateOfRelease)
                .NotEmpty();

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
