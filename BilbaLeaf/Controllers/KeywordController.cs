using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Service.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BilbaLeaf.Entities.Enums;

namespace BilbaLeaf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private IKeywordService _keywordService;
        public KeywordController(IKeywordService keywordService)
        {
            this._keywordService=keywordService;
        }

        #region Synonym
        [HttpGet]
        [Route("getSynById/{id}")]
        public IActionResult GetSynonymById(Int64 id)
        {
            try
            {
                var result = _keywordService.GetSynonymById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route("GetManySynonym/{keywordId}")]
        public IActionResult GetManySynonym(Int64 keywordId)
        {
            try
            {
                var result = _keywordService.GetManySynonym(keywordId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("upsertSyn")]
        public async Task<IActionResult> UpsertSynonym(SynonymDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Int64 result = 0;
                    if (model.Id == 0)
                    {
                        result = await _keywordService.CreateSynonym(model);
                    }
                    else
                    {
                        result = await _keywordService.UpdateSynonym(model);
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
                return BadRequest("Something went wrong while trying to Upsert Synonym."); ;
            }
        }
        [HttpGet]
        [Route("getLangEnum")]
        public IActionResult GetLanguageEnum()
        {
            try
            {
                Enums enums = new Enums();
                var result = enums.ExportEnum<Language>();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("delSynById/{id}")]
        public async Task<IActionResult> DeleteSynonym(Int64 id)
        {
            try
            {
                if (id > 0)
                {
                    await _keywordService.DeleteSynonym(id);
                    return Ok(new { Success = true });
                }
                else
                {
                    return BadRequest("Not Found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        #endregion Synonym

        #region Keyword

        [HttpPost]
        [Route("allPaged")]
        public IActionResult GetKeywordsPaged(QueryObject query)
        {
            try
            {
                var result = _keywordService.GetAllPaged(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get all Keyword.");
            }
        }

        [HttpPost]
        [Route("upsert")]
        public async Task<IActionResult> UpsertKeyword(KeywordDTO keyword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkkeyword = _keywordService.CheckKeywordName(keyword);
                    if (checkkeyword)
                    {
                        return BadRequest("Keyword already exists.");
                    }
                    Int64 result=0;
                    if (keyword.Id == 0)
                    {
                        result = await _keywordService.Create(keyword);
                    }
                    else
                    {
                        result = await _keywordService.Update(keyword);
                    }
                    if (result == -1)
                    {
                        return BadRequest("Keyword cannot contain space.");
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
                return BadRequest("Something went wrong while trying upsert Keyword.");
            }
        }

        [HttpDelete]
        [Route("del/{id}")]
        public async Task<IActionResult> DeleteKeyword(Int64 id)
        {
            try
            {
                if (id>0)
                {
                    await _keywordService.Delete(id);
                    return Ok(new { Success = true });
                }
                else
                {
                    return BadRequest("Not Found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getbyId/{id}")]
        public async Task<ActionResult> GetById(Int64 id)
        {
            try
            {
                var result =await _keywordService.GetKeywordById(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest("Something went wrong while trying to get a Keyword by Id.");
            }
        }
        #endregion Keyword

        #region ArticleKeyword
        [HttpPost]
        [Route("getKWbySubStr")]
        public async Task<ActionResult> GetKeywordsBySubStr(WordVM word)
        {
            try
            {
                var result = _keywordService.GetKeywordsBySubStr(word.Word);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to get a Keyword by Sub String.");
            }
        }
        [HttpPost]
        [Route("saveKeywordBundle")]
        public async Task<ActionResult> SaveKeywordBundle(KeywordBundle bundle)
        {
            try
            {

                var success = await _keywordService.SaveKeywords(bundle);
                return Ok(new {Success=success});
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Save Keywords");
            }
        }

        [HttpGet]
        [Route("getKeywords/{articleId}")]
        public async Task<ActionResult> GetKewords(Int64 articleId)
        {
            try
            {

                var result = _keywordService.GetKeywordsByArticleId(articleId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Get Keywords");
            }
        }
        #endregion ArticleKeyword
    }
}