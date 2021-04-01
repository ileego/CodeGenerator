using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeGenerator.Infra.Common.Service
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public sealed class AppServiceResult
    {
        public AppServiceResult() { }

        public AppServiceResult([NotNull] ProblemDetails problemDetails)
        {
            this.ProblemDetails = problemDetails;
        }

        public bool IsSucceed => ProblemDetails == null;

        public ProblemDetails ProblemDetails { get; private set; }

        public static implicit operator AppServiceResult([NotNull] ProblemDetails problemDetails)
        {
            return new AppServiceResult(problemDetails);
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public sealed class AppServiceResult<TValue>
    {
        public AppServiceResult() { }

        public AppServiceResult([NotNull] TValue value)
        {
            this.Content = value;
        }

        public AppServiceResult([NotNull] ProblemDetails problemDetails)
        {
            this.ProblemDetails = problemDetails;
        }

        public ProblemDetails ProblemDetails { get; set; }

        public bool IsSucceed => this.Content != null && this.ProblemDetails == null;

        public TValue Content { get; set; }

        public static implicit operator AppServiceResult<TValue>(AppServiceResult result)
        {
            return new AppServiceResult<TValue>()
            {
                Content = default,
                ProblemDetails = result.ProblemDetails
            };
        }

        public static implicit operator AppServiceResult<TValue>(TValue value)
        {
            return new AppServiceResult<TValue>(value);
        }

        public static implicit operator AppServiceResult<TValue>(ProblemDetails problemDetails)
        {
            return new AppServiceResult<TValue>(problemDetails);
        }
    }
}
