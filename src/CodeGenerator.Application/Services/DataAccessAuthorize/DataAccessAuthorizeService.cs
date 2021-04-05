using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.DataAccessAuthorize;
using CodeGenerator.Core.Repository.DataAccessAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationRole;
using CodeGenerator.Core.Repository.District;


namespace CodeGenerator.Application.Services.DataAccessAuthorize
{
    /// <summary>
    /// 数据访问授权 - 应用服务
    /// </summary>
    public class DataAccessAuthorizeService : AppService, IDataAccessAuthorizeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IDataAccessAuthorizeRepository _dataAccessAuthorizeRepository;
		private readonly IApplicationRoleRepository _applicationRoleRepository;
		private readonly IDistrictRepository _districtRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public DataAccessAuthorizeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IDataAccessAuthorizeRepository dataAccessAuthorizeRepository,
			IApplicationRoleRepository applicationRoleRepository,
			IDistrictRepository districtRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._dataAccessAuthorizeRepository = dataAccessAuthorizeRepository;
			this._applicationRoleRepository = applicationRoleRepository;
			this._districtRepository = districtRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(DataAccessAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var dataAccessAuthorize = _mapper.Map<Core.Entities.DataAccessAuthorize>(input);
			dataAccessAuthorize .Id = SnowflakeId.Default().NextId();
            await _dataAccessAuthorizeRepository.InsertAsync(dataAccessAuthorize);
            await SaveAsync();
            return AppServiceResult(dataAccessAuthorize.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, DataAccessAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var dataAccessAuthorize = await _dataAccessAuthorizeRepository.FindByIdAsync(id);
            if (null == dataAccessAuthorize)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			dataAccessAuthorize = _mapper.Map(input, dataAccessAuthorize);
            await _dataAccessAuthorizeRepository.UpdateAsync(dataAccessAuthorize);
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
            var dataAccessAuthorize = await _dataAccessAuthorizeRepository.FindByIdAsync(id);
            if (null == dataAccessAuthorize)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _dataAccessAuthorizeRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<DataAccessAuthorizeDto>> GetByIdAsync(long id)
        {
            var dataAccessAuthorize = await _dataAccessAuthorizeRepository.FindByIdAsync(id);
            if (null == dataAccessAuthorize)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<DataAccessAuthorizeDto>(dataAccessAuthorize);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<DataAccessAuthorizeDto>>> QueryListAsync(DataAccessAuthorizeParamsDto queryParams)
        {
            var query = _dataAccessAuthorizeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.DataAccessAuthorize, DataAccessAuthorizeDto>(
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
        private async Task CheckReferenceAsync(DataAccessAuthorizeInputDto input)
        {
			var applicationRole = await _applicationRoleRepository.FindByIdAsync(input.ApplicationRoleId);
			if (null == applicationRole)
			{
				throw new AppServiceException("引用的[角色]记录不存在");
			}
			var district = await _districtRepository.FindByIdAsync(input.DistrictId);
			if (null == district)
			{
				throw new AppServiceException("引用的[行政区]记录不存在");
			}

            return;
        }
    }
}