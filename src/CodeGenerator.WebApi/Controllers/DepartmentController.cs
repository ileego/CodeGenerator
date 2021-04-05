using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.Department;
using CodeGenerator.Application.Services.Department;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 部门 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// Ctor
        /// </summary>
        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        /// <summary>
        /// 创建 - 部门
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("部门管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] DepartmentInputDto inputDto)
        {
            return await _departmentService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("部门管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] DepartmentInputDto inputDto)
        {
            return await _departmentService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("部门管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _departmentService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("部门管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<DepartmentDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _departmentService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 部门
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("部门管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<DepartmentDto>>>> QueryListAsync(
            [FromBody] DepartmentParamsDto queryParams)
        {
            return await _departmentService.QueryListAsync(queryParams);
        }
    }
}
