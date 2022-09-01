using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Repository;
using BilbaLeaf.Service.Infrastructure;
using LogicLync.Api.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace BilbaLeaf.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
       
        private readonly ICategoryService _categoryService;
        IWebHostEnvironment host;
        readonly PhotoSettings photoSettings;

        public CategoryController
        (
            IWebHostEnvironment environment,
            ICategoryService categoryService,
            IOptionsSnapshot<PhotoSettings> options

        )
        {
            host = environment;
            _categoryService = categoryService;
            photoSettings = options.Value;

        }

        [HttpGet]
        [Route("getAllForWeb")]
        public async Task<IActionResult> GetAllForWeb()
        {
            try
            {
                var categories = _categoryService.GetAllForWeb();
                return Ok(categories);
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong while trying to get Categories.");
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> SaveCategory([FromForm] CategoryDTO category)
        {
            try
            {
                if (_categoryService.IsExisting(category.Name))
                {
                    return BadRequest("Category Already Created.");
                }
                if(category.Password != "bigyan")
                {
                    return BadRequest("Not Completed: declaration typo.");
                }
                var file = Request.Form.Files[0];
                
                if (file == null) return BadRequest("No file found.");
                if (file.Length > photoSettings.MaxBytes) return BadRequest(" Image shouldn't be >10MB.");

                if (!photoSettings.AcceptedFileTypes.Any(x => x.ToLower() == Path.GetExtension(file.FileName.ToLower())))
                {
                    return BadRequest("Invalid file type.");
                }
                string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\CategoryImages");
                
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                var imagepath = string.Empty;
                var imagename = string.Empty;
                if (file != null)
                {
                    imagepath = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    imagename = category.Name;
                    var filepath = Path.Combine(uploadFolderPath, imagepath);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    category.ImagePath = imagepath;
                    category.ImageName = imagename;
                }

                var id = await _categoryService.AddCategory(category);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while posting Category.");
            }
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm] CategoryDTO category)
        {
            try
            {
                
                if (category.Password != "bigyan")
                {
                    return BadRequest("Not Completed: declaration typo.");
                }
                CategoryDTO _category = await _categoryService.GetCategoryById(category.Id);
                if (Request.Form.Files.Count > 0)
                {

                    var file = Request.Form.Files[0];

                    if (file == null) return BadRequest("No file found.");
                    if (file.Length > photoSettings.MaxBytes) return BadRequest(" Image shouldn't be >10MB.");

                    if (!photoSettings.AcceptedFileTypes.Any(x => x.ToLower() == Path.GetExtension(file.FileName.ToLower())))
                    {
                        return BadRequest("Invalid file type.");
                    }
                    string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\CategoryImages");
                
                    var imagePath = _category.ImagePath;
                    if (!String.IsNullOrEmpty(imagePath))
                    {
                        var fileInfo = (uploadFolderPath + "\\" + imagePath);
                        System.IO.File.Delete(fileInfo);
                        category.ImageName = "";
                        category.ImagePath = "";
                    }
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }
                    var imagepath = string.Empty;
                    var imagename = string.Empty;
                    if (file != null)
                    {
                        imagepath = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        imagename = category.Name;
                        var filepath = Path.Combine(uploadFolderPath, imagepath);
                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        category.ImagePath = imagepath;
                        category.ImageName = imagename;
                    }
                }
                else
                {
                    category.ImageName = _category.ImageName;
                    category.ImagePath = _category.ImagePath;
                }

                await _categoryService.UpdateCategory(category);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while Updating Category");
            }
        }

        [Route("getParents")]
        [HttpGet]
        public IEnumerable<System.Web.Mvc.SelectListItem> Parent()
        {
            var result = _categoryService.GetParent();
            return result;
        }

        [Route("getCategories")]
        [HttpGet]
        public IActionResult Categories()
        {
            List<Category> categories = _categoryService.GetAllCategories();
            foreach(Category cat in categories)
            {
                cat.ImagePath= "Uploads" + "\\" + "CategoryImages" + "\\" + cat.ImagePath;
            }
            return Ok(categories);
        }

        [Route("getOnlyParents")]
        [HttpGet]
        public IActionResult ParentCategory()
        {
            var result = _categoryService.GetOnlyParent();
            return Ok(result);
        }

        [HttpGet, Route("getCategoryById/{Id}")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            category.ImagePath = "Uploads" + "\\" + "CategoryImages" + "\\" + category.ImagePath;
            return Ok(category);
        }

        [HttpGet, Route("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\CategoryImages");

            CategoryDTO _category = await _categoryService.GetCategoryById(Id);
            var imagePath = _category.ImagePath;
            if (!String.IsNullOrEmpty(imagePath))
            {
                var fileInfo = (uploadFolderPath + "\\" + imagePath);
                System.IO.File.Delete(fileInfo);
            }
            bool isDeleted = await _categoryService.Delete(Id);
            return Ok(new {Success = isDeleted });
        }
    }
}