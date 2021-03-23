using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Infra.Common.Paging
{
    public class Paged
    {
        /// <summary>
        /// 需要注入automapper configuration provider
        /// </summary>
        private readonly IConfigurationProvider _mapperConfigurationProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configurationProvider">MapperConfigurationProvider</param>
        public Paged(IConfigurationProvider configurationProvider)
        {
            _mapperConfigurationProvider = configurationProvider;
        }

        public PagedResult<T> GetPaged<T>(IQueryable<T> query,
                                         int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public PagedResult<U> GetPaged<T, U>(IQueryable<T> query,
                                            int page, int pageSize) where U : class
        {
            var result = new PagedResult<U>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<U>(_mapperConfigurationProvider)
                                  .ToList();

            return result;
        }
    }
}
