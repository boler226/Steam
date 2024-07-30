using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Steam.Constants;
using Steam.Data;
using Steam.Services.ControllerServices;
using Steam.Data.Entities.Identity;
using Steam.Helpers;
using Steam.Interfaces;
using Steam.Models.Account;
using Steam.Services.ControllerServices.Interfaces;
using Steam.Common.Exceptions;

namespace Steam.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController(
        UserManager<UserEntity> userManager,
        IJwtTokenService jwtTokenService,
        IValidator<RegisterViewModel> validator,
        IAccountsControllerService service
        ) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInViewModel model)
        {
           UserEntity user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Wrong authentication data");

            return Ok(new JwtTokenResponse
            {
                Token = await jwtTokenService.CreateTokenAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] RegisterViewModel model)
        {
            var validatorResult = await validator.ValidateAsync(model);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            try
            {
                var user = await service.SignUpAsync(model);

                return Ok(new JwtTokenResponse
                {
                    Token = await jwtTokenService.CreateTokenAsync(user)
                });
            }
            catch (IdentityException ex)
            {
                return StatusCode(500, ex.IdentityResult.Errors);
            }
        }
    }
}
