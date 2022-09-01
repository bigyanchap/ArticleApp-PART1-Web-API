using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BilbaLeaf.Service.Infrastructure
{
    public interface ICategoryService
    {
        bool IsExisting(string name);
        Task<Int64> AddCategory(CategoryDTO categoryDTO);

        IEnumerable<SelectListItem> GetParent();
        List<Category> GetAllCategories();
        List<CategoryDTO> GetAllForWeb();
        Task<CategoryDTO> GetCategoryById(Int64 id);
        Task<bool> Delete(Int64 id);

        Task UpdateCategory(CategoryDTO categoryDTO);

        IEnumerable<System.Web.Mvc.SelectListItem> GetParentBasedOnProductCategory(List<Int64> categoryId);

        IEnumerable<System.Web.Mvc.SelectListItem> GetOnlyParent();
    }
}
