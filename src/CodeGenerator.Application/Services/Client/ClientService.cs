using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodeGenerator.Application.DTOs.Client;
using CodeGenerator.Core.Repository.Client;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;
using CodeGenerator.Infra.Common.Uow;
using CodeGenerator.Infra.Common.Utils;


namespace CodeGenerator.Application.Services.Client
{
    /// <summary>
    /// 客户端 - 应用服务
    /// </summary>
    public class ClientService : AppService, IClientService
    {
        
		private readonly IMapper _mapper;
		private readonly Paged _paged;
		private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Ctor
        /// </summary>
		public ClientService(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			Paged paged,
			IClientRepository clientRepository
			) : base(unitOfWork)

        {
			this._mapper = mapper;
			this._paged = paged;
			this._clientRepository = clientRepository;

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<long>> CreateAsync(ClientInputDto input)
        {
            await CheckReferenceAsync(input);
            var client = _mapper.Map<Core.Entities.Client>(input);
			client .Id = SnowflakeId.Default().NextId();
            await _clientRepository.InsertAsync(client);
            await SaveAsync();
            return AppServiceResult(client.Id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppServiceResult> UpdateAsync(long id, ClientInputDto input)
        {
            await CheckReferenceAsync(input);
            var client = await _clientRepository.FindByIdAsync(id);
            if (null == client)
            {
                throw new AppServiceException("您正在修改的数据不存在");
            }
			client = _mapper.Map(input, client);
            await _clientRepository.UpdateAsync(client);
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
            var client = await _clientRepository.FindByIdAsync(id);
            if (null == client)
            {
                throw new AppServiceException("您要删除的数据不存在");
            }
            await _clientRepository.DeleteAsync(id);
            await SaveAsync();
            return AppServiceResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<ClientDto>> GetByIdAsync(long id)
        {
            var client = await _clientRepository.FindByIdAsync(id);
            if (null == client)
            {
                throw new AppServiceException("您查询的数据不存在");
            }

            var dto = _mapper.Map<ClientDto>(client);
            return AppServiceResult(dto);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<AppServiceResult<PagedResult<ClientDto>>> QueryListAsync(ClientParamsDto queryParams)
        {
            var query = _clientRepository.Query
                .OrderByDescending(t => t.Id)
                .AsQueryable();
            //TODO: 需要处理查询参数
            var pagedData = await _paged.GetPagedAsync<Core.Entities.Client, ClientDto>(
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
        private async Task CheckReferenceAsync(ClientInputDto input)
        {

            return;
        }
    }
}