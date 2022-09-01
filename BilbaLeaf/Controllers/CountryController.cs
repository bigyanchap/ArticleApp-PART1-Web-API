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
    public class CountryController : ControllerBase
    {
        private ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _countryService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Get Countries.");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _countryService.GetCountryById(id);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Get Country.");
            }
        }

        [HttpPost]
        [Route("UpsertCountry")]
        public async Task<IActionResult> UpsertCountry(CountryDTO country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = 0;
                    country.Code = country.Code.ToUpper();
                    if (country.Id == 0)
                    {
                        result = await _countryService.Create(country);
                    }
                    else
                    {
                        result = await _countryService.Update(country);
                    }
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Modelset is not valid.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Upsert Country."); ;
            }
        }

        [Route("CountryItem")]
        [HttpPost]
        public async Task<IActionResult> CountryItem(QueryObject query)
        {
            var result = await _countryService.Country(query);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("DeleteCountry/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _countryService.Delete(id);
                    return Ok();
                }
                else
                {
                    return BadRequest("Not Found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong while trying to Delete Country.");
            }
        }

    }
}