using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BilbaLeaf.DTO;
using BilbaLeaf.Service.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BilbaLeaf.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DataManagerController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public DataManagerController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("killOrphanCoverImages")]
        public IActionResult KillOrphanCoverImages()
        {
            try
            {
                string[] imagesNames = Directory.GetFiles("Uploads\\ArticleCoverImages");
                foreach(string fullPath in imagesNames)
                {
                    var imagePath = fullPath.Split("\\").Last();
                    bool imageExisting = _articleService.IsExistingCoverImage(imagePath);
                    if (!imageExisting)
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Kill orphan Cover Images.");
            }
        }

    }
    
}