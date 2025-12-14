using AutoMapper;
using Domain.Model;
using Domain.RepoInterface;
using ServiceAbestraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork units;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork units,IMapper mapper)
        {
            this.units = units;
            this.mapper = mapper;
        }
        public async Task<ReturnCategoryDto> AddCategory(ReturnCategoryDto category)
        {
            var CategoryMap= mapper.Map<Category>(category);
            await units.GetRepository<Category, int>().AddAsync(CategoryMap);
            await units.SaveChangesAsync();
            return mapper.Map<ReturnCategoryDto>(CategoryMap);
        }

        public async Task DeleteCategory(ReturnCategoryDto category)
        {
            await units.GetRepository<Category, int>().Delete(category.Id);
            await units.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReturnCategoryDto>> GetAllCategories()
        {
            var categories= await units.GetRepository<Category, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ReturnCategoryDto>>(categories);

        }

        public async Task<ReturnCategoryDto?> GetCategoryById(int id)
        {
            var category = await units.GetRepository<Category, int>().GetByIdAsync(id);
            return mapper.Map<ReturnCategoryDto?>(category);

        }

        public async Task UpdateCategory(ReturnCategoryDto category)
        {
            var CategoryMap = await units.GetRepository<Category,int>().GetByIdAsync(category.Id);
            if (CategoryMap == null) {
                throw new Exception("Category not found");
            }
            await units.GetRepository<Category, int>().Update(CategoryMap);
            await units.SaveChangesAsync();
        }
    }
}
