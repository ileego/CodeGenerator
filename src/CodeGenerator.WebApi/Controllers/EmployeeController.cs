using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CodeGenerator.Application.DTOs.Employee;
using CodeGenerator.Application.Services.Employee;
using CodeGenerator.Infra.Common.Authorize;
using CodeGenerator.Infra.Common.Paging;
using CodeGenerator.Infra.Common.Service;


namespace CodeGenerator.WebApi.Controllers
{
    /// <summary>
    /// 工作人员 - 应用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// 创建 - 工作人员
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Permission("工作人员管理", "创建")]
        public async Task<ActionResult<AppServiceResult<long>>> CreateAsync(
            [FromBody] EmployeeInputDto inputDto)
        {
            return await _employeeService.CreateAsync(inputDto);
        }

        /// <summary>
        /// 修改 - 工作人员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [Permission("工作人员管理", "修改")]
        public async Task<ActionResult<AppServiceResult>> UpdateAsync([FromRoute] long id,
            [FromBody] EmployeeInputDto inputDto)
        {
            return await _employeeService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 删除 - 工作人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id}")]
        [Permission("工作人员管理", "删除")]
        public async Task<ActionResult<AppServiceResult>> DeleteAsync([FromRoute] long id)
        {
            return await _employeeService.DeleteAsync(id);
        }

        /// <summary>
        /// 按Id查询 - 工作人员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("工作人员管理", "按Id查明细")]
        public async Task<ActionResult<AppServiceResult<EmployeeDto>>> GetByIdAsync([FromRoute] long id)
        {
            return await _employeeService.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询 - 工作人员
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("queryList")]
        [Permission("工作人员管理", "查询列表")]
        public async Task<ActionResult<AppServiceResult<PagedResult<EmployeeDto>>>> QueryListAsync(
            [FromBody] EmployeeParamsDto queryParams)
        {
            return await _employeeService.QueryListAsync(queryParams);
        }
    }
}
