using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApplicationRole;
using CodeGenerator.Core.Repository.ApplicationRole;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationSystem;


namespace CodeGenerator.Application.Services.ApplicationRole
{
    /// <summary>
    /// 角色 - 应用服务
    /// </summary>
    public class ApplicationRoleService : AppService, IApplicationRoleService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApplicationRoleRepository _applicationRoleRepository;
		private readonly IApplicationSystemRepository _applicationSystemRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApplicationRoleService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApplicationRoleRepository applicationRoleRepository,
			IApplicationSystemRepository applicationSystemRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._applicationRoleRepository = applicationRoleRepository;
			this._applicationSystemRepository = applicationSystemRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApplicationRoleInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationRole = _mapper.Map<Core.Entities.ApplicationRole>(input);
			applicationRole .Id = SnowflakeId.Default().NextId();
            await _applicationRoleRepository.InsertAsync(applicationRole);
            await SaveAsync();
            return AppServiceResult(applicationRole.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApplicationRoleInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationRole = await _applicationRoleRepository.FindByIdAsync(id);
            if (null == applicationRole)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			applicationRole = _mapper.Map(input, applicationRole);
            await _applicationRoleRepository.UpdateAsync(applicationRole);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var applicationRole = await _applicationRoleRepository.FindByIdAsync(id);
            if (null == applicationRole)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _applicationRoleRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApplicationRoleDto>> GetByIdAsync(long id)
        {
            var applicationRole = await _applicationRoleRepository.FindByIdAsync(id);
            if (null == applicationRole)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApplicationRoleDto>(applicationRole);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApplicationRoleDto>>> QueryListAsync(ApplicationRoleParamsDto queryParams)
        {
            var query = _applicationRoleRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApplicationRole, ApplicationRoleDto>(
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
        private async Task CheckReferenceAsync(ApplicationRoleInputDto input)
        {
			var applicationSystem = await _applicationSystemRepository.FindByIdAsync(input.ApplicationSystemId);
			if (null == applicationSystem)
			{
				throw new AppServiceException("引用的[所属应用]记录不存在");
			}

            return;
        }
    }
}