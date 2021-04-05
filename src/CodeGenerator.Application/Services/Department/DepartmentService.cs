using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.Department;
using CodeGenerator.Core.Repository.Department;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.Organization;


namespace CodeGenerator.Application.Services.Department
{
    /// <summary>
    /// 部门 - 应用服务
    /// </summary>
    public class DepartmentService : AppService, IDepartmentService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IOrganizationRepository _organizationRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public DepartmentService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IDepartmentRepository departmentRepository,
			IOrganizationRepository organizationRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._departmentRepository = departmentRepository;
			this._organizationRepository = organizationRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(DepartmentInputDto input)
        {
            await CheckReferenceAsync(input);
            var department = _mapper.Map<Core.Entities.Department>(input);
			department .Id = SnowflakeId.Default().NextId();
            await _departmentRepository.InsertAsync(department);
            await SaveAsync();
            return AppServiceResult(department.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, DepartmentInputDto input)
        {
            await CheckReferenceAsync(input);
            var department = await _departmentRepository.FindByIdAsync(id);
            if (null == department)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			department = _mapper.Map(input, department);
            await _departmentRepository.UpdateAsync(department);
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
            var department = await _departmentRepository.FindByIdAsync(id);
            if (null == department)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _departmentRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<DepartmentDto>> GetByIdAsync(long id)
        {
            var department = await _departmentRepository.FindByIdAsync(id);
            if (null == department)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<DepartmentDto>(department);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<DepartmentDto>>> QueryListAsync(DepartmentParamsDto queryParams)
        {
            var query = _departmentRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.Department, DepartmentDto>(
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
        private async Task CheckReferenceAsync(DepartmentInputDto input)
        {
			if (input.DepartmentId.HasValue)
			{
				var department = await _departmentRepository.FindByIdAsync(input.DepartmentId.Value);
				if (null == department)
				{
					throw new AppServiceException("引用的[上级部门]记录不存在");
				}
			}
			var organization = await _organizationRepository.FindByIdAsync(input.OrganizationId);
			if (null == organization)
			{
				throw new AppServiceException("引用的[所属组织]记录不存在");
			}

            return;
        }
    }
}