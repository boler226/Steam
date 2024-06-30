using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Interfaces;

namespace Steam.Services
{
    public class ExistingEntityCheckerService(
        AppEFContext context
        ) : IExistingEntityCheckerService
    {
        public async Task<bool> IsCorrectCategoryId(int id, CancellationToken cancellationToken) =>
            await context.Categories.AnyAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> IsCorrectGameId(int id, CancellationToken cancellationToken) =>
            await context.Games.AnyAsync(g => g.Id == id, cancellationToken);
    }
}
