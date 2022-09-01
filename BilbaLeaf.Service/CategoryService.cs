using AutoMapper;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using static BilbaLeaf.Entities.Enums;

namespace LogiLync.Service
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool IsExisting(string name)
        {
            Category category = _categoryRepository.FindBy(x => x.Name == name).FirstOrDefault();
            if (category == null) return false;
            return true;
        }

        public async Task<Int64> AddCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Add(category);
            await _unitOfWork.Commit();
            return category.Id;
        }

        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = await _categoryRepository.GetSingle(categoryDTO.Id);
            if (category != null)
            {
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Description;
                category.StatusId = categoryDTO.StatusId;
                category.ParentId = categoryDTO.ParentId;
                category.ImagePath = categoryDTO.ImagePath;
                category.ImageName = categoryDTO.ImageName;
                await _unitOfWork.Commit();
            }
        }

        public async Task<CategoryDTO> GetCategoryById(Int64 id)
        {
            var result = await _categoryRepository.GetSingle(id);
            if (result == null) return new CategoryDTO();
            var category = _mapper.Map<CategoryDTO>(result);
            return category;

        }
        public async Task<bool> Delete(Int64 id)
        {
            var result = await _categoryRepository.GetSingle(id);
            if (result == null) return false;
            _categoryRepository.Delete(result);
            await _unitOfWork.Commit();
            return true;
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> GetParent()
        {
            var category = _categoryRepository.GetAll();
            var parent = new List<System.Web.Mvc.SelectListItem>();
            foreach (var item in category)
            {
                item.Name = AppendParent(item.ParentId, item.Name);
                parent.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return parent;
        }

        public List<Category> GetAllCategories()=>
                _categoryRepository.GetAll().ToList();

        public List<CategoryDTO> GetAllForWeb()=>
                                _categoryRepository
                                .FindBy(cat => cat.StatusId == (int)CategoryStatus.PUBLISHED)
                                .Select(c => new CategoryDTO
                                {
                                    Id=c.Id,
                                    StatusId=c.StatusId,
                                    ImagePath= "Uploads\\CategoryImages\\" + c.ImagePath,
                                    Name = c.Name
                                }).ToList();

        public IEnumerable<System.Web.Mvc.SelectListItem> GetOnlyParent()
        {
            var category = _categoryRepository.GetAll().Where(x=>(x.ParentId==0|| x.ParentId==null));
            var parent = new List<System.Web.Mvc.SelectListItem>();
            foreach (var item in category)
            {
                item.Name = item.Name;
                parent.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return parent;
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> GetParentBasedOnProductCategory(List<Int64> categoryId)
        {
            var category = _categoryRepository.GetAll().Where(x => categoryId.Contains(x.Id));
            var parent = new List<System.Web.Mvc.SelectListItem>();
            foreach (var item in category.ToList())
            {
                item.Name = AppendParent(item.ParentId, item.Name);
                parent.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            return parent;
        }

        public string AppendParent(Int64? parentId, string categoryName)
        {
            try
            {
                if (parentId == null || parentId == 0)
                {
                    return categoryName;
                }

                var category = _categoryRepository.GetSingle((Int64)parentId).Result;
                if (category != null)
                {
                    categoryName = category.Name + "->" + categoryName;
                    AppendParent(category.ParentId, categoryName);
                }
                return categoryName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
