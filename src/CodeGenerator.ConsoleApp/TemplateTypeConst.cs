using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace CodeGenerator.ConsoleApp
{
    /// <summary>
    /// 模板类型与对应的文件名称
    /// </summary>
    public static class TemplateTypeConst
    {
        public const string CONTROLLER = "Controller.cst";
        public const string ENTITY = "Entity.cst";
        public const string ENTITY_MAP = "EntityMap.cst";
        public const string IREPOSITORY = "IRepository.cst";
        public const string REPOSITORY = "Repository.cst";
        public const string ISERVICE = "IService.cst";
        public const string SERVICE = "Service.cst";
        public const string INPUT_DTO = "InputDto.cst";
        public const string QUERY_PARAMS_DTO = "QueryParamsDto.cst";
        public const string QUERY_RESULT_DTO = "QueryResultDto.cst";
        public const string VALIDATOR = "Validator.cst";
        public const string MAPPER_PROFILES = "MapperProfiles.cst";
    }
}
