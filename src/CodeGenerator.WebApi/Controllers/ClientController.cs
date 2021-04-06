using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.Client;
using CodeGenerator.Application.Services.Client;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 客户端 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        /// <summary>
        /// 创建 - 客户端
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("客户端管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ClientInputDto inputDto)
        {
            return await _clientService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 客户端
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("客户端管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ClientInputDto inputDto)
        {
            return await _clientService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("客户端管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _clientService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("客户端管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ClientDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _clientService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 客户端
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("客户端管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ClientDto>>>> QueryListAsync(
            [FromBody] ClientParamsDto queryParams)
        {
            return await _clientService.QueryListAsync(queryParams);
        }
    }
}
