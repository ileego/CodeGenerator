using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.Organization;
using CodeGenerator.Core.Repository.Organization;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.OrganizationType;


namespace CodeGenerator.Application.Services.Organization
{
    /// <summary>
    /// 组织 - 应用服务
    /// </summary>
    public class OrganizationService : AppService, IOrganizationService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IOrganizationRepository _organizationRepository;
		private readonly IOrganizationTypeRepository _organizationTypeRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public OrganizationService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IOrganizationRepository organizationRepository,
			IOrganizationTypeRepository organizationTypeRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._organizationRepository = organizationRepository;
			this._organizationTypeRepository = organizationTypeRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(OrganizationInputDto input)
        {
            await CheckReferenceAsync(input);
            var organization = _mapper.Map<Core.Entities.Organization>(input);
			organization .Id = SnowflakeId.Default().NextId();
            await _organizationRepository.InsertAsync(organization);
            await SaveAsync();
            return AppServiceResult(organization.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, OrganizationInputDto input)
        {
            await CheckReferenceAsync(input);
            var organization = await _organizationRepository.FindByIdAsync(id);
            if (null == organization)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			organization = _mapper.Map(input, organization);
            await _organizationRepository.UpdateAsync(organization);
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
            var organization = await _organizationRepository.FindByIdAsync(id);
            if (null == organization)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _organizationRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<OrganizationDto>> GetByIdAsync(long id)
        {
            var organization = await _organizationRepository.FindByIdAsync(id);
            if (null == organization)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<OrganizationDto>(organization);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<OrganizationDto>>> QueryListAsync(OrganizationParamsDto queryParams)
        {
            var query = _organizationRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.Organization, OrganizationDto>(
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
        private async Task CheckReferenceAsync(OrganizationInputDto input)
        {
			if (input.OrganizationId.HasValue)
			{
				var organization = await _organizationRepository.FindByIdAsync(input.OrganizationId.Value);
				if (null == organization)
				{
					throw new AppServiceException("引用的[上级组织]记录不存在");
				}
			}
			var organizationType = await _organizationTypeRepository.FindByIdAsync(input.OrganizationTypeId);
			if (null == organizationType)
			{
				throw new AppServiceException("引用的[组织类型]记录不存在");
			}

            return;
        }
    }
}