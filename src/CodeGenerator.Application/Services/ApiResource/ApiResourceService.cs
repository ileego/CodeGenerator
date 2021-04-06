using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApiResource;
using CodeGenerator.Core.Repository.ApiResource;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.ApiResource
{
    /// <summary>
    /// Api资源 - 应用服务
    /// </summary>
    public class ApiResourceService : AppService, IApiResourceService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApiResourceRepository _apiResourceRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApiResourceService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApiResourceRepository apiResourceRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._apiResourceRepository = apiResourceRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApiResourceInputDto input)
        {
            await CheckReferenceAsync(input);
            var apiResource = _mapper.Map<Core.Entities.ApiResource>(input);
			apiResource .Id = SnowflakeId.Default().NextId();
            await _apiResourceRepository.InsertAsync(apiResource);
            return AppServiceResult(apiResource.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApiResourceInputDto input)
        {
            await CheckReferenceAsync(input);
            var apiResource = await _apiResourceRepository.FindByIdAsync(id);
            if (null == apiResource)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			apiResource = _mapper.Map(input, apiResource);
            await _apiResourceRepository.UpdateAsync(apiResource);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var apiResource = await _apiResourceRepository.FindByIdAsync(id);
            if (null == apiResource)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _apiResourceRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApiResourceDto>> GetByIdAsync(long id)
        {
            var apiResource = await _apiResourceRepository.FindByIdAsync(id);
            if (null == apiResource)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApiResourceDto>(apiResource);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApiResourceDto>>> QueryListAsync(ApiResourceParamsDto queryParams)
        {
            var query = _apiResourceRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApiResource, ApiResourceDto>(
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
        private async Task CheckReferenceAsync(ApiResourceInputDto input)
        {

            return;
        }
    }
}