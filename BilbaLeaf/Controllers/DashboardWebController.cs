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
    public class DashboardWebController : ControllerBase
    {
        private IArticleService _articleService;
        public DashboardWebController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _articleService.GetForWeb();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Get Articles.");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //var result = await _articleService.GetArticleById(id);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Get Article.");
            }
        }

    }
}