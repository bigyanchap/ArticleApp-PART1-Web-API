using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilbaLeaf.Service.Infrastructure
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetAll();
        Task<CountryDTO> GetCountryById(int id);
        Task<QueryResult<CountryDTO>> Country(QueryObject queryObject);
        CountryDTO GetCountryById(int Id, bool isAuthorized);
        CountryDTO GetCountryById(int Id, Expression<Func<Country, bool>> where = null, params Expression<Func<Country, object>>[] includeExpressions);
        Task<int> Create(CountryDTO model);
        Task<int> Update(CountryDTO model);
        Task Delete(int id);
        void SaveChanges();
        bool CheckExistence(string countryName);
    }
}
