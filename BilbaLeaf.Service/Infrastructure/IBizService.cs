using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BilbaLeaf.Service.Infrastructure
{
    public interface IBizService
    {

        Task<Int64> AddBiz(BizDTO categoryDTO);
        Task<BizDTO> GetBizById(Int64 id);
        Task UpdateBiz(BizDTO categoryDTO);
    }
}
