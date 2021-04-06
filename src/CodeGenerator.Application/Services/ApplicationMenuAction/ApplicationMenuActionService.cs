using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApplicationMenuAction;
using CodeGenerator.Core.Repository.ApplicationMenuAction;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationMenu;
using CodeGenerator.Core.Repository.ApplicationAction;


namespace CodeGenerator.Application.Services.ApplicationMenuAction
{
    /// <summary>
    /// 应用菜单功能关联 - 应用服务
    /// </summary>
    public class ApplicationMenuActionService : AppService, IApplicationMenuActionService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApplicationMenuActionRepository _applicationMenuActionRepository;
		private readonly IApplicationMenuRepository _applicationMenuRepository;
		private readonly IApplicationActionRepository _applicationActionRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApplicationMenuActionService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApplicationMenuActionRepository applicationMenuActionRepository,
			IApplicationMenuRepository applicationMenuRepository,
			IApplicationActionRepository applicationActionRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._applicationMenuActionRepository = applicationMenuActionRepository;
			this._applicationMenuRepository = applicationMenuRepository;
			this._applicationActionRepository = applicationActionRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApplicationMenuActionInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationMenuAction = _mapper.Map<Core.Entities.ApplicationMenuAction>(input);
			applicationMenuAction .Id = SnowflakeId.Default().NextId();
            await _applicationMenuActionRepository.InsertAsync(applicationMenuAction);
            return AppServiceResult(applicationMenuAction.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApplicationMenuActionInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationMenuAction = await _applicationMenuActionRepository.FindByIdAsync(id);
            if (null == applicationMenuAction)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			applicationMenuAction = _mapper.Map(input, applicationMenuAction);
            await _applicationMenuActionRepository.UpdateAsync(applicationMenuAction);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var applicationMenuAction = await _applicationMenuActionRepository.FindByIdAsync(id);
            if (null == applicationMenuAction)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _applicationMenuActionRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApplicationMenuActionDto>> GetByIdAsync(long id)
        {
            var applicationMenuAction = await _applicationMenuActionRepository.FindByIdAsync(id);
            if (null == applicationMenuAction)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApplicationMenuActionDto>(applicationMenuAction);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApplicationMenuActionDto>>> QueryListAsync(ApplicationMenuActionParamsDto queryParams)
        {
            var query = _applicationMenuActionRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApplicationMenuAction, ApplicationMenuActionDto>(
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
        private async Task CheckReferenceAsync(ApplicationMenuActionInputDto input)
        {
			var applicationMenu = await _applicationMenuRepository.FindByIdAsync(input.ApplicationMenuId);
			if (null == applicationMenu)
			{
				throw new AppServiceException("引用的[应用菜单]记录不存在");
			}
			var applicationAction = await _applicationActionRepository.FindByIdAsync(input.ApplicationActionId);
			if (null == applicationAction)
			{
				throw new AppServiceException("引用的[应用功能]记录不存在");
			}

            return;
        }
    }
}