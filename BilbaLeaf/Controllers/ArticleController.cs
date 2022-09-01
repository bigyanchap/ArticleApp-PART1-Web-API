using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Service.Infrastructure;
using LogicLync.Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static BilbaLeaf.Entities.Enums;

namespace BilbaLeaf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleService _articleService;
        readonly PhotoSettings photoSettings;
        IWebHostEnvironment host;
        public ArticleController
        (
            IArticleService articleService,
            IWebHostEnvironment environment,
            IOptionsSnapshot<PhotoSettings> options
        )
        {
            this._articleService=articleService;
            host = environment;
            photoSettings = options.Value;
        }

        #region basic

        [HttpPost]
        [Route("allPaged")]
        public IActionResult GetAllArticles(ArticleQueryObject query)
        {
            try
            {
                var result=_articleService.GetAllPaged(query);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Something went wrong while trying to get all Articles.");
            }
        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetArticleById(Int64 id)
        {
            try
            {
                var article = _articleService.GetArticleById(id);
                if (!String.IsNullOrEmpty(article.ImagePath))
                {
                    article.ImagePath = "Uploads\\ArticleCoverImages" + "\\" + article.ImagePath;
                }
                return Ok(article);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        [Route("upsert")]
        public async Task<IActionResult> UpsertArticle(ArticleDTO article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Int64 result=0;
                    if (article.Id == 0)
                    {
                        result = await _articleService.Create(article);
                    }
                    else
                    {
                        result = await _articleService.Update(article);
                    }
                    return Ok(result);
                }
                else
                {
                    return BadRequest("ModelState is not valid.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e); ;
            }
        }

        [HttpDelete]
        [Route("del")]
        public async Task<IActionResult> DeleteArticle(ArticleDTO model)
        {
            try
            {
                if (model != null)
                {
                    await _articleService.Delete(model);
                    return Ok();
                }
                else
                {
                    return BadRequest("Not Found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet,Route("getArticleStatuses")]
        public IActionResult GetArticleStatuses()
        {
            try
            {
                Enums enums = new Enums();
                var result = enums.ExportEnum<ArticleStatus>();
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Something went wrong while trying to get article statuses.");
            }
        }
        [HttpGet, Route("getSeasons")]
        public IActionResult GetSeasons()
        {
            try
            {
                var result = Season.SeasonList;
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get seasons.");
            }
        }
        [HttpGet, Route("getTwentyFourHourTimings")]
        public IActionResult GetTwentyFourHourTimings()
        {
            try
            {
                var result = TwentyFourHourTiming.TwentyFourHourTimingList;
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get timings.");
            }
        }
        #endregion basic

        #region article-image
        [HttpPost]
        [Route("uploadImage/{id}")]
        public async Task<IActionResult> UploadCoverImage(Int64 id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var article = _articleService.GetArticleById(id);
                if (article == null) return NotFound();

                if (file == null) return BadRequest("No file found");
                if (file.Length > photoSettings.MaxBytes) return BadRequest(" Image shouldn't be >10MB.");
                
                if (!photoSettings.AcceptedFileTypes.Any(x => x.ToLower() == Path.GetExtension(file.FileName.ToLower())))
                {
                    return BadRequest("Invalid file type.");
                }
                string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\ArticleCoverImages");
                if (!String.IsNullOrEmpty(article.ImagePath))
                {
                    var fileInfo = (uploadFolderPath + "\\" + article.ImagePath);
                    /*Delete old image from Folder: */
                    System.IO.File.Delete(fileInfo);
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
                    imagename = file.FileName;
                    var filepath = Path.Combine(uploadFolderPath, imagepath);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    article.ImagePath = imagepath;
                    article.ImageName = imagename;
                }
                
                var id_= await _articleService.UpdateImage(article);
                article.ImagePath = "Uploads\\ArticleCoverImages" + "\\" + article.ImagePath;
                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while trying to get article images.");
            }
        }
        [HttpPost]
        [Route("uploadIcon/{id}")]
        public async Task<IActionResult> UploadIcon(Int64 id)
        {
            try
            {
                var file_ = Request.Form.Files.First();
                var article = _articleService.GetArticleById(id);
                if (article == null) return NotFound();

                if (file_ == null) return BadRequest("No file found");

                if (file_.Length > photoSettings.MaxBytes) return BadRequest(" Image shouldn't be >10MB.");
                if (!photoSettings.AcceptedFileTypes.Any(x => x.ToLower() == Path.GetExtension(file_.FileName.ToLower())))
                {
                    return BadRequest("Invalid file type.");
                }

                string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\ArticleIcons");
                string uploadIconPath = Path.Combine(host.ContentRootPath, "Uploads\\ArticleIcons");
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                uploadIconPath = Path.Combine(host.ContentRootPath, "Uploads\\ArticleIcons");
                if (!Directory.Exists(uploadIconPath))
                {
                    Directory.CreateDirectory(uploadIconPath);
                }

                var iconname = string.Empty;
                if (file_ != null)
                {
                    iconname = Guid.NewGuid().ToString() + Path.GetExtension(file_.FileName);
                    var filepath = Path.Combine(uploadFolderPath, iconname);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file_.CopyToAsync(stream);
                        article.ImagePath = iconname;
                        await _articleService.Update(article);
                    }
                }


                var IconPath = "Uploads\\ArticleIcons" + _articleService.GetArticleById(id).ImagePath;

                return Ok(new {IconPath= IconPath });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while trying to get article images.");
            }
        }
        
        [HttpDelete]
        [Route("DeleteImage/{Id}")]
        public async Task<IActionResult> DeleteImage(Int64 Id)
        {
            try
            {
                /*Preserve objectname in some value: */
                var article= _articleService.GetArticleById(Id);
                /*Delete from Database: */
                var imagepath = article.ImagePath;
                article.ImagePath = "";
                article.ImageName = "";
                await _articleService.Update(article);
                var uploadImagePath = Path.Combine(host.ContentRootPath, "Uploads\\ArticleCoverImages");
                var fileInfo = (uploadImagePath + "\\" + imagepath);
                /*Delete from Folder: */
                System.IO.File.Delete(fileInfo);
                /*Now return remaining images: */
                return Ok(article);
            }
            catch (Exception e)
            {
                return BadRequest("Failed to Delete the Image.");
            }
        }
        #endregion article-image

        #region article-reference

        [HttpGet]
        [Route("GetReferenceImage/{id}")]
        public IActionResult GetReferenceImage(Int64 id)
        {
            var image = _articleService.GetArticleReferenceImage(id);
            return Ok(image);
        }

        [HttpDelete]
        [Route("DeleteRef/{referenceId}")]
        public async Task<IActionResult> DeleteRef(Int64 referenceId)
        {
            try
            {
                /*Preserve objectname in some value: */
                var referenceImage = _articleService.GetArticleReferenceImagePath(referenceId);
                /*Delete from Database: */
                _articleService.DeleteRef(referenceId);
                if (referenceImage.ImagePath != null)
                {
                    var uploadImagePath = Path.Combine(host.ContentRootPath, "Uploads\\ReferenceImages");
                    var fileInfo = (uploadImagePath + "\\" + referenceImage.ImagePath);
                    /*Delete from Disk: */
                    System.IO.File.Delete(fileInfo);
                }
                return Ok(new { Success = true });
            }
            catch (Exception e)
            {
                return BadRequest("Failed to Delete the Image.");
            }
        }

        [HttpPost]
        [Route("UploadRefImage/{id}")]
        public async Task<IActionResult> UploadRefImage(Int64 id)
        {
            try
            {
                var files = Request.Form.Files;
                var reference = _articleService.GetArticleReferenceImagePath(id);
                if (reference == null) return NotFound();
                if (reference.ImagePath != null)
                {
                    var _uploadImagePath = Path.Combine(host.ContentRootPath, "Uploads\\ReferenceImages");
                    string imageName = _articleService.GetRefImageName(id);
                    var fileInfo = (_uploadImagePath + "\\" + imageName);
                    System.IO.File.Delete(fileInfo);
                }

                if (files == null || !files.Any()) return BadRequest("No file found");
                foreach (var item in files)
                {
                    if (item.Length > photoSettings.MaxBytes) return BadRequest(" Max file size exceed.");
                    if (!photoSettings.AcceptedFileTypes.Any(x => x.ToLower() == Path.GetExtension(item.FileName.ToLower())))
                    {
                        return BadRequest("Invalid file type.");
                    }
                }

                string uploadFolderPath = Path.Combine(host.ContentRootPath, "Uploads\\ReferenceImages");
                string uploadImagePath = Path.Combine(host.ContentRootPath, "Uploads\\ReferenceImages");
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                uploadImagePath = Path.Combine(host.ContentRootPath, "Uploads\\ReferenceImages");
                if (!Directory.Exists(uploadImagePath))
                {
                    Directory.CreateDirectory(uploadImagePath);
                }
                var listfilename = new List<string>();
                foreach (var item in files)
                {
                    var filename = string.Empty;
                    if (item != null)
                    {
                        filename = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        var filepath = Path.Combine(uploadImagePath, filename);
                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                            listfilename.Add(filename);
                        }
                    }
                }
                await _articleService.UploadRefImage(id, listfilename.FirstOrDefault());
                var image = _articleService.GetArticleReferenceImage(id);
                return Ok(image);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while dealing with Article Reference Image.");
            }
        }

        [HttpPost, Route("createRef")]
        public IActionResult CreateRef(ArticleReferenceDTO model)
        {
            try
                {
                var result = _articleService.CreateRef(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to create a Reference.");
            }
        }
        [HttpPost, Route("updateRef")]
        public IActionResult UpdateRef(ArticleReferenceDTO model)
        {
            try
            {
                var result = _articleService.UpdateRef(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to update a Reference.");
            }
        }
        [HttpGet, Route("getReferenceById/{id}")]
        public IActionResult GetReferenceById(Int64 id)
        {
            try
            {
                var result = _articleService.GetReferenceById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get a Reference.");
            }
        }
        [HttpGet, Route("getReferencesByArticleId/{articleId}")]
        public IActionResult GetReferencesByArticleId(Int64 articleId)
        {
            try
            {
                var result = _articleService.GetReferencesByArticleId(articleId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get Article References.");
            }
        }

        #endregion article-reference

    }
}