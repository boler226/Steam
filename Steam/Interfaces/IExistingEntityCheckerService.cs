namespace Steam.Interfaces
{
    public interface IExistingEntityCheckerService
    {
        Task<bool> IsCorrectCategoryId(int id, CancellationToken cancellationToken);
        Task<bool> IsCorrectGameId(int id, CancellationToken cancellationToken);
    }
}
