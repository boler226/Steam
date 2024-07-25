using Microsoft.AspNetCore.Identity;

namespace Steam.Common.Exceptions
{
    public class IdentityException(
        IdentityResult identityResult,
        string message = "Identity exception"
        ) : Exception(message)
    {
        public IdentityResult IdentityResult { get; init; } = identityResult
            ?? throw new ArgumentNullException(nameof(identityResult));
    }
}
