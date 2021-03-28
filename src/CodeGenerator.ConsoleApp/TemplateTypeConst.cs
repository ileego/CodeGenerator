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
        public const string DTO = "Dto.cst";
        public const string ENTITY = "Entity.cst";
        public const string ENTITYMAP = "EntityMap.cst";
        public const string IREPOSITORY = "IRepository.cst";
        public const string REPOSITORY = "Repository.cst";
        public const string ISERVICE = "IService.cst";
        public const string SERVICE = "Service.cst";
        public const string QUERYPARAMS = "QueryParams.cst";
        public const string QUERYRESULT = "QueryResult.cst";
        public const string VALIDATOR = "Validator.cst";
    }
}
