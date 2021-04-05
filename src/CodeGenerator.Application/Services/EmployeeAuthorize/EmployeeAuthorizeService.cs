using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.EmployeeAuthorize;
using CodeGenerator.Core.Repository.EmployeeAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationRole;
using CodeGenerator.Core.Repository.Employee;


namespace CodeGenerator.Application.Services.EmployeeAuthorize
{
    /// <summary>
    /// 工作人员授权 - 应用服务
    /// </summary>
    public class EmployeeAuthorizeService : AppService, IEmployeeAuthorizeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IEmployeeAuthorizeRepository _employeeAuthorizeRepository;
		private readonly IApplicationRoleRepository _applicationRoleRepository;
		private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public EmployeeAuthorizeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IEmployeeAuthorizeRepository employeeAuthorizeRepository,
			IApplicationRoleRepository applicationRoleRepository,
			IEmployeeRepository employeeRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._employeeAuthorizeRepository = employeeAuthorizeRepository;
			this._applicationRoleRepository = applicationRoleRepository;
			this._employeeRepository = employeeRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(EmployeeAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var employeeAuthorize = _mapper.Map<Core.Entities.EmployeeAuthorize>(input);
			employeeAuthorize .Id = SnowflakeId.Default().NextId();
            await _employeeAuthorizeRepository.InsertAsync(employeeAuthorize);
            await SaveAsync();
            return AppServiceResult(employeeAuthorize.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, EmployeeAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var employeeAuthorize = await _employeeAuthorizeRepository.FindByIdAsync(id);
            if (null == employeeAuthorize)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			employeeAuthorize = _mapper.Map(input, employeeAuthorize);
            await _employeeAuthorizeRepository.UpdateAsync(employeeAuthorize);
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
            var employeeAuthorize = await _employeeAuthorizeRepository.FindByIdAsync(id);
            if (null == employeeAuthorize)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _employeeAuthorizeRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<EmployeeAuthorizeDto>> GetByIdAsync(long id)
        {
            var employeeAuthorize = await _employeeAuthorizeRepository.FindByIdAsync(id);
            if (null == employeeAuthorize)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<EmployeeAuthorizeDto>(employeeAuthorize);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<EmployeeAuthorizeDto>>> QueryListAsync(EmployeeAuthorizeParamsDto queryParams)
        {
            var query = _employeeAuthorizeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.EmployeeAuthorize, EmployeeAuthorizeDto>(
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
        private async Task CheckReferenceAsync(EmployeeAuthorizeInputDto input)
        {
			var applicationRole = await _applicationRoleRepository.FindByIdAsync(input.ApplicationRoleId);
			if (null == applicationRole)
			{
				throw new AppServiceException("引用的[角色]记录不存在");
			}
			var employee = await _employeeRepository.FindByIdAsync(input.EmployeeId);
			if (null == employee)
			{
				throw new AppServiceException("引用的[工作人员]记录不存在");
			}

            return;
        }
    }
}