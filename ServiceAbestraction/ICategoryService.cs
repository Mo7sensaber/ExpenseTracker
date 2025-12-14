using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbestraction
{
    public interface ICategoryService
    {
        Task<ReturnCategoryDto> AddCategory(ReturnCategoryDto category);
        Task<IEnumerable<ReturnCategoryDto>> GetAllCategories();
        Task<ReturnCategoryDto?> GetCategoryById(int id);
        Task DeleteCategory(ReturnCategoryDto category);
        Task UpdateCategory(ReturnCategoryDto category);


    }
}
