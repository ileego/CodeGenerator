using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.Employee;
using CodeGenerator.Core.Repository.Employee;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.Department;


namespace CodeGenerator.Application.Services.Employee
{
    /// <summary>
    /// 工作人员 - 应用服务
    /// </summary>
    public class EmployeeService : AppService, IEmployeeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public EmployeeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IEmployeeRepository employeeRepository,
			IDepartmentRepository departmentRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._employeeRepository = employeeRepository;
			this._departmentRepository = departmentRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(EmployeeInputDto input)
        {
            await CheckReferenceAsync(input);
            var employee = _mapper.Map<Core.Entities.Employee>(input);
			employee .Id = SnowflakeId.Default().NextId();
            await _employeeRepository.InsertAsync(employee);
            return AppServiceResult(employee.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, EmployeeInputDto input)
        {
            await CheckReferenceAsync(input);
            var employee = await _employeeRepository.FindByIdAsync(id);
            if (null == employee)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			employee = _mapper.Map(input, employee);
            await _employeeRepository.UpdateAsync(employee);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var employee = await _employeeRepository.FindByIdAsync(id);
            if (null == employee)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _employeeRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<EmployeeDto>> GetByIdAsync(long id)
        {
            var employee = await _employeeRepository.FindByIdAsync(id);
            if (null == employee)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<EmployeeDto>(employee);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<EmployeeDto>>> QueryListAsync(EmployeeParamsDto queryParams)
        {
            var query = _employeeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.Employee, EmployeeDto>(
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
        private async Task CheckReferenceAsync(EmployeeInputDto input)
        {
			var department = await _departmentRepository.FindByIdAsync(input.DepartmentId);
			if (null == department)
			{
				throw new AppServiceException("引用的[所属部门]记录不存在");
			}

            return;
        }
    }
}