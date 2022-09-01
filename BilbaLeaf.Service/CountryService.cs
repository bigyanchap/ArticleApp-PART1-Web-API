using AutoMapper;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Service.Extension;
using BilbaLeaf.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilbaLeaf.Service
{
    public class CountryService : ICountryService
    {
        ICountryRepository _countryRepository;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        public CountryService(
            ICountryRepository countryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CountryDTO> GetAll()
        {
            IEnumerable<Country> countries = _countryRepository.GetAll().OrderBy(c=>c.Name);
            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }

        public async Task<int> Create(CountryDTO model)
        {
            Country obj = _mapper.Map<Country>(model);
            await _countryRepository.Add(obj);
            await _unitOfWork.Commit();
            return obj.Id;
        }
        public async Task<int> Update(CountryDTO model)
        {

            var country = await _countryRepository.GetSingle(model.Id);
            if (country != null)
            {
                country.Name = model.Name;
                country.Code = model.Code;
                await _unitOfWork.Commit();
            }
            return country.Id;
        }
        public async Task Delete(int id)
        {
            var country = await _countryRepository.GetSingle(id);
            _countryRepository.Delete(country);
            await _unitOfWork.Commit();
        }

        public async Task<CountryDTO> GetCountryById(int id)
        {
            var country = await _countryRepository.GetSingle(id);
            if (country == null) return new CountryDTO();
            return _mapper.Map<CountryDTO>(country);
        }

        public CountryDTO GetCountryById(int Id, bool isAuthorized)
        {
            throw new NotImplementedException();
        }

        public CountryDTO GetCountryById(int Id, Expression<Func<Country, bool>> where = null, params Expression<Func<Country, object>>[] includeExpressions)
        {
            throw new NotImplementedException();
            //return _countryRepository.GetById(Id, where, includeExpressions);
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public async Task<QueryResult<CountryDTO>> Country(QueryObject query)
        {
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "Name";
            }
            var columnMap = new Dictionary<string, Expression<Func<Country, object>>>()
            {
                ["Id"] = p => p.Id,
                ["Name"] = p => p.Name,
                ["CountryCode"] = p => p.Code
            };
            var country = _countryRepository.All;

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                country = country.Where(a => a.Name.Trim().ToLower().Contains(query.SearchString.Trim().ToLower())
                                            || a.Code.Trim().ToLower().Contains(query.SearchString.Trim().ToLower()));
            }

            var result = await country.ApplyOrdering(query, columnMap).ToListAsync();
            var filterdatacount = country.Count();
            var pagination = _mapper.Map<List<CountryDTO>>(result);
            var queryResult = new QueryResult<CountryDTO>
            {
                TotalItems = country.Count(),
                Items = pagination
            };
            return queryResult;
        }
        public bool CheckExistence(string countryName) =>
            _countryRepository.FindBy(c => c.Name.ToLower().Trim() == countryName.ToLower().Trim()).Count() > 0; 

    }
}
