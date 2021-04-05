using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ApplicationMenu;
using CodeGenerator.Core.Repository.ApplicationMenu;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApplicationSystem;


namespace CodeGenerator.Application.Services.ApplicationMenu
{
    /// <summary>
    /// 应用菜单 - 应用服务
    /// </summary>
    public class ApplicationMenuService : AppService, IApplicationMenuService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IApplicationMenuRepository _applicationMenuRepository;
		private readonly IApplicationSystemRepository _applicationSystemRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ApplicationMenuService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IApplicationMenuRepository applicationMenuRepository,
			IApplicationSystemRepository applicationSystemRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._applicationMenuRepository = applicationMenuRepository;
			this._applicationSystemRepository = applicationSystemRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ApplicationMenuInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationMenu = _mapper.Map<Core.Entities.ApplicationMenu>(input);
			applicationMenu .Id = SnowflakeId.Default().NextId();
            await _applicationMenuRepository.InsertAsync(applicationMenu);
            await SaveAsync();
            return AppServiceResult(applicationMenu.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ApplicationMenuInputDto input)
        {
            await CheckReferenceAsync(input);
            var applicationMenu = await _applicationMenuRepository.FindByIdAsync(id);
            if (null == applicationMenu)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			applicationMenu = _mapper.Map(input, applicationMenu);
            await _applicationMenuRepository.UpdateAsync(applicationMenu);
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
            var applicationMenu = await _applicationMenuRepository.FindByIdAsync(id);
            if (null == applicationMenu)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _applicationMenuRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ApplicationMenuDto>> GetByIdAsync(long id)
        {
            var applicationMenu = await _applicationMenuRepository.FindByIdAsync(id);
            if (null == applicationMenu)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ApplicationMenuDto>(applicationMenu);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ApplicationMenuDto>>> QueryListAsync(ApplicationMenuParamsDto queryParams)
        {
            var query = _applicationMenuRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ApplicationMenu, ApplicationMenuDto>(
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
        private async Task CheckReferenceAsync(ApplicationMenuInputDto input)
        {
			if (input.ApplicationMenuId.HasValue)
			{
				var applicationMenu = await _applicationMenuRepository.FindByIdAsync(input.ApplicationMenuId.Value);
				if (null == applicationMenu)
				{
					throw new AppServiceException("引用的[父级应用菜单]记录不存在");
				}
			}
			var applicationSystem = await _applicationSystemRepository.FindByIdAsync(input.ApplicationSystemId);
			if (null == applicationSystem)
			{
				throw new AppServiceException("引用的[所属应用]记录不存在");
			}

            return;
        }
    }
}