using OMS.Common.Dtos.Categories;

namespace OMS.Bll.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
