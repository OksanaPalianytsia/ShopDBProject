using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICategoryDal
    {
        List<CategoryDTO> GetAllCategories();
        CategoryDTO GetCategoryById(int categoryId);
        void UpdateCategory(CategoryDTO category);
        void CreateCategory(CategoryDTO category);
        void DeleteCategory(int categoryId);
    }
}
