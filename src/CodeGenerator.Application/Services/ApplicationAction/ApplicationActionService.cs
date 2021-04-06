using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApplicationAction;
using CodeGenerator.Core.Repository.ApplicationAction;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.ApplicationAction
{
    /// <summary>
    /// 应用功能 - 应用服务
    /// </summary>
    public class ApplicationActionService : AppService, IApplicationActionService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApplicationActionRepository _applicationActionRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApplicationActionService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApplicationActionRepository applicationActionRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._applicationActionRepository = applicationActionRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApplicationActionInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationAction = _mapper.Map<Core.Entities.ApplicationAction>(input);
			applicationAction .Id = SnowflakeId.Default().NextId();
            await _applicationActionRepository.InsertAsync(applicationAction);
            return AppServiceResult(applicationAction.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApplicationActionInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationAction = await _applicationActionRepository.FindByIdAsync(id);
            if (null == applicationAction)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			applicationAction = _mapper.Map(input, applicationAction);
            await _applicationActionRepository.UpdateAsync(applicationAction);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var applicationAction = await _applicationActionRepository.FindByIdAsync(id);
            if (null == applicationAction)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _applicationActionRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApplicationActionDto>> GetByIdAsync(long id)
        {
            var applicationAction = await _applicationActionRepository.FindByIdAsync(id);
            if (null == applicationAction)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApplicationActionDto>(applicationAction);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApplicationActionDto>>> QueryListAsync(ApplicationActionParamsDto queryParams)
        {
            var query = _applicationActionRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApplicationAction, ApplicationActionDto>(
                query,
                queryParams.PageParams.Page,
                queryParams.PageParams.PageSize);
            return AppServiceResult(pagedData);
        }

        /// <summary>
        /// 检查引用的记录是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CheckReferenceAsync(ApplicationActionInputDto input)
        {

            return;
        }
    }
}