using AutoMapper;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using static BilbaLeaf.Entities.Enums;

namespace LogiLync.Service
{
    public class BizService : IBizService
    {
        public IBizRepository _bizRepository;
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public BizService(IBizRepository bizRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper
            )
        {
            _bizRepository = bizRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Int64> AddBiz(BizDTO _biz_)
        {
            var biz = _mapper.Map<Biz>(_biz_);
            await _bizRepository.Add(biz);
            await _unitOfWork.Commit();
            return biz.Id;
        }

        public async Task<BizDTO> GetBizById(Int64 id)
        {
            var result = await _bizRepository.GetSingle(id);
            if (result == null) return new BizDTO();
            var biz = _mapper.Map<BizDTO>(result);
            return biz;

        }

        public async Task UpdateBiz(BizDTO _biz_)
        {
            var biz = await _bizRepository.GetSingle(_biz_.Id);
            if (biz != null)
            {
                //biz.Name = _biz_.Name;
                //biz.Description = _biz_.Description;
                await _unitOfWork.Commit();
            }
        }
    }
}
