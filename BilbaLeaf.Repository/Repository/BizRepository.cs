using BilbaLeaf.Entities;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Repository.Repository
{
    public class BizRepository : RepositoryBase<Biz>, IBizRepository
    {
        public BizRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
