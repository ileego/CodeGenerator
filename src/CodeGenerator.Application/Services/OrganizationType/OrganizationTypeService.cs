using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.OrganizationType;
using CodeGenerator.Core.Repository.OrganizationType;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.OrganizationType
{
    /// <summary>
    /// 组织类型 - 应用服务
    /// </summary>
    public class OrganizationTypeService : AppService, IOrganizationTypeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IOrganizationTypeRepository _organizationTypeRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public OrganizationTypeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IOrganizationTypeRepository organizationTypeRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._organizationTypeRepository = organizationTypeRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(OrganizationTypeInputDto input)
        {
            await CheckReferenceAsync(input);
            var organizationType = _mapper.Map<Core.Entities.OrganizationType>(input);
			organizationType .Id = SnowflakeId.Default().NextId();
            await _organizationTypeRepository.InsertAsync(organizationType);
            await SaveAsync();
            return AppServiceResult(organizationType.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, OrganizationTypeInputDto input)
        {
            await CheckReferenceAsync(input);
            var organizationType = await _organizationTypeRepository.FindByIdAsync(id);
            if (null == organizationType)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			organizationType = _mapper.Map(input, organizationType);
            await _organizationTypeRepository.UpdateAsync(organizationType);
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
            var organizationType = await _organizationTypeRepository.FindByIdAsync(id);
            if (null == organizationType)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _organizationTypeRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<OrganizationTypeDto>> GetByIdAsync(long id)
        {
            var organizationType = await _organizationTypeRepository.FindByIdAsync(id);
            if (null == organizationType)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<OrganizationTypeDto>(organizationType);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<OrganizationTypeDto>>> QueryListAsync(OrganizationTypeParamsDto queryParams)
        {
            var query = _organizationTypeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.OrganizationType, OrganizationTypeDto>(
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
        private async Task CheckReferenceAsync(OrganizationTypeInputDto input)
        {

            return;
        }
    }
}