using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.ClientsAuthorize;
using CodeGenerator.Core.Repository.ClientsAuthorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;
using CodeGenerator.Core.Repository.ApiResource;
using CodeGenerator.Core.Repository.Client;


namespace CodeGenerator.Application.Services.ClientsAuthorize
{
    /// <summary>
    /// 客户端授权 - 应用服务
    /// </summary>
    public class ClientsAuthorizeService : AppService, IClientsAuthorizeService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IClientsAuthorizeRepository _clientsAuthorizeRepository;
		private readonly IApiResourceRepository _apiResourceRepository;
		private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ClientsAuthorizeService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IClientsAuthorizeRepository clientsAuthorizeRepository,
			IApiResourceRepository apiResourceRepository,
			IClientRepository clientRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._clientsAuthorizeRepository = clientsAuthorizeRepository;
			this._apiResourceRepository = apiResourceRepository;
			this._clientRepository = clientRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ClientsAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var clientsAuthorize = _mapper.Map<Core.Entities.ClientsAuthorize>(input);
			clientsAuthorize .Id = SnowflakeId.Default().NextId();
            await _clientsAuthorizeRepository.InsertAsync(clientsAuthorize);
            await SaveAsync();
            return AppServiceResult(clientsAuthorize.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ClientsAuthorizeInputDto input)
        {
            await CheckReferenceAsync(input);
            var clientsAuthorize = await _clientsAuthorizeRepository.FindByIdAsync(id);
            if (null == clientsAuthorize)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			clientsAuthorize = _mapper.Map(input, clientsAuthorize);
            await _clientsAuthorizeRepository.UpdateAsync(clientsAuthorize);
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
            var clientsAuthorize = await _clientsAuthorizeRepository.FindByIdAsync(id);
            if (null == clientsAuthorize)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _clientsAuthorizeRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ClientsAuthorizeDto>> GetByIdAsync(long id)
        {
            var clientsAuthorize = await _clientsAuthorizeRepository.FindByIdAsync(id);
            if (null == clientsAuthorize)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ClientsAuthorizeDto>(clientsAuthorize);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ClientsAuthorizeDto>>> QueryListAsync(ClientsAuthorizeParamsDto queryParams)
        {
            var query = _clientsAuthorizeRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.ClientsAuthorize, ClientsAuthorizeDto>(
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
        private async Task CheckReferenceAsync(ClientsAuthorizeInputDto input)
        {
			var apiResource = await _apiResourceRepository.FindByIdAsync(input.ApiResourceId);
			if (null == apiResource)
			{
				throw new AppServiceException("引用的[Api资源]记录不存在");
			}
			var client = await _clientRepository.FindByIdAsync(input.ClientId);
			if (null == client)
			{
				throw new AppServiceException("引用的[客户端]记录不存在");
			}

            return;
        }
    }
}