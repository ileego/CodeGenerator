using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApplicationSystem;
using CodeGenerator.Core.Repository.ApplicationSystem;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.ApplicationSystem
{
    /// <summary>
    /// 应用系统 - 应用服务
    /// </summary>
    public class ApplicationSystemService : AppService, IApplicationSystemService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApplicationSystemRepository _applicationSystemRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApplicationSystemService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApplicationSystemRepository applicationSystemRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._applicationSystemRepository = applicationSystemRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApplicationSystemInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationSystem = _mapper.Map<Core.Entities.ApplicationSystem>(input);
			applicationSystem .Id = SnowflakeId.Default().NextId();
            await _applicationSystemRepository.InsertAsync(applicationSystem);
            return AppServiceResult(applicationSystem.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApplicationSystemInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationSystem = await _applicationSystemRepository.FindByIdAsync(id);
            if (null == applicationSystem)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			applicationSystem = _mapper.Map(input, applicationSystem);
            await _applicationSystemRepository.UpdateAsync(applicationSystem);
            return AppServiceResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> DeleteAsync(long id)
        {
            var applicationSystem = await _applicationSystemRepository.FindByIdAsync(id);
            if (null == applicationSystem)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _applicationSystemRepository.DeleteAsync(id);
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApplicationSystemDto>> GetByIdAsync(long id)
        {
            var applicationSystem = await _applicationSystemRepository.FindByIdAsync(id);
            if (null == applicationSystem)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApplicationSystemDto>(applicationSystem);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApplicationSystemDto>>> QueryListAsync(ApplicationSystemParamsDto queryParams)
        {
            var query = _applicationSystemRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApplicationSystem, ApplicationSystemDto>(
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
        private async Task CheckReferenceAsync(ApplicationSystemInputDto input)
        {

            return;
        }
    }
}