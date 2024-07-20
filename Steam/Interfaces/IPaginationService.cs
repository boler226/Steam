using Steam.Models.Pagination;

namespace Steam.Interfaces
{
    public interface IPaginationService<EntityViewModelType, PaginationViewModelType> where PaginationViewModelType : PaginationViewModel
    {
        Task<PageViewModel<EntityViewModelType>> GetPageAsync(PaginationViewModelType model);
    }
}
