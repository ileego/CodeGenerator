using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取当前DBContext
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
