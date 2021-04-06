using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.District;
using CodeGenerator.Core.Repository.District;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.District
{
    /// <summary>
    /// 行政区 - 应用服务
    /// </summary>
    public class DistrictService : AppService, IDistrictService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IDistrictRepository _districtRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public DistrictService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IDistrictRepository districtRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._districtRepository = districtRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(DistrictInputDto input)
        {
            await CheckReferenceAsync(input);
            var district = _mapper.Map<Core.Entities.District>(input);
			district .Id = SnowflakeId.Default().NextId();
            await _districtRepository.InsertAsync(district);
            return AppServiceResult(district.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, DistrictInputDto input)
        {
            await CheckReferenceAsync(input);
            var district = await _districtRepository.FindByIdAsync(id);
            if (null == district)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			district = _mapper.Map(input, district);
            await _districtRepository.UpdateAsync(district);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var district = await _districtRepository.FindByIdAsync(id);
            if (null == district)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _districtRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<DistrictDto>> GetByIdAsync(long id)
        {
            var district = await _districtRepository.FindByIdAsync(id);
            if (null == district)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<DistrictDto>(district);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<DistrictDto>>> QueryListAsync(DistrictParamsDto queryParams)
        {
            var query = _districtRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.District, DistrictDto>(
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
        private async Task CheckReferenceAsync(DistrictInputDto input)
        {

            return;
        }
    }
}