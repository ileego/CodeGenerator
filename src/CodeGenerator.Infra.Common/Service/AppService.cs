using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeGenerator.Infra.Common.Service
{
    /// <summary>
    /// 应用服务基类
    /// </summary>
    public abstract class AppService : IAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected AppService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        protected bool BeginTransaction()
        {
            this._unitOfWork.BeginTransaction();
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected bool Save()
        {
            this._unitOfWork.Commit();
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> SaveAsync()
        {
            await this._unitOfWork.CommitAsync();
            return true;
        }

        /// <summary>
        /// 默认返回成功，不附带其它数据
        /// </summary>
        /// <returns></returns>
        protected AppServiceResult AppServiceResult() => new AppServiceResult();

        /// <summary>
        /// 默认返回成功，携带value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected AppServiceResult<TValue> AppServiceResult<TValue>(TValue value) =>
            new AppServiceResult<TValue>(value);

        /// <summary>
        /// 出问题了...
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="detail"></param>
        /// <param name="title"></param>
        /// <param name="instance"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected ProblemDetails Problem(HttpStatusCode? statusCode = null,
            string detail = null,
            string title = null,
            string instance = null,
            string type = null)
        {
            return new ProblemDetails()
            {
                Status = (int?)statusCode,
                Detail = detail,
                Title = title,
                Instance = instance,
                Type = type
            };
        }
    }
}
