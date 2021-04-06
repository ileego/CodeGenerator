using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.ClientsAuthorize;
using CodeGenerator.Application.Services.ClientsAuthorize;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 客户端授权 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsAuthorizeController : ControllerBase
    {
        private readonly IClientsAuthorizeService _clientsAuthorizeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public ClientsAuthorizeController(IClientsAuthorizeService clientsAuthorizeService)
        {
            this._clientsAuthorizeService = clientsAuthorizeService;
        }

        /// <summary>
        /// 创建 - 客户端授权
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("客户端授权管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] ClientsAuthorizeInputDto inputDto)
        {
            return await _clientsAuthorizeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 客户端授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("客户端授权管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] ClientsAuthorizeInputDto inputDto)
        {
            return await _clientsAuthorizeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 客户端授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("客户端授权管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _clientsAuthorizeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 客户端授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission("客户端授权管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<ClientsAuthorizeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _clientsAuthorizeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 客户端授权
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("客户端授权管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<ClientsAuthorizeDto>>>> QueryListAsync(
            [FromBody] ClientsAuthorizeParamsDto queryParams)
        {
            return await _clientsAuthorizeService.QueryListAsync(queryParams);
        }
    }
}
