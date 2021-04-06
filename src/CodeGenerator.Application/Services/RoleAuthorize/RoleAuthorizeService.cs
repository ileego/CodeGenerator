using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.RoleAuthorize;
using CodeGenerator.Core.Repository.RoleAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationRole;
using CodeGenerator.Core.Repository.ApplicationMenu;


namespace CodeGenerator.Application.Services.RoleAuthorize
{
    /// <summary>
    /// 角色授权 - 应用服务
    /// </summary>
    public class RoleAuthorizeService : AppService, IRoleAuthorizeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IRoleAuthorizeRepository _roleAuthorizeRepository;
		private readonly IApplicationRoleRepository _applicationRoleRepository;
		private readonly IApplicationMenuRepository _applicationMenuRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public RoleAuthorizeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IRoleAuthorizeRepository roleAuthorizeRepository,
			IApplicationRoleRepository applicationRoleRepository,
			IApplicationMenuRepository applicationMenuRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._roleAuthorizeRepository = roleAuthorizeRepository;
			this._applicationRoleRepository = applicationRoleRepository;
			this._applicationMenuRepository = applicationMenuRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(RoleAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var roleAuthorize = _mapper.Map<Core.Entities.RoleAuthorize>(input);
			roleAuthorize .Id = SnowflakeId.Default().NextId();
            await _roleAuthorizeRepository.InsertAsync(roleAuthorize);
            return AppServiceResult(roleAuthorize.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, RoleAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var roleAuthorize = await _roleAuthorizeRepository.FindByIdAsync(id);
            if (null == roleAuthorize)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			roleAuthorize = _mapper.Map(input, roleAuthorize);
            await _roleAuthorizeRepository.UpdateAsync(roleAuthorize);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var roleAuthorize = await _roleAuthorizeRepository.FindByIdAsync(id);
            if (null == roleAuthorize)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _roleAuthorizeRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<RoleAuthorizeDto>> GetByIdAsync(long id)
        {
            var roleAuthorize = await _roleAuthorizeRepository.FindByIdAsync(id);
            if (null == roleAuthorize)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<RoleAuthorizeDto>(roleAuthorize);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<RoleAuthorizeDto>>> QueryListAsync(RoleAuthorizeParamsDto queryParams)
        {
            var query = _roleAuthorizeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.RoleAuthorize, RoleAuthorizeDto>(
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
        private async Task CheckReferenceAsync(RoleAuthorizeInputDto input)
        {
			var applicationRole = await _applicationRoleRepository.FindByIdAsync(input.ApplicationRoleId);
			if (null == applicationRole)
			{
				throw new AppServiceException("引用的[角色]记录不存在");
			}
			var applicationMenu = await _applicationMenuRepository.FindByIdAsync(input.ApplicationMenuId);
			if (null == applicationMenu)
			{
				throw new AppServiceException("引用的[应用菜单]记录不存在");
			}

            return;
        }
    }
}