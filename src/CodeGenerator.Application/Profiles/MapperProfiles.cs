using AutoMapper;
using System.Collections.Generic;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Core.Entities;
using CodeGenerator.Application.DTOs.ApiResource;
using CodeGenerator.Application.DTOs.ApplicationAction;
using CodeGenerator.Application.DTOs.ApplicationMenu;
using CodeGenerator.Application.DTOs.ApplicationMenuAction;
using CodeGenerator.Application.DTOs.ApplicationRole;
using CodeGenerator.Application.DTOs.ApplicationSystem;
using CodeGenerator.Application.DTOs.Client;
using CodeGenerator.Application.DTOs.ClientsAuthorize;
using CodeGenerator.Application.DTOs.DataAccessAuthorize;
using CodeGenerator.Application.DTOs.Department;
using CodeGenerator.Application.DTOs.District;
using CodeGenerator.Application.DTOs.Employee;
using CodeGenerator.Application.DTOs.EmployeeAuthorize;
using CodeGenerator.Application.DTOs.Organization;
using CodeGenerator.Application.DTOs.OrganizationType;
using CodeGenerator.Application.DTOs.RoleAuthorize;


namespace CodeGenerator.Application.Profiles
{
    /// <summary>
    /// AutoMapper 配置
    /// </summary>
    public class MapperProfiles : Profile
    {
        /// <summary>
        /// 配置Map
        /// </summary>
        public MapperProfiles()
        {
			/* Api资源 */
			CreateMap<ApiResource,ApiResourceInputDto>();
			CreateMap<ApiResourceInputDto,ApiResource>();
			CreateMap<ApiResource,ApiResourceDto>();
			CreateMap<ApiResourceDto,ApiResource>();

			/* 应用功能 */
			CreateMap<ApplicationAction,ApplicationActionInputDto>();
			CreateMap<ApplicationActionInputDto,ApplicationAction>();
			CreateMap<ApplicationAction,ApplicationActionDto>();
			CreateMap<ApplicationActionDto,ApplicationAction>();

			/* 应用菜单 */
			CreateMap<ApplicationMenu,ApplicationMenuInputDto>();
			CreateMap<ApplicationMenuInputDto,ApplicationMenu>();
			CreateMap<ApplicationMenu,ApplicationMenuDto>();
			CreateMap<ApplicationMenuDto,ApplicationMenu>();

			/* 应用菜单功能关联 */
			CreateMap<ApplicationMenuAction,ApplicationMenuActionInputDto>();
			CreateMap<ApplicationMenuActionInputDto,ApplicationMenuAction>();
			CreateMap<ApplicationMenuAction,ApplicationMenuActionDto>();
			CreateMap<ApplicationMenuActionDto,ApplicationMenuAction>();

			/* 角色 */
			CreateMap<ApplicationRole,ApplicationRoleInputDto>();
			CreateMap<ApplicationRoleInputDto,ApplicationRole>();
			CreateMap<ApplicationRole,ApplicationRoleDto>();
			CreateMap<ApplicationRoleDto,ApplicationRole>();

			/* 应用系统 */
			CreateMap<ApplicationSystem,ApplicationSystemInputDto>();
			CreateMap<ApplicationSystemInputDto,ApplicationSystem>();
			CreateMap<ApplicationSystem,ApplicationSystemDto>();
			CreateMap<ApplicationSystemDto,ApplicationSystem>();

			/* 客户端 */
			CreateMap<Client,ClientInputDto>();
			CreateMap<ClientInputDto,Client>();
			CreateMap<Client,ClientDto>();
			CreateMap<ClientDto,Client>();

			/* 客户端授权 */
			CreateMap<ClientsAuthorize,ClientsAuthorizeInputDto>();
			CreateMap<ClientsAuthorizeInputDto,ClientsAuthorize>();
			CreateMap<ClientsAuthorize,ClientsAuthorizeDto>();
			CreateMap<ClientsAuthorizeDto,ClientsAuthorize>();

			/* 数据访问授权 */
			CreateMap<DataAccessAuthorize,DataAccessAuthorizeInputDto>();
			CreateMap<DataAccessAuthorizeInputDto,DataAccessAuthorize>();
			CreateMap<DataAccessAuthorize,DataAccessAuthorizeDto>();
			CreateMap<DataAccessAuthorizeDto,DataAccessAuthorize>();

			/* 部门 */
			CreateMap<Department,DepartmentInputDto>();
			CreateMap<DepartmentInputDto,Department>();
			CreateMap<Department,DepartmentDto>();
			CreateMap<DepartmentDto,Department>();

			/* 行政区 */
			CreateMap<District,DistrictInputDto>();
			CreateMap<DistrictInputDto,District>();
			CreateMap<District,DistrictDto>();
			CreateMap<DistrictDto,District>();

			/* 工作人员 */
			CreateMap<Employee,EmployeeInputDto>();
			CreateMap<EmployeeInputDto,Employee>();
			CreateMap<Employee,EmployeeDto>();
			CreateMap<EmployeeDto,Employee>();

			/* 工作人员授权 */
			CreateMap<EmployeeAuthorize,EmployeeAuthorizeInputDto>();
			CreateMap<EmployeeAuthorizeInputDto,EmployeeAuthorize>();
			CreateMap<EmployeeAuthorize,EmployeeAuthorizeDto>();
			CreateMap<EmployeeAuthorizeDto,EmployeeAuthorize>();

			/* 组织 */
			CreateMap<Organization,OrganizationInputDto>();
			CreateMap<OrganizationInputDto,Organization>();
			CreateMap<Organization,OrganizationDto>();
			CreateMap<OrganizationDto,Organization>();

			/* 组织类型 */
			CreateMap<OrganizationType,OrganizationTypeInputDto>();
			CreateMap<OrganizationTypeInputDto,OrganizationType>();
			CreateMap<OrganizationType,OrganizationTypeDto>();
			CreateMap<OrganizationTypeDto,OrganizationType>();

			/* 角色授权 */
			CreateMap<RoleAuthorize,RoleAuthorizeInputDto>();
			CreateMap<RoleAuthorizeInputDto,RoleAuthorize>();
			CreateMap<RoleAuthorize,RoleAuthorizeDto>();
			CreateMap<RoleAuthorizeDto,RoleAuthorize>();


        }
    }
}